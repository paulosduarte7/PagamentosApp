using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PagamentosApp.Domain.Entities;
using PagamentosApp.Domain.Interfaces;

namespace PagamentosApp.Infrastructure.Persistence
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly IMongoCollection<Transacao> _collection;

        public TransacaoRepository(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDbSettings:ConnectionString"];
            var databaseName = configuration["MongoDbSettings:DatabaseName"];
            var collectionName = configuration["MongoDbSettings:CollectionName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Transacao>(collectionName);
        }

        public async Task CriarAsync(Transacao transacao, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(transacao, cancellationToken: cancellationToken);
        }


        public async Task<List<Transacao>> ObterAsync(string usuario, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(t => t.Pessoa == usuario).ToListAsync(cancellationToken);
        }

        public async Task<bool> RemoverAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await _collection.DeleteOneAsync(t => t.Id == id, cancellationToken);
            return result.DeletedCount > 0;
        }
    }
}
