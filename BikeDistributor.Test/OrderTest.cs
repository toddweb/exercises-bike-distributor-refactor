using BikeDistributor.Discounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
	{
		[TestMethod]
		public void GivenAnEmptyOrder_WhenCalculateTotal_ThenShouldBeZero()
		{
			var sut = new Order(string.Empty);

			Assert.AreEqual(0, sut.CalculateTotal());
		}

		[TestMethod]
        public void GivenAnOrderWithNoDiscounts_WhenCalculateLineTotal_ThenShouldMultiplyQuantityTimesPrice()
		{
			var sut = new Order(string.Empty);
			var line = new Line(new Bike(string.Empty, string.Empty, 250), 1);

			sut.AddLine(line);

			Assert.AreEqual(line.CalculateTotal(), sut.CalculateLineTotal(line));
		}

		[TestMethod]
		public void GivenAnOrderWithNoDiscounts_WhenCalculateSubTotal_ThenShouldSumLines()
		{
			var sut = new Order(string.Empty);

			sut.AddLine(new Line(new Bike(string.Empty, string.Empty, 250), 1));
			sut.AddLine(new Line(new Bike(string.Empty, string.Empty, 450), 3));
			sut.AddLine(new Line(new Bike(string.Empty, string.Empty, 300), 2));

			Assert.AreEqual(sut.Lines.Sum(x => sut.CalculateLineTotal(x)), sut.CalculateSubTotal());
		}

		[TestMethod]
		public void GivenAnOrderWithNoDiscounts_WhenCalculateTax_ThenShouldMultiplySubtotalTimesTaxRate()
		{
			const double TaxRate = 1.0875d;

			var sut = new Order(string.Empty);

			sut.AddLine(new Line(new Bike(string.Empty, string.Empty, 250), 1));
			sut.ApplyTaxRate(TaxRate);

			Assert.AreEqual(sut.CalculateSubTotal() * TaxRate, sut.CalculateTax());
		}

		[TestMethod]
		public void GivenAnOrderWithNoDiscounts_WhenCalculateTotal_ThenShouldAddSubtotalPlusTax()
		{
			const double TaxRate = 1.0875d;

			var sut = new Order(string.Empty);

			sut.AddLine(new Line(new Bike(string.Empty, string.Empty, 250), 1));

			Assert.AreEqual(sut.CalculateSubTotal() + sut.CalculateTax(), sut.CalculateTotal());
		}

		[TestMethod]
		public void GivenAnOrderWithOneLineDiscount_WhenCalculateLineTotal_ThenDiscountIsApplied()
		{
			const double Discount = .8d;
			const int PriceTheshold = 5000;
			const int QuantityThreshold = 5;

			var sut = new Order(string.Empty);
			var bike = new Bike(string.Empty, string.Empty, PriceTheshold);
			var discount = new LineDiscount("FIRSTDISCOUNT", Discount, QuantityThreshold, PriceTheshold);
			var line = new Line(bike, QuantityThreshold + 1);

			sut.AddLine(line);
			sut.ApplyDiscount(discount);

			Assert.AreEqual(discount.Apply(line), sut.CalculateLineTotal(line));
		}

		[TestMethod]
		public void GivenAnOrderWithMultipleLineDiscounts_WhenCalculateLineTotal_ThenProperDiscountIsApplied()
		{
			const int LargestPriceThreshold = Bike.FiveThousand;
			const int LargestQuantityThreshold = 20;

			var sut = new Order(string.Empty);
			var oneThousandDollarsAnd20Bikes = new LineDiscount("ONETHOUSAND_20ORMORE", .9d, LargestQuantityThreshold, Bike.OneThousand);
			var twoThousandDollarsAnd10Bikes = new LineDiscount("TWOTHOUSAND_10ORMORE", .8d, 10, Bike.TwoThousand);
			var fiveThousandDollarsAnd5Bikes = new LineDiscount("FIVETHOUSAND_5ORMORE", .8d, 5, LargestPriceThreshold);
			var line = new Line(new Bike(string.Empty, string.Empty, LargestPriceThreshold), LargestQuantityThreshold + 1);

			sut.AddLine(line);
			sut.ApplyDiscount(oneThousandDollarsAnd20Bikes);
			sut.ApplyDiscount(twoThousandDollarsAnd10Bikes);
			sut.ApplyDiscount(fiveThousandDollarsAnd5Bikes);

			Assert.AreEqual(fiveThousandDollarsAnd5Bikes.Apply(line), sut.CalculateLineTotal(line));
		}

		[TestMethod]
		public void GivenAnOrder_WhenTwoDuplicateDiscountsAreApplied_ThenOnlyOneTakesEffect()
		{
			const string DiscountCode = "DUPLICATE";

			var sut = new Order(string.Empty);

			sut.ApplyDiscount(new LineDiscount(DiscountCode, 1, 1, 1));
			sut.ApplyDiscount(new LineDiscount(DiscountCode, 1, 1, 1));

			Assert.AreEqual(1, sut.Discounts.Count);
		}

		[TestMethod]
		public void GivenAnOrder_WhenValidTaxRateIsApplied_ThenAllIsWell()
		{
			var sut = new Order(string.Empty);

			sut.ApplyTaxRate(1.0875d);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenAnOrder_WhenInValidTaxRateIsApplied_ThenExceptionIsThrown()
		{
			var sut = new Order(string.Empty);

			sut.ApplyTaxRate(-1);
		}
	}
}


