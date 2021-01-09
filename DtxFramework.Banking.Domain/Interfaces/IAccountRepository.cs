namespace DtxFramework.Banking.Domain.Interfaces
{
	public interface IAccountRepository
	{
		System.Collections.Generic.IEnumerable<Models.Account> GetAll();

		System.Threading.Tasks.Task
			<System.Collections.Generic.IEnumerable<Models.Account>> GetAllAsync();
	}
}
