using Geolocalizacao.Domain.Core.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Geolocalizacao.Domain.Entities.SetoresCensitarios
{
    public class Setor : Entity
    {
        public Setor(
            Guid id,
            long featureId, 
            Geometry geometry, 
            string codigo, 
            string codigoSituacao, 
            string situacao, 
            string codigoUf, 
            string nomeUf, 
            string uf, 
            string codigoMunicipio, 
            string municipio, 
            string codigoDistrito, 
            string nomeDistrito, 
            string codigoSubDistrito, 
            string nomeSubDistrito)
        {
            Id = id;
            FeatureId = featureId;
            Geometry = geometry;
            Codigo = codigo;
            CodigoSituacao = codigoSituacao;
            Situacao = situacao;
            CodigoUf = codigoUf;
            NomeUf = nomeUf;
            Uf = uf;
            CodigoMunicipio = codigoMunicipio;
            Municipio = municipio;
            CodigoDistrito = codigoDistrito;
            NomeDistrito = nomeDistrito;
            CodigoSubDistrito = codigoSubDistrito;
            NomeSubDistrito = nomeSubDistrito;
        }

        public long FeatureId { get; private set; }
        [BsonElement("geometry")]
        public Geometry Geometry { get; private set; }
        public string Codigo { get; private set; }
        public string CodigoSituacao { get; private set; }
        public string Situacao { get; private set; }
        public string CodigoUf { get; private set; }
        public string NomeUf { get; private set; }
        public string Uf { get; private set; }
        public string CodigoMunicipio { get; private set; }
        public string Municipio { get; private set; }
        public string CodigoDistrito { get; private set; }
        public string NomeDistrito { get; private set; }
        public string CodigoSubDistrito { get; private set; }
        public string NomeSubDistrito { get; private set; }
    }
}
