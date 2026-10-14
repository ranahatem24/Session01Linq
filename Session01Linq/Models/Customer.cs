namespace C48_Session01LinqG01.Models
{
	public class Customer
	{
		public string CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public Order[] Orders { get; set; }
		public override string ToString() => $"{CustomerID} {Address}\n{City}, {Region} {PostalCode} {Country}\n{Phone}";
	}


}
