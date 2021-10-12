using MongoDB.Bson;
using MongoDB.Driver;
using NathalyFestasWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Services
{
    public class PedidosRepository
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<Pedido> pedidosCollection;

        private static PedidosRepository instance;
        public static PedidosRepository Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new PedidosRepository();
                }
                return instance;
            }
        }
        public PedidosRepository()
        {
            mongoClient = new MongoClient("mongodb+srv://mainUser:echelon241@nathalyfestasdb.ldb74.mongodb.net/nathalyfestas?retryWrites=true&w=majority");
            mongoDatabase = mongoClient.GetDatabase("NathalyFestas");
            pedidosCollection = mongoDatabase.GetCollection<Pedido>("Pedidos");
        }

        public Task<Pedido> GetByID(ObjectId id)
        {
            FilterDefinition<Pedido> filter = Builders<Pedido>.Filter.Eq("Id", id);
            return pedidosCollection.Find(filter).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> Insert(Pedido pedido)
        {
            Pedido p = await GetByID(pedido.Id);
            if (p is null)
            {
                pedidosCollection.InsertOne(pedido);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task Replace(Pedido pedido)
        {
            pedido.LastModified = DateTime.UtcNow;
            var findFilter = Builders<Pedido>.Filter.Eq("Id", pedido.Id);
            return pedidosCollection.FindOneAndReplaceAsync(findFilter, pedido);
        }

        public Task<List<Pedido>> GetAll()
        {
            var sortLastModified = Builders<Pedido>.Sort.Descending("LastModified");
            return pedidosCollection.Find(Builders<Pedido>.Filter.Empty).Sort(sortLastModified).ToListAsync();
        }

        public bool Delete(Pedido pedido)
        {
            var findFilter = Builders<Pedido>.Filter.Eq("Id", pedido.Id);
            var deleteResult = pedidosCollection.DeleteOne(findFilter);
            return (deleteResult.DeletedCount > 0) && deleteResult.IsAcknowledged;
        }
    }
}
