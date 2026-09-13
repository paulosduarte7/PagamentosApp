using PagamentosApp.Application.Dtos;
using PagamentosApp.Domain.Entities;
using PagamentosApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Application.Services
{
    public interface ITextProcessingAppService
    {
        Task<TransacaoCartaoDto> ProcessarEGravarComprovanteAsync(byte[] fileBytes, string mimeType, string pessoa, CancellationToken cancellationToken = default);

        Task<List<Transacao>> ObterTransacoes(string usuario);

        Task<bool> RemoverTransacao(string id);

    }
}
