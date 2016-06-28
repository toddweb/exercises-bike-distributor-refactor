namespace BikeDistributor.Discounts
{
	public class OrderDiscount : Discount
	{
		public OrderDiscount(string code, double percentage) : base(code, percentage)
		{
		}

		public override double Apply<T>(T discountableItem)
		{
			var order = discountableItem as Order;

			return order != null ? order.CalculateSubTotal() * Percentage : 0;
		}

		public override bool IsApplicable<T>(T discountableItem)
		{
			return discountableItem is Order;
		}
	}
}
