using System;

namespace BikeDistributor.Discounts
{
	public class LineDiscount : Discount
	{
		public LineDiscount(string code, double percentage, int quantityThreshold, int priceThreshold) : base(code, percentage)
		{
			if (quantityThreshold <= 0) throw new ArgumentException("Value must be greater than 0", "quantityThreshold");
			if (priceThreshold <= 0) throw new ArgumentException("Value must be greater than 0", "priceThreshold");

			QuantityThreshold = quantityThreshold;
			PriceTheshold = priceThreshold;
		}

		public int PriceTheshold { get; private set; }

		public int QuantityThreshold { get; private set; }

		public override double Apply<IDiscountableItem>(IDiscountableItem discountableItem)
		{
			var line = discountableItem as Line;

			return line == null ? 0 : line.CalculateTotal() * Percentage;
		}

		public override bool IsApplicable<IDiscountableItem>(IDiscountableItem discountableItem)
		{
			var line = discountableItem as Line;

			return line != null && line.Bike.Price == PriceTheshold && line.Quantity >= QuantityThreshold;
		}
	}
}
