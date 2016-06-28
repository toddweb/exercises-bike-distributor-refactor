using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
	[TestClass]
	public class LineTests
	{
		[TestMethod]
		public void GivenLine_WhenCalculateTotal_ThenShouldReturnQuantityTimesPrice()
		{
			const int Price = 1000;
			const int Quantity = 2;

			var sut = new Line(new Bike(string.Empty, string.Empty, Price), Quantity);

			Assert.AreEqual(Price * Quantity, sut.CalculateTotal());
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenNullBike_WhenLineIsCreated_ThenExceptionIsThrown()
		{
			var sut = new Line(null, 1);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void GivenInvalidQuantity_WhenLineIsCreated_ThenExceptionIsThrown()
		{
			var sut = new Line(new Bike(string.Empty, string.Empty, 1), 0);
		}

		[TestMethod]
		public void GivenValidArguments_WhenLineIsCreated_ThenAllIsWell()
		{
			Assert.IsNotNull(new Line(new Bike(string.Empty, string.Empty, 1), 1));
		}

	}
}
