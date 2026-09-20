using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagamentosApp.Application.Dtos;
using PagamentosApp.Application.Services;
using System.Security.Claims;

namespace PagamentosApp.Api.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> ProcessFile([FromForm] IFormFile? file, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userIdClaim is null)
                return BadRequest("Usuário não autenticado.");

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
                var response = await _textProcessingAppService.ProcessarEGravarComprovanteAsync(fileBytes, mimeType, userIdClaim, cancellationToken);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }

        [HttpGet("transacoes")]
        public async Task<IActionResult> GetTransacoesPessoa(CancellationToken cancellationToken)
        {
            // Extrai o ID do usuário das Claims do Token JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim is null)
                return BadRequest("Usuário não autenticado.");

            try
            {
                var transacoes = await _textProcessingAppService.ObterTransacoes(userIdClaim);
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
