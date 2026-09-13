using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Application.Dtos
{
    public record TransacaoCartaoDto(
        decimal Valor,
        string Descricao,
        DateTime DataHora,
        string TipoTransacao,
        string Pessoa
    );
}
