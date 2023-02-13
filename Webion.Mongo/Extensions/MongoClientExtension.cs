using Webion.Mongo.Abstractions;

namespace Webion.Mongo.Extensions;

public static class MongoClientExtension
{
    public static void Configure<TClient, TConfig>(this TClient client)
        where TClient : MongoClient
        where TConfig : IMongoClientConfig<TClient>, new()
    {
        new TConfig().Configure(client);
    }
}