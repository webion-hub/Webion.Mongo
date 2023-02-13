namespace Webion.Mongo.Extensions;

public static class MongoIndexManagerExtension
{
    public static void CreateIfNotExists<T>(this IMongoIndexManager<T> index,
        CreateIndexModel<T> model
    )
    {
        var indexes = index.List().ToList();
        var exists = indexes.Any(i => i.Names.Contains(model.Options.Name));
        if (exists)
            return;

        index.CreateOneAsync(model);
    }
}