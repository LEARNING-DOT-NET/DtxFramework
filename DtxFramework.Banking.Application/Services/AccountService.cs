namespace DtxFramework.Banking.Application.Services
{
	public class AccountService : object, Interfaces.IAccountService
	{
		public AccountService(Domain.Interfaces.IAccountRepository repository) : base()
		{
			Repository = repository;
		}

		protected Domain.Interfaces.IAccountRepository Repository { get; }

		public System.Collections.Generic.IEnumerable<Domain.Models.Account> GetAll()
		{
			var result =
				Repository.GetAll();

			return result;
		}

		public async System.Threading.Tasks.Task
			<System.Collections.Generic.IEnumerable<Domain.Models.Account>> GetAllAsync()
		{
			var result =
				await Repository.GetAllAsync();

			return result;
		}
	}
}
