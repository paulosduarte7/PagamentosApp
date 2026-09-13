using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Domain.Entities
{

    public class Transacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }

        [BsonElement("valor")]
        public decimal Valor { get; private set; }

        [BsonElement("descricao")]
        public string Descricao { get; private set; } = string.Empty;

        [BsonElement("data_hora")]
        public DateTime DataHora { get; private set; }

        [BsonElement("tipo_transacao")]
        public string TipoTransacao { get; private set; } = string.Empty;

        [BsonElement("criado_em")]
        public DateTime CriadoEm { get; private set; }
        [BsonElement("Pessoa")]
        public string Pessoa { get; private set; } = string.Empty;

        // Construtor para ORM / MongoDB
        public Transacao() { }

        public Transacao(decimal valor, string descricao, DateTime dataHora, string tipoTransacao, string pessoa)
        {
            Valor = valor;
            Descricao = descricao;
            DataHora = dataHora;
            CriadoEm = DateTime.UtcNow;
            TipoTransacao = tipoTransacao;
            Pessoa = pessoa;
        }
    }
}