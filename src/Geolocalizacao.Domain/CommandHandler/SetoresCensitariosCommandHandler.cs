using Geolocalizacao.Domain.Commands.SetoresCensitarios;
using Geolocalizacao.Domain.Core.Bus;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository;
using Geolocalizacao.Domain.Enumerables;
using MediatR;
using NetTopologySuite.IO.ShapeFile.Extended;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Geolocalizacao.Domain.CommandHandler
{
    public class SetoresCensitariosCommandHandler : CommandHandler,
        IRequestHandler<RegistrarSetoresCensitariosCommand, bool>,
        IRequestHandler<ProcessarSetoresCensitariosCommand, bool>,
        IRequestHandler<CarregarSetoresCommand, bool>
    {
        private readonly IArquivoSetoresCensitariosRepository _repository;
        private readonly ISetorRepository _setorRepository;
        private readonly IUser _user;
        private readonly IMediatorHandler _mediator;

        public SetoresCensitariosCommandHandler(
            IArquivoSetoresCensitariosRepository repository,
            ISetorRepository setorRepository,
            IUser user,
            IMediatorHandler mediator,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _repository = repository;
            _setorRepository = setorRepository;
            _user = user;
            _mediator = mediator;
        }

        public async Task<bool> Handle(RegistrarSetoresCensitariosCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var setorCensitario = new ArquivoSetoresCensitarios(
                Guid.NewGuid(),
                StatusArquivoSetorCensitario.Pendente,
                message.Nome,
                message.Files.First().FileName,
                DateTime.Now,
                _user.Id
                );

            var fileName = $"spatial_data_{setorCensitario.Id}";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            // Salvar arquivos em pasta temporária
            foreach (var file in message.Files)
            {
                using (Stream fileStream = new FileStream($"{filePath}{Path.GetExtension(file.FileName)}", FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            await _repository.Add(setorCensitario);
            await Commit();

            await _mediator.SendCommand(new ProcessarSetoresCensitariosCommand(setorCensitario.Id));

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(ProcessarSetoresCensitariosCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var setorCensitario = await _repository.GetByIdAsync(message.Id);
            setorCensitario.AtualizarStatus(StatusArquivoSetorCensitario.Processando);

            _repository.Update(setorCensitario);
            await Commit();


            var fileName = $"spatial_data_{setorCensitario.Id}";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            var setores = new List<Setor>();
            try
            {
                setores = ReadShapeFile(filePath);
            }
            catch (Exception ex)
            {
                var mensagem = "Ocorreu um erro ao converter arquivo Shape File: " + ex.Message;

                AddNotification("", mensagem);
                setorCensitario.AtualizarStatus(StatusArquivoSetorCensitario.Erro, mensagem);

                _repository.Update(setorCensitario);
                await Commit();

                await Task.FromResult(false);
            }            

            await _mediator.SendCommand(new CarregarSetoresCommand(setorCensitario.Id, setores));

            File.Delete($"{filePath}.shp");
            File.Delete($"{filePath}.shp");

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(CarregarSetoresCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var setorCensitario = await _repository.GetByIdAsync(message.Id);

            if (!message.Setores.Any())
            {
                var mensagem = "O arquivo informado não possuem uma lista de setores.";
                AddNotification("", mensagem);
                setorCensitario.AtualizarStatus(StatusArquivoSetorCensitario.Erro, mensagem);

                _repository.Update(setorCensitario);
                await Commit();

                await Task.FromResult(false);
            }

            //var codigos = message.Setores.Select(c => c.FeatureId);

            //var setoresExistentes = _setorRepository.Find(c => c.Uf == message.Setores.First().Uf && codigos.Contains(c.FeatureId));            

            try
            {
                //Deleta todos os setores da uf
                var result = _setorRepository.DeletarSetores(message.Setores.First().Uf);

                //// Adicionar itens que não existem no banco de dados
                //var setoresAAdicionar = message.Setores.Where(c => !setoresExistentes.Select(s => s.FeatureId).Contains(c.FeatureId)).ToList();

                //if (setoresAAdicionar.Any())
                //    await _setorRepository.AddRange(setoresAAdicionar);

                // Adiciona todos os setores
                await _setorRepository.AddRange(message.Setores);
            }
            catch (Exception ex)
            {
                var mensagem = "Ocorreu um erro ao adicionar áreas do setor censitário: " + ex.Message;

                AddNotification("", mensagem);
                setorCensitario.AtualizarStatus(StatusArquivoSetorCensitario.Erro, mensagem);

                _repository.Update(setorCensitario);
                await Commit();

                await Task.FromResult(false);
            }

            //try
            //{
            //    // Atualizar itens que existem no banco de dados
            //    var setoresAAtualizar = message.Setores.Where(c => setoresExistentes.Select(s => s.FeatureId).Contains(c.FeatureId));
            //    foreach (var item in setoresAAtualizar)
            //    {
            //        await _setorRepository.UpdateAsync(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var mensagem = "Ocorreu um erro ao atualizar áreas do setor censitário: " + ex.Message;

            //    AddNotification("", mensagem);
            //    setorCensitario.AtualizarStatus(StatusArquivoSetorCensitario.Erro, mensagem);

            //    _repository.Update(setorCensitario);
            //    await Commit();

            //    await Task.FromResult(false);
            //}

            setorCensitario.AtualizarStatus(StatusArquivoSetorCensitario.Processado);

            _repository.Update(setorCensitario);
            await Commit();

            return await Task.FromResult(true);
        }

        private List<Setor> ReadShapeFile(string filePath)
        {
            using var reader = new ShapeDataReader(filePath);

            var setores = new List<Setor>();

            var mbr = reader.ShapefileBounds;
            var result = reader.ReadByMBRFilter(mbr);
            var coll = result.GetEnumerator();
            while (coll.MoveNext())
            {
                var item = coll.Current;

                var texto = item.Geometry.ToText();
                texto = texto.Replace(item.Geometry.GeometryType.ToUpper() + " ", "");

                Geometry geometry = null;

                if (item.Geometry.GeometryType == "Polygon")
                {
                    var split = texto.Split('(', ')');

                    if (split.Any(c => c.Trim() != "" && c.Trim() != ","))
                    {

                        // Converter objeto NetTopologySuite geometry em array de double 
                        var poligonos = split.Where(c => c.Trim() != "" && c.Trim() != ",")
                        .Select(poligonos => poligonos.TrimStart().Split(',')
                            .Select(coordenadas => coordenadas.TrimStart().Split(' ')
                                .Select(pontos => double.Parse(pontos, CultureInfo.InvariantCulture.NumberFormat))
                                .ToArray())
                            .ToArray())
                        .ToArray();

                        geometry = new Domain.Entities.SetoresCensitarios.Polygon(item.Geometry.GeometryType, poligonos);
                    }
                }
                else if (item.Geometry.GeometryType == "MultiPolygon")
                {
                    if ((item.Geometry as dynamic).Count > 0)
                    {
                        var geometries = (item.Geometry as dynamic).Geometries;

                        var objetos = new List<double[][][]>();

                        foreach (var polygon in geometries as NetTopologySuite.Geometries.Polygon[])
                        {
                            var subTexto = polygon.AsText();
                            subTexto = subTexto.Replace(polygon.GeometryType.ToUpper() + " ", "");
                            var split = subTexto.Split('(', ')');

                            // Converter objeto NetTopologySuite geometry em array de double 
                            var poligonos = split.Where(c => c.Trim() != "" && c.Trim() != ",")
                            .Select(poligonos => poligonos.TrimStart().Split(',')
                                .Select(coordenadas => coordenadas.TrimStart().Split(' ')
                                    .Select(pontos => double.Parse(pontos, CultureInfo.InvariantCulture.NumberFormat))
                                    .ToArray())
                                .ToArray())
                            .ToArray();

                            objetos.Add(poligonos);
                        }

                        geometry = new Domain.Entities.SetoresCensitarios.MultiPolygon(item.Geometry.GeometryType, objetos.ToArray());
                    }

                }

                if (geometry == null)
                    continue;

                setores.Add(new Setor
                            (
                                Guid.NewGuid(),
                                item.FeatureId,
                                geometry,
                                (string)item.Attributes.GetOptionalValue("CD_SETOR"),
                                (string)item.Attributes.GetOptionalValue("CD_SIT"),
                                (string)item.Attributes.GetOptionalValue("NM_SIT"),
                                (string)item.Attributes.GetOptionalValue("CD_UF"),
                                (string)item.Attributes.GetOptionalValue("NM_UF"),
                                (string)item.Attributes.GetOptionalValue("SIGLA_UF"),
                                (string)item.Attributes.GetOptionalValue("CD_MUN"),
                                (string)item.Attributes.GetOptionalValue("NM_MUN"),
                                (string)item.Attributes.GetOptionalValue("CD_DIST"),
                                (string)item.Attributes.GetOptionalValue("NM_DIST"),
                                (string)item.Attributes.GetOptionalValue("CD_SUBDIST"),
                                (string)item.Attributes.GetOptionalValue("NM_SUBDIST")
                                ));

            }

            return setores;
        }
    }
}
