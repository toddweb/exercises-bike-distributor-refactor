namespace BikeDistributor
{
	public interface IReceiptGenerator
	{
		string Generate(Order order);
	}
}
