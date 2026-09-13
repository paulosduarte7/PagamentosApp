using Microsoft.AspNetCore.Mvc;
using PagamentosApp.Application.Dtos;
using PagamentosApp.Application.Services;

namespace PagamentosApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextProcessingController : ControllerBase
    {
        private readonly ITextProcessingAppService _textProcessingAppService;

        public TextProcessingController(ITextProcessingAppService textProcessingAppService)
        {
            _textProcessingAppService = textProcessingAppService;
        }

        [HttpPost("incluir")]
        public async Task<IActionResult> ProcessFile([FromForm] string usuario, IFormFile? file, CancellationToken cancellationToken)
        {
            byte[]? fileBytes = null;
            string? mimeType = null;

            if (file is not null && file.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, cancellationToken);
                fileBytes = memoryStream.ToArray();
                mimeType = file.ContentType; // Ex: image/png, application/pdf
            }
            else
                return BadRequest("Nenhum arquivo foi enviado.");
            try
            {
                var response = await _textProcessingAppService.ProcessarEGravarComprovanteAsync(fileBytes, mimeType, usuario, cancellationToken);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }

        [HttpGet("transacoes/{usuario}")]
        public async Task<IActionResult> GetTransacoesPessoa(string usuario, CancellationToken cancellationToken)
        {
            if(usuario is null || string.IsNullOrWhiteSpace(usuario))
                return BadRequest("O usuário não pode ser nulo ou vazio.");

            try
            {
                var transacoes = await _textProcessingAppService.ObterTransacoes(usuario);
                return Ok(transacoes);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações: {ex.Message}");
            }
                
        }

        [HttpDelete("transacoes/{id}")]
        public async Task<IActionResult> DeleteTransacao(string id, CancellationToken cancellationToken)
        {
            try
            {
                await _textProcessingAppService.RemoverTransacao(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir transação: {ex.Message}");
            }
        }
    }
}
