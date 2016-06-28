using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
	[TestClass]
	public class ReceiptGeneratorTests
	{
		private const double TaxRate = .0725d;
		
		private readonly static Bike Defy = new Bike("Giant", "Defy 1", Bike.OneThousand);
		private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", Bike.TwoThousand);
		private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand);

		[TestMethod]
		public void ReceiptOneDefy()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(Defy, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(ResultStatementOneDefy, new TextReceiptGenerator().Generate(order));
		}

		private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

		[TestMethod]
		public void ReceiptOneElite()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(Elite, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(ResultStatementOneElite, new TextReceiptGenerator().Generate(order));
		}

		private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

		[TestMethod]
		public void ReceiptOneDuraAce()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(DuraAce, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(ResultStatementOneDuraAce, new TextReceiptGenerator().Generate(order));
		}

		private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

		[TestMethod]
		public void HtmlReceiptOneDefy()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(Defy, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(HtmlResultStatementOneDefy, new HtmlReceiptGenerator().Generate(order));
		}

		private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

		[TestMethod]
		public void HtmlReceiptOneElite()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(Elite, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(HtmlResultStatementOneElite, new HtmlReceiptGenerator().Generate(order));
		}

		private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

		[TestMethod]
		public void HtmlReceiptOneDuraAce()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(DuraAce, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(HtmlResultStatementOneDuraAce, new HtmlReceiptGenerator().Generate(order));
		}

		private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";
		
		[TestMethod]
		public void XmlReceiptOneDefy()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(Defy, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(XmlResultStatementOneDefy, new XmlReceiptGenerator().Generate(order));
		}

		private const string XmlResultStatementOneDefy = @"<Order><Company><![CDATA[Anywhere Bike Shop]]></Company><Lines><Line><Quantity>1</Quantity><Brand><![CDATA[Giant]]></Brand><Model><![CDATA[Defy 1]]></Model><Total>$1,000.00</Total></Line></Lines><SubTotal>$1,000.00</SubTotal><Tax>$72.50</Tax><Total>$1,072.50</Total></Order>";
		
		[TestMethod]
		public void XmlReceiptOneElite()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(Elite, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(XmlResultStatementOneElite, new XmlReceiptGenerator().Generate(order));
		}

		private const string XmlResultStatementOneElite = @"<Order><Company><![CDATA[Anywhere Bike Shop]]></Company><Lines><Line><Quantity>1</Quantity><Brand><![CDATA[Specialized]]></Brand><Model><![CDATA[Venge Elite]]></Model><Total>$2,000.00</Total></Line></Lines><SubTotal>$2,000.00</SubTotal><Tax>$145.00</Tax><Total>$2,145.00</Total></Order>";

		[TestMethod]
		public void XmlReceiptOneDuraAce()
		{
			var order = new Order("Anywhere Bike Shop");
			order.AddLine(new Line(DuraAce, 1));
			order.ApplyTaxRate(TaxRate);
			Assert.AreEqual(XmlResultStatementOneDuraAce, new XmlReceiptGenerator().Generate(order));
		}

		private const string XmlResultStatementOneDuraAce = @"<Order><Company><![CDATA[Anywhere Bike Shop]]></Company><Lines><Line><Quantity>1</Quantity><Brand><![CDATA[Specialized]]></Brand><Model><![CDATA[S-Works Venge Dura-Ace]]></Model><Total>$5,000.00</Total></Line></Lines><SubTotal>$5,000.00</SubTotal><Tax>$362.50</Tax><Total>$5,362.50</Total></Order>";
	}
}
