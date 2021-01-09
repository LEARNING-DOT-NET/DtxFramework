namespace DtxFramework.Banking.Application.Interfaces
{
	public interface IAccountService
	{
		System.Collections.Generic.IEnumerable<Domain.Models.Account> GetAll();

		System.Threading.Tasks.Task
			<System.Collections.Generic.IEnumerable<Domain.Models.Account>> GetAllAsync();
	}
}
