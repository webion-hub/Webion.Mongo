using System.Linq.Expressions;
using MongoDB.Bson;

namespace Webion.Mongo.Extensions;

public static class AggregateFluentExtension
{
    public static IAggregateFluent<T> MatchIf<T>(this IAggregateFluent<T> @this,
        bool condition,
        Expression<Func<T, bool>> filter
    )
    {
        return condition
            ? @this.Match(filter)
            : @this;
    }


    public static IAggregateFluent<T> SortByTextScore<T>(this IAggregateFluent<T> @this)
    {
        return @this
            .AddFields(new BsonDocument
            {
                ["score"] = new BsonDocument
                {
                    ["$meta"] = "textScore",
                }
            })
            .Sort(new BsonDocument("score", -1))
            .Unset("score");
    }


    public static IAggregateFluent<T> AddFields<T>(this IAggregateFluent<T> @this,
        BsonDocument fields
    )
    {
        return @this.AppendStage<T>(new BsonDocument
        {
            ["$addFields"] = fields,
        });
    }

    public static IAggregateFluent<T> Unset<T>(this IAggregateFluent<T> @this,
        params string[] fields
    )
    {
        return @this.AppendStage<T>(new BsonDocument
        {
            ["$unset"] = new BsonArray(fields),
        });
    }
}