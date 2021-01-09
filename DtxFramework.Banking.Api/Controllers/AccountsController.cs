namespace DtxFramework.Banking.Api.Controllers
{
	[Microsoft.AspNetCore.Mvc.ApiController]
	[Microsoft.AspNetCore.Mvc.Route(template: "api/[controller]")]
	public class AccountsController : Microsoft.AspNetCore.Mvc.Controller
	{
		public AccountsController
			(Application.Interfaces.IAccountService accountService)
		{
			AccountService = accountService;
		}

		protected Application.Interfaces.IAccountService AccountService { get; }

		[Microsoft.AspNetCore.Mvc.HttpGet]
		public Microsoft.AspNetCore.Mvc.ActionResult
			<System.Collections.Generic.IEnumerable<Domain.Models.Account>>
			Get()
		{
			var result =
				AccountService.GetAll();

			return Ok(value: result);
		}
	}
}
