using PagamentosApp.Domain.Entities;

namespace PagamentosApp.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task CriarAsync(Transacao transacao, CancellationToken cancellationToken = default);
        Task<List<Transacao>> ObterAsync(string usuario, CancellationToken cancellationToken = default);
        Task<bool> RemoverAsync(string id, CancellationToken cancellationToken = default);
    }
}
