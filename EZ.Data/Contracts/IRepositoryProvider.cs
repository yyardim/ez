using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data
{
    /// <summary>
    /// Interface for a class that can provide repos by type.
    /// The class may create the repos dynamically if it is unable to find one in its cache of repos
    /// </summary>
    /// <remarks>
    /// Repos created by this provider tend to require a <see cref="DbContext"/> to retrieve data
    /// </remarks>
    public interface IRepositoryProvider
    {
        /// <summary>
        /// Get and set the <see cref="DbContext"/> with which to initialize a repository
        /// if one must be created
        /// </summary>
        DbContext DbContext { get; set; }   //TODO: Should I use this or EZContext??

        /// <summary>
        /// Get an <see cref="IRepository{T}"/> for entity type, T.
        /// </summary>
        /// <typeparam name="T">
        /// Root entity type of the <see cref="IRepository{T}"/>
        /// </typeparam>
        /// <returns></returns>
        IRepository<T> GetRepositoryForEntityType<T>() where T : class;

        /// <summary>
        /// Get a repo of type T
        /// </summary>
        /// <typeparam name="T">
        /// Type of the repo, typically a custom repo interface
        /// </typeparam>
        /// <param name="factory">
        /// An optional repo creation function that takes a <see cref="DbContext"/>
        /// and returns a repo of T. Used if the repo must be created.
        /// </param>
        /// <remarks>
        /// Looks for the requested repo in its cache, returning if found.
        /// If not found, tries to make one with the factory, fallingback to a default factory if the factory parameter is null
        /// </remarks>
        T GetRepository<T>(Func<DbContext, object> factory = null) where T : class;

        /// <summary>
        /// Set the repo to return from this provider
        /// </summary>
        /// <remarks>
        /// Set a repo if you don't want this provider to create one.
        /// Useful in testing and when developing without a backend implementation of the object returned by a repo of type T.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        void SetRepository<T>(T repository);
    }
}
