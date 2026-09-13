using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PagamentosApp.Infrastructure.Integrations.Dtos
{
    //estrutura request
    public record GeminiRequest(
        [property: JsonPropertyName("contents")] List<GeminiContent> Contents
    );

    public record GeminiContent(
        [property: JsonPropertyName("parts")] List<GeminiPart> Parts
    );

    public record GeminiPart(
        [property: JsonPropertyName("text")] string? Text = null,
        [property: JsonPropertyName("inlineData")] GeminiInlineData? InlineData = null
    );

    public record GeminiInlineData(
        [property: JsonPropertyName("mimeType")] string MimeType,
        [property: JsonPropertyName("data")] string Data
    );

    // Estrutura do Response
    public record GeminiResponse(
        [property: JsonPropertyName("candidates")] List<GeminiCandidate>? Candidates
    );

    public record GeminiCandidate(
        [property: JsonPropertyName("content")] GeminiContent? Content
    );
}


