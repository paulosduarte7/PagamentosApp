using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Application.Dtos
{
    public record ProcessTextRequest(string UserText, byte[]? FileBytes = null, string? MimeType = null);
}
