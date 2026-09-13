using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Application.Prompts
{
    public static class FinancePrompts
    {
        public static string ExtracaoComprovanteCartao(string pessoa)
        {
            var anoAtual = DateTime.UtcNow.Year;

            return $$"""
                     Você é um extrator de dados financeiros de alta precisão especializado em OCR e estruturação de dados. 
                    Sua tarefa é analisar a imagem da notificação ou comprovante de transação financeira e extrair exatamente as informações presentes nela. 
                    Regras Estritas de Extração: 
                    1. valor: Tipo number. Extraia apenas o valor numérico usando ponto (.) como separador decimal (ex: 150.50). Remova símbolos de moeda (R$) e substitua vírgulas por pontos. Se o valor estiver totalmente ilegível ou ausente, defina como null. 
                    2. descricao: Tipo string. Nome do estabelecimento, pessoa, loja ou serviço onde a transação foi realizada. Se ausente ou ilegível, defina estritamente como null. 
                    3. dataHora: Tipo string no formato ISO-8601 (AAAA-MM-DDTHH:mm:ss). Se o ano não estiver visível na imagem, assuma o ano {{anoAtual}}. Se os segundos não estiverem visíveis, defina como 00. 
                    4. tipoTransacao: Tipo string. Identifique a modalidade da operação. Permita EXCLUSIVAMENTE um dos seguintes valores exatos: "TED", "Pix", "Crédito", "Débito" ou "Outros". Se não for possível determinar com clareza, marque estritamente como "Outros". 
                    5. pessoa: Tipo string. Use estritamente o valor fixo: "{{pessoa}}". 
                    Guardrails e Formatação: 
                    - NUNCA invente ou infira informações que não estejam visíveis na imagem. 
                    - Sua resposta DEVE conter ESTRITAMENTE um objeto JSON válido. 
                    - NÃO inclua texto explicativo, saudações, introduções ou marcações Markdown de código (```json). 
                    Exemplo do formato JSON esperado: { "valor": 129.90, "descricao": "Uber *Ride", "dataHora": "{{anoAtual}}-09-08T14:30:00", "tipoTransacao": "Crédito", "pessoa": "{{pessoa}}" } 
                """;
        }
    }
}

