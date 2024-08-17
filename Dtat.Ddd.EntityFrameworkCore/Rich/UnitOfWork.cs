using Microsoft.EntityFrameworkCore;

namespace Dtat.Ddd.EntityFrameworkCore.Rich
{
    ///*
    public abstract class UnitOfWork<TContext> :
        object, IUnitOfWork where TContext : DbContext
    {
        public UnitOfWork(TContext databaseContext) : base()
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

        public async System.Threading.Tasks.Task<int> SaveAsync()
        {
            int result =
                await DatabaseContext.SaveChangesAsync();

            return result;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
    //*/

    /*
	public abstract class UnitOfWork<TContext> :
		object, IUnitOfWork where TContext : Microsoft.EntityFrameworkCore.DbContext
	{
        //public UnitOfWork(TContext databaseContext) : base()
        //{
        //    //DatabaseContext = databaseContext;
        //}
        //public UnitOfWork(TContext databaseContext, Options options) : base()
        //{
        //    Options = options;
        //}
        public UnitOfWork(Options options) : base()
        {
            Options = options;
        }

        // **********
        protected Options Options { get; set; }
		// **********

		// **********
		private TContext _databaseContext;
		// **********

		// **********
		protected TContext DatabaseContext
		{
			get
			{
				if (_databaseContext == null)
				{
					var optionsBuilder =
						new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<TContext>();

					switch (Options.Provider)
					{
						case Enums.Provider.SqlServer:
							{
								optionsBuilder.UseSqlServer
									(connectionString: Options.ConnectionString);

								break;
							}

						case Enums.Provider.MySql:
							{
								//optionsBuilder.UseMySql
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Enums.Provider.Oracle:
							{
								//optionsBuilder.UseOracle
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Enums.Provider.PostgreSQL:
							{
								//optionsBuilder.UsePostgreSQL
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Enums.Provider.InMemory:
							{
								optionsBuilder.UseInMemoryDatabase
									(databaseName: Options.InMemoryDatabaseName);

								break;
							}

						default:
							{
								break;
							}
					}

					_databaseContext =
						(TContext)System.Activator.CreateInstance
						(type: typeof(TContext), args: optionsBuilder.Options);
				}

				return _databaseContext;
			}
		}
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

		~UnitOfWork()
		{
			Dispose(false);
		}

		public async System.Threading.Tasks.Task<int> SaveAsync()
		{
            int result = await DatabaseContext.SaveChangesAsync();

            return result;
		}
	}
	//*/
}
