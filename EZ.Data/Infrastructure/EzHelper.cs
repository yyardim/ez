using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data
{
    public static class PredicateBuilder
    {
        /// <summary>
        /// Produces a predicate for IQueryable LINQ "Where" clause that queries by id for specific value,
        /// </summary>
        /// <typeparam name="T">The type to query which must have an int property named "Id".</typeparam>
        /// <param name="id">The int value of the id of the desired entity.</param>
        /// <returns>An predicate expression suitable for a LINQ "Where" or "First" clause.</returns>
        /// <remarks>
        /// See <see cref="Model.IRepository{T}.GetById"/> for usage.
        /// </remarks>
        /// <Example>
        /// If T is a Foo and Foo.Id is of type int, then
        /// var predicate = GetByIdPredicate{T}(42) returns the equivalent of 
        /// "f => f.Id == 42" and can be used to get the Foo with Id==42 by writing
        /// aFooDbSet.FirstOrDefault(predicate)".
        /// </Example>
        public static Expression<Func<T, bool>> GetByIdPredicate<T>(int id)
        {
            var itemParam = Expression.Parameter(typeof(T), "item");
            var itemPropertyExpr = Expression.Property(itemParam, "Id");
            var idParam = Expression.Constant(id);
            var newBody = Expression.MakeBinary(ExpressionType.Equal, itemPropertyExpr, idParam);
            var newLambda = Expression.Lambda(newBody, itemParam);
            return newLambda as Expression<Func<T, bool>>;
        }
    }
    public class EzHelper
    {
    }

    public static class LinqExtensions
    {
        /// <summary>
        ///   This method extends the LINQ methods to flatten a collection of
        ///   items that have a property of children of the same type.
        /// </summary>
        /// <typeparam name = "T">Item type.</typeparam>
        /// <param name = "source">Source collection.</param>
        /// <param name = "childPropertySelector">
        ///   Child property selector delegate of each item.
        ///   IEnumerable'T' childPropertySelector(T itemBeingFlattened)
        /// </param>
        /// <returns>Returns a one level list of elements of type T.</returns>
        public static IQueryable<T> Flatten<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> childPropertySelector)
        {
            return source
                .Flatten((itemBeingFlattened, objectsBeingFlattened) =>
                         childPropertySelector(itemBeingFlattened));
        }

        /// <summary>
        ///   This method extends the LINQ methods to flatten a collection of
        ///   items that have a property of children of the same type.
        /// </summary>
        /// <typeparam name = "T">Item type.</typeparam>
        /// <param name = "source">Source collection.</param>
        /// <param name = "childPropertySelector">
        ///   Child property selector delegate of each item.
        ///   IEnumerable'T' childPropertySelector(T itemBeingFlattened,
        ///   IEnumerable'T' objectsBeingFlattened)
        /// </param>
        /// <returns>Returns a one level list of elements of type T.</returns>
        public static IQueryable<T> Flatten<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>, IEnumerable<T>> childPropertySelector)
        {
            return source
                .Concat(source
                            .Where(item => childPropertySelector(item, source) != null)
                            .SelectMany(itemBeingFlattened =>
                                        childPropertySelector(itemBeingFlattened, source)
                                            .Flatten(childPropertySelector)))
                .AsQueryable()
                .Distinct();
        }
    }
}
