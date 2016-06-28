using BikeDistributor.Discounts;
using System;

namespace BikeDistributor
{
    public class Line : IDiscountableItem
	{
        public Line(Bike bike, int quantity)
        {
			if (bike == null) throw new ArgumentException("Must have value", "bike");
			if (quantity <= 0) throw new ArgumentException("Value must be greater than 0", "quantity");

            Bike = bike;
            Quantity = quantity;
        }

        public Bike Bike { get; private set; }

        public int Quantity { get; private set; }

		public int CalculateTotal()
		{
			return Quantity * Bike.Price;
		}
    }
}
