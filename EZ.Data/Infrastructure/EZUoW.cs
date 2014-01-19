using EZ.Data.Contracts;
using EZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data
{
    /// <summary>
    /// The EZ "Unit Of Work"
    ///     1) decouple the repos from the controllers
    ///     2) decouples the DbContext and EZ from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular root entity type
    /// such as <see cref="Persons"/>. A repository typically exposes "Get" methods
    /// if those features are supported. The repositories rely on their parent UoW
    /// to provide the interface to the data layer (which is the EZ DbContext in EZ).
    /// </remarks>
    public class EZUoW : IUoW, IDisposable
    {
        //private readonly IDatabaseFactory databaseFactory;
        private EZContext DbContext { get; set; }
        protected IRepositoryProvider RepositoryProvider { get; set; }

        public EZUoW(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        // EZ repositories
        //public IRepository<Ez> Ezs { get { return GetStandardRepo<Ez>(); } }
        public IEzRepository Ezs { get { return GetRepo<IEzRepository>(); } }
        //public IRepository<Persons> Persons { get { return GetStandardRepo<Persons>(); } }
        public IPersonRepository Persons { get { return GetRepo<IPersonRepository>(); } }
        //public IRepository<Address> Addresses { get { return GetStandardRepo<Address>(); } }
        public IAddressRepository Addresses { get { return GetRepo<IAddressRepository>(); } }
        public ICategoryRepository Categories { get { return GetRepo<ICategoryRepository>(); } }
        public IEventerRepository Eventers { get { return GetRepo<IEventerRepository>(); } }
        public IPersonAddressRepository PersonAddresses { get { return GetRepo<IPersonAddressRepository>(); } }

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new EZContext();

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EZ to do so.
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We con't use this performance tweak because we don't need the extra performance and,
            //when autodetect is false, we'd have to be careful. We're not being that careful.
        }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
