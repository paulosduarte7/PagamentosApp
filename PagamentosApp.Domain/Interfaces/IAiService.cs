using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Domain.Interfaces
{
    public interface IAiService
    {
        Task<string> GenerateResponseAsync(string prompt, byte[]? fileBytes = null, string? mimeType = null, CancellationToken cancellationToken = default);
    }
}

