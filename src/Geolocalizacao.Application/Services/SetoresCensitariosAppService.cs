using AutoMapper;
using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Commands.SetoresCensitarios;
using Geolocalizacao.Domain.Core.Bus;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository;
using Geolocalizacao.Domain.Enumerables;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO.ShapeFile.Extended;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Services
{
    public class SetoresCensitariosAppService : ApplicationBaseService, ISetoresCensitariosAppService
    {
        private readonly IArquivoSetoresCensitariosRepository _repository;        
        private readonly ISetorRepository _setorRepository;
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public SetoresCensitariosAppService(
            IArquivoSetoresCensitariosRepository repository,
            ISetorRepository setorRepository,
            IUser user,
            IMapper mapper,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _repository = repository;
            _setorRepository = setorRepository;
            _mapper = mapper;
            _mediator = mediator;
            _user = user;
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await _repository.GetAll().AnyAsync(c => c.Id == id);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<ArquivoSetoresCensitariosViewModel>> GetAllAsync()
        {
            return await _repository.GetAll().Select(c => new ArquivoSetoresCensitariosViewModel
            {
                Id = c.Id,
                Status = c.Status,
                Nome = c.Nome,
                NomeArquivo = c.NomeArquivo,
                IdUsuarioInclusao = c.IdUsuarioInclusao,
                DataInclusao = c.DataInclusao,
            }).ToListAsync();
        }

        public async Task RegisterAsync(ArquivoSetoresCensitariosUploadViewModel obj)
        {
            var registerCommand = _mapper.Map<RegistrarSetoresCensitariosCommand>(obj);
            await _mediator.SendCommand(registerCommand);
        }

        public async Task CerragarArquivo(ArquivoSetoresCensitariosUploadViewModel obj)
        {
            if (!obj.Files.Any(c=> c.FileName.Substring(c.FileName.Length - 3) == "shp"))
            {
                _notifications.AddNotification("", "É obrigatório o carregamento de arquivo .shp");
                return;
            }

            if (!obj.Files.Any(c => c.FileName.Substring(c.FileName.Length - 3) == "dbf"))
            {
                _notifications.AddNotification("", "É obrigatório o carregamento de arquivo .dbf");
                return;
            }

            var arquivo = new ArquivoSetoresCensitarios(
                Guid.NewGuid(),
                StatusArquivoSetorCensitario.Pendente,
                obj.Nome,
                obj.Files.FirstOrDefault(c => c.FileName.Substring(c.FileName.Length - 3) == "shp").FileName,
                DateTime.Now,
                _user.Id
                );

            await _repository.Add(arquivo);
            await _repository.SaveChangesAsync();
            try
            {
                var fileName = $"spatial_data_{Guid.NewGuid()}";
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                // Salvar Arquivos em pasta temporária
                foreach (var item in obj.Files)
                {
                    await SalvarArquivo(item, $"{filePath}{Path.GetExtension(item.FileName)}");
                }

                await ProcessarArquivos(obj.Files, arquivo, filePath);                

            }
            catch (Exception ex)
            {

                var y = 1;
            }
        }

        private async Task ProcessarArquivos(List<IFormFile> files, ArquivoSetoresCensitarios arquivoSetoresCensitarios, string filePath)
        {

            try
            {
                arquivoSetoresCensitarios.UpdateStatus((int)StatusArquivoSetorCensitario.Processando);
                _repository.Update(arquivoSetoresCensitarios);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            
            // Processar arquivos
            var setores = LerShapeFile($"{filePath}.shp");

            try
            {
                await _setorRepository.AddRange(setores);
            }
            catch (Exception ex)
            {

                throw;
            }
            

            try
            {
                arquivoSetoresCensitarios.UpdateStatus((int)StatusArquivoSetorCensitario.Processado);
                _repository.Update(arquivoSetoresCensitarios);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            try
            {
                // Remover arquivos da pasta temporária
                foreach (var item in files)
                {
                    File.Delete($"{filePath}{Path.GetExtension(item.FileName)}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }

        private async Task SalvarArquivo(IFormFile file, string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        private List<Setor> LerShapeFile(string filePath)
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
