using System;

namespace BikeDistributor.Discounts
{
	public abstract class Discount
	{
		public Discount(string code, double percentage)
		{
			if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Required parameter missing", "code");
			if (percentage <= 0) throw new ArgumentException("Value must be greater than 0", "percentage");

			Code = code;
			Percentage = percentage;
		}

		public string Code { get; private set; }

		public double Percentage { get; private set; }

		public abstract bool IsApplicable<T>(T discountableItem) where T : IDiscountableItem;

		public abstract double Apply<T>(T discountableItem) where T : IDiscountableItem;
	}
}
