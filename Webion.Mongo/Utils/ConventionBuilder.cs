using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Webion.Mongo.Utils;

public sealed class ConventionBuilder
{
    private readonly ConventionPack _conventionPack = new();
    private readonly string _name;
    private Predicate<Type> _filter = (t) => true;


    public static ConventionBuilder Create(string name)
    {
        return new ConventionBuilder(name);
    }

    private ConventionBuilder(string name)
    {
        _name = name;
    }


    public ConventionBuilder UseFilter(Predicate<Type> filter)
    {
        _filter = filter;
        return this;
    }

    public ConventionBuilder UseCamelCase()
    {
        _conventionPack.Add(new CamelCaseElementNameConvention());
        return this;
    }
    
    public ConventionBuilder UseStringEnums()
    {
        _conventionPack.Add(new EnumRepresentationConvention(BsonType.String));
        return this;
    }

    public void Register()
    {
        ConventionRegistry.Register(
            name: _name,
            conventions: _conventionPack,
            filter: t => _filter(t)
        );
    }
}