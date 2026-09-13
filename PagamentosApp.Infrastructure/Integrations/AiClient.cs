using Microsoft.Extensions.Configuration;
using PagamentosApp.Domain.Interfaces;
using PagamentosApp.Infrastructure.Integrations.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace PagamentosApp.Infrastructure.Integrations
{
    public class AiClient : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration["GeminiSettings:BaseUrl"] ?? "https://generativelanguage.googleapis.com/";
            var apiKey = _configuration["GeminiSettings:ApiKey"];

            _httpClient.BaseAddress = new Uri(baseUrl);

            // Adiciona a chave de API nos headers conforme exigido pelo Google
            if (!string.IsNullOrEmpty(apiKey))
            {
                _httpClient.DefaultRequestHeaders.Remove("X-goog-api-key");
                _httpClient.DefaultRequestHeaders.Add("X-goog-api-key", apiKey);
            }
        }

        public async Task<string> GenerateResponseAsync(string prompt, byte[]? fileBytes = null, string? mimeType = null, CancellationToken cancellationToken = default)
        {
            var parts = new List<GeminiPart>
            {
                new GeminiPart(Text: prompt)
            };

            // Se houver arquivo anexo, adicione como inlineData nas parts
            if (fileBytes is not null && !string.IsNullOrEmpty(mimeType))
            {
                var base64Data = Convert.ToBase64String(fileBytes);
                parts.Add(new GeminiPart(InlineData: new GeminiInlineData(mimeType, base64Data)));
            }

            var requestPayload = new GeminiRequest(
                Contents: new List<GeminiContent> { new GeminiContent(parts) }
            );

            // Dispara a requisição POST para o modelo
            var response = await _httpClient.PostAsJsonAsync(
                "v1beta/models/gemini-3.6-flash:generateContent",
                requestPayload,
                cancellationToken
            );

            // Se o serviço do Google estiver indisponível (503), sobrecarregado (429) ou com erro (400/500)
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                // Aqui você pode logar o erro detalhado do Google
                // _logger.LogError("Erro na API do Gemini: {StatusCode} - {Error}", response.StatusCode, errorContent);

                if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    throw new Exception("O serviço de IA está temporariamente sobrecarregado. Tente novamente em alguns segundos.");
                }

                throw new HttpRequestException($"Erro ao se comunicar com o serviço de IA ({response.StatusCode}): {errorContent}");
            }

            // Desserializa o JSON retornado pelo Gemini
            var geminiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>(cancellationToken: cancellationToken);

            // Extrai o texto contido na primeira resposta
            var textResult = geminiResponse?.Candidates?.FirstOrDefault()
                ?.Content?.Parts?.FirstOrDefault()?.Text;

            return textResult ?? "Nenhuma resposta foi gerada pela IA.";
        }

    }
}
