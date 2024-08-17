using Microsoft.EntityFrameworkCore;

namespace Dtat.Ddd.EntityFrameworkCore.Rich
{
    public abstract class UnitOfWorkQuery<TContext> :
        object, IQueryUnitOfWork where TContext : DbContext
    {
        public UnitOfWorkQuery(TContext databaseContext) : base()
        {
            DatabaseContext = databaseContext;
        }

        // **********
        protected TContext DatabaseContext { get; }
        // **********

        // **********
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        public bool IsDisposed { get; protected set; }
        // **********

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).

                if (DatabaseContext != null)
                {
                    DatabaseContext.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            IsDisposed = true;
        }

        ~UnitOfWorkQuery()
        {
            Dispose(false);
        }
    }
}
