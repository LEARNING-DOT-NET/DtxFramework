namespace DtxFramework.Banking.Domain.Interfaces
{
	public interface IAccountRepository
	{
		System.Collections.Generic.IEnumerable<Models.Account> GetAll();
	}
}
