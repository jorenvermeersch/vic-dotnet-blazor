using Bogus;

namespace BogusStore.Fakers.Common;

/// <summary>
/// Base clase to create <see cref="Entity"/> fakers.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EntityFaker<TEntity> : Faker<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Default constructor to generate an Id and set locale to 'NL' as default.
    /// </summary>
    /// <param name="locale"></param>
    protected EntityFaker(string locale = "nl", int Id = 1) : base(locale)
    {
        int id = Id;
        RuleFor(x => x.Id, f => id++);
    }

    /// <summary>
    /// Builder method to reset the Id as the default so the (relational) database can generate one.
    /// </summary>
    /// <returns></returns>
    public EntityFaker<TEntity> AsTransient()
    {
        RuleFor(x => x.Id, f => default);
        return this;
    }
}