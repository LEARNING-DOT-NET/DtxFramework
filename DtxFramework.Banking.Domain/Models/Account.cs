namespace DtxFramework.Banking.Domain.Models
{
	public class Account : object
	{
		public Account() : base()
		{
		}

		public int Id { get; set; }

		public string Type { get; set; }

		public decimal Balance { get; set; }
	}
}
