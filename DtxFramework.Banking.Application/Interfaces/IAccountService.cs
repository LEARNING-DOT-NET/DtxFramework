namespace DtxFramework.Banking.Application.Interfaces
{
	public interface IAccountService
	{
		System.Collections.Generic.IEnumerable<Domain.Models.Account> GetAll();
	}
}
