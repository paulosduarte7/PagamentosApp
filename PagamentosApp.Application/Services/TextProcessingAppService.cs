using PagamentosApp.Application.Dtos;
using PagamentosApp.Application.Prompts;
using PagamentosApp.Application.Services;
using PagamentosApp.Domain.Entities;
using PagamentosApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PagamentosApp.Application.Services
{
    public class TextProcessingAppService : ITextProcessingAppService
    {
        private readonly IAiService _aiService;
        private readonly ITransacaoRepository _transacaoRepository;

        public TextProcessingAppService(IAiService aiService, ITransacaoRepository transacaoRepository)
        {
            _aiService = aiService;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<TransacaoCartaoDto> ProcessarEGravarComprovanteAsync(byte[] fileBytes, string mimeType, string pessoa, CancellationToken cancellationToken = default)
        {
            var prompt = FinancePrompts.ExtracaoComprovanteCartao(pessoa);

            // 1. Envia imagem + prompt para a IA
            var jsonResponse = await _aiService.GenerateResponseAsync(prompt, fileBytes, mimeType, cancellationToken);

            // 2. Desserializa o JSON retornado
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dto = JsonSerializer.Deserialize<TransacaoCartaoDto>(jsonResponse, options);

            if (dto is null)
            {
                throw new InvalidOperationException("Falha ao desserializar os dados da transação extraídos pela IA.");
            }

            // 3. Converte para a Entidade de Domínio e grava no MongoDB
            var transacao = new Transacao(dto.Valor, dto.Descricao, dto.DataHora, dto.TipoTransacao, dto.Pessoa);
            await _transacaoRepository.CriarAsync(transacao, cancellationToken);

            return dto;
        }

        public async Task<List<Transacao>> ObterTransacoes(string usuario)
        {
            var retorno = await _transacaoRepository.ObterAsync(usuario);
            return retorno;
        }

        public async Task<bool> RemoverTransacao(string id)
        {
            var retorno = await _transacaoRepository.RemoverAsync(id);
            return retorno;
        }
    }
}

