using System;
using System.Collections.Generic;
using System.Data.Entity;
using EZ.Data.Contracts;

namespace EZ.Data
{
    /// <summary>
    /// A maker of EZ Repositories
    /// </summary>
    /// <remarks>
    /// An instance of this class contains repository factory functions for different types.
    /// Each factory function takes an EZ <see cref="DbContext"/> and returns a repository bound to that DbContext
    /// <para>
    /// Designed to be a "Singleton", configured at web application start with all of the factory functions,
    /// needed to create any type of repository.
    /// Should be thread-safe to use because it is configured at app start,
    /// before any request for a factory, and should be immutable thereafter
    /// </para>
    /// </remarks>
    public class RepositoryFactories
    {
        /// <summary>
        /// Get the dictionary of repository factory functions
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A dictionary value is a repository factory function that takes a <see cref="DbContext"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;
        
        /// <summary>
        /// Return the runtime EZ repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM EZ FACTORY FUNCTIONS
        /// </remarks>
        private IDictionary<Type, Func<DbContext, object>> GetEZFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
            {
                {typeof(IAddressRepository), dbContext => new AddressRepository(dbContext)},
                {typeof(ICategoryRepository), dbContext => new CategoryRepository(dbContext)},
                {typeof(IEventerRepository), dbContext => new EventerRepository(dbContext)},
                {typeof(IEzRepository), dbContext => new EzRepository(dbContext)},
                {typeof(IPersonAddressRepository), dbContext => new PersonAddressRepository(dbContext)},
                {typeof(IPersonRepository), dbContext => new PersonRepository(dbContext)}                
            };
        }

        /// <summary>
        /// Constructor that initializes with runtime EZ repository factories
        /// </summary>
        public RepositoryFactories()
        {
            _repositoryFactories = GetEZFactories();
        }

        /// <summary>
        /// Constructor that initializes with an arbitrary collection of factories
        /// </summary>
        /// <param name="factories">
        /// The repository factory functions for this instance
        /// </param>
        /// <remarks>
        /// This ctor is primarily useful for testing this class
        /// </remarks>
        public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        /// <summary>
        /// Get the repository factory function for the type
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type
        /// but could be any type(e.g. an entity type)
        /// </remarks>
        public Func<DbContext,object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="T">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EZ <see cref="DbContext"/>
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="_repositoryFactories"/>.
        /// If not, falls back to the <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// You can substitue an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="_repositoryFactories"/>.
        /// </remarks>
        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EZRepository<T>(dbContext);
        }
    }
}
