using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BikeDistributor.Discounts;

namespace BikeDistributor.Test.Promotions
{
	[TestClass]
	public class LineDiscountTests
	{
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenNullCode_WhenDiscountIsCreated_ThenExceptionIsThrown()
		{
			var sut = new LineDiscount(null, 0, 0, 0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenEmptyCode_WhenDiscountIsCreated_ThenExceptionIsThrown()
		{
			var sut = new LineDiscount(string.Empty, 0, 0, 0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenInvalidPercentage_WhenDiscountIsCreated_ThenExceptionIsThrown()
		{
			var sut = new LineDiscount("d", 0, 0, 0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenInvalidQuantityThreshold_WhenLineDiscountIsCreated_ThenExceptionIsThrown()
		{
			var sut = new LineDiscount("d", 1, 0, 1);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenInvalidPriceThreshold_WhenLineDiscountIsCreated_ThenExceptionIsThrown()
		{
			var sut = new LineDiscount("d", 1, 1, 0);
		}

		[TestMethod]
		public void GivenValidArguments_WhenLineDiscountIsCreated_ThenAllIsWell()
		{
			Assert.IsNotNull(new LineDiscount("d", 1, 1, 1));
		}

		[TestMethod]
		public void GivenALineDiscount_WhenAppliedToLine_ThenShouldReturnLineTotalTimesPercentage()
		{
			var sut = new LineDiscount("d", 1, 1, 1);
			var line = new Line(new Bike(string.Empty, string.Empty, 100), 2);

			Assert.AreEqual(line.CalculateTotal() * sut.Percentage, sut.Apply(line));
		}

		[TestMethod]
		public void GivenALineDiscount_WhenAppliedToNonLine_ThenShouldReturnZero()
		{
			var sut = new LineDiscount("d", 1, 1, 1);
			var order = new Order(string.Empty);

			Assert.AreEqual(0, sut.Apply(order));
		}

		[TestMethod]
		public void GivenALineDiscount_WhenIsApplicableOnLine_ThenShouldReturnTrue()
		{
			const int PriceThreshold = 100;
			const int QuantityThreshold = 2;

			var sut = new LineDiscount("d", 1, QuantityThreshold, PriceThreshold);
			var line = new Line(new Bike(string.Empty, string.Empty, PriceThreshold), QuantityThreshold + 1);

			Assert.IsTrue(sut.IsApplicable(line));
		}

		[TestMethod]
		public void GivenALineDiscount_WhenIsApplicableOnNonLine_ThenShouldReturnFalse()
		{
			var sut = new LineDiscount("d", 1, 1, 1);
			var order = new Order(string.Empty);

			Assert.IsFalse(sut.IsApplicable(order));
		}
	}
}
