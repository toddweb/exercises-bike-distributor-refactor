using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BikeDistributor.Discounts;

namespace BikeDistributor.Test.Promotions
{
	[TestClass]
	public class OrderDiscountTests
	{
		[TestMethod]
		public void GivenAnOrderDiscount_WhenApplyToOrder_ThenShouldReturnOrderSubTotalTimesPercentage()
		{
			var sut = new OrderDiscount("d", 1);
			var order = new Order(string.Empty);

			order.AddLine(new Line(new Bike(string.Empty, string.Empty, 1), 1));

			Assert.AreEqual(order.CalculateSubTotal() * sut.Percentage, sut.Apply(order));
		}

		[TestMethod]
		public void GivenAnOrderDiscount_WhenApplyToNonOrder_ThenShouldReturnZero()
		{
			var sut = new OrderDiscount("d", 1);
			var line = new Line(new Bike(string.Empty, string.Empty, 1), 1);

			Assert.AreEqual(0, sut.Apply(line));
		}

		[TestMethod]
		public void GivenAnOrderDiscount_WhenIsApplicableOnOrder_ThenShouldReturnTrue()
		{
			var sut = new OrderDiscount("d", 1);

			Assert.IsTrue(sut.IsApplicable(new Order(string.Empty)));
		}

		[TestMethod]
		public void GivenAnOrderDiscount_WhenIsApplicableOnNonOrder_ThenShouldReturnFalse()
		{
			var sut = new OrderDiscount("d", 1);

			Assert.IsFalse(sut.IsApplicable(new Line(new Bike(string.Empty, string.Empty, 1), 1)));
		}
	}
}
