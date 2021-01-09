using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DtxFramework.Banking.Data.Repositories
{
	public class AccountRepository : Domain.Interfaces.IAccountRepository
	{
		public AccountRepository
			(Context.DatabaseContext databaseContext) : base()
		{
			DatabaseContext = databaseContext;
		}

		protected Context.DatabaseContext DatabaseContext { get; }

		public System.Collections.Generic.IEnumerable<Domain.Models.Account> GetAll()
		{
			var result =
				DatabaseContext.Accounts
				.ToList()
				;

			return result;
		}

		public async System.Threading.Tasks.Task
			<System.Collections.Generic.IEnumerable<Domain.Models.Account>> GetAllAsync()
		{
			// ToListAsync -> using Microsoft.EntityFrameworkCore;

			var result =
				await
				DatabaseContext.Accounts
				.ToListAsync()
				;

			return result;
		}
	}
}
