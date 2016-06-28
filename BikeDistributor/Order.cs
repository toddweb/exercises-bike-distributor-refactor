using BikeDistributor.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeDistributor
{
	public class Order : IDiscountableItem
	{
		private readonly IList<Discount> _discounts = new List<Discount>();

        private readonly IList<Line> _lines = new List<Line>();

		private double _taxRate = 1;

        public Order(string company)
        {
            Company = company;
		}

        public string Company { get; private set; }

		public IList<Discount> Discounts { get { return _discounts; } }

		public IList<Line> Lines { get { return _lines; } }

		public void AddLine(Line line)
        {
            _lines.Add(line);
        }
		
		public void ApplyDiscount(Discount discount)
		{
			if (_discounts.Any(x => x.Code.Equals(discount.Code, System.StringComparison.InvariantCultureIgnoreCase))) return;

			_discounts.Add(discount);
		}

		public void ApplyTaxRate(double taxRate)
		{
			if (taxRate <= 0) throw new ArgumentException("Value must be greater than 0", "taxRate");

			_taxRate = taxRate;
		}

		public double CalculateLineTotal(Line line)
		{
			var largestDiscount = _discounts
				.OfType<LineDiscount>()
				.Where(x => x.IsApplicable(line))
				.OrderByDescending(x => x.Percentage)
				.FirstOrDefault();

			return largestDiscount != null
				? largestDiscount.Apply(line)
				: line.CalculateTotal();
		}

		public double CalculateSubTotal()
		{
			return _lines.Sum(x => CalculateLineTotal(x));
		}

		public double CalculateTotal()
		{
			var largestDiscount = _discounts
				.OfType<OrderDiscount>()
				.Where(x => x.IsApplicable(this))
				.OrderByDescending(x => x.Percentage)
				.FirstOrDefault();

			var subTotal = largestDiscount != null
				? largestDiscount.Apply(this)
				: CalculateSubTotal();

			return subTotal + CalculateTax();
		}

		public double CalculateTax()
		{
			return CalculateSubTotal() * _taxRate;
		}
	}
}
