namespace DtxFramework.Banking.Data.Context
{
	public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public DatabaseContext
			(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options: options)
		{
		}

		public Microsoft.EntityFrameworkCore.DbSet<Domain.Models.Account> Accounts { get; set; }
	}
}
