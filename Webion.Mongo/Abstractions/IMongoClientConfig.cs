namespace Webion.Mongo.Abstractions;

public interface IMongoClientConfig<TClient>
    where TClient : MongoClient
{
    void Configure(TClient client);
}