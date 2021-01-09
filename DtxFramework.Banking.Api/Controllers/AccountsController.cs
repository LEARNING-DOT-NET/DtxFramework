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
		public
			Microsoft.AspNetCore.Mvc.ActionResult
			<System.Collections.Generic.IEnumerable<Domain.Models.Account>>
			Get()
		{
			var result =
				AccountService.GetAll();

			return Ok(value: result);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet(template: "GetAll")]
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK,
			type: typeof(System.Collections.Generic.IEnumerable<Domain.Models.Account>))]
		public
			async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<System.Collections.Generic.IEnumerable<Domain.Models.Account>>>
			GetAsync()
		{
			var result =
				await AccountService.GetAllAsync();

			return Ok(value: result);
		}
	}
}
