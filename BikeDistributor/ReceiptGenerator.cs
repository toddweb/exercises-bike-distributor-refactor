using System;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
	public class ReceiptGenerator
	{
		public string Generate(Order order, ReceiptFormats format = ReceiptFormats.Text)
		{
			var receipt = string.Empty;

			switch (format)
			{
				case ReceiptFormats.Html:
					receipt = HtmlReceipt(order);
					break;
				case ReceiptFormats.Xml:
					receipt = XmlReceipt(order);
					break;
				default:
					receipt = Receipt(order);
					break;
			}

			return receipt;
		}

		private string Receipt(Order order)
		{
			var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", order.Company, Environment.NewLine));

			if (order.Lines.Any())
				order.Lines.ToList().ForEach(line => result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, order.CalculateLineTotal(line).ToString("C"))));

			result.AppendLine(string.Format("Sub-Total: {0}", order.CalculateSubTotal().ToString("C")));
			result.AppendLine(string.Format("Tax: {0}", order.CalculateTax().ToString("C")));
			result.Append(string.Format("Total: {0}", order.CalculateTotal().ToString("C")));

			return result.ToString();
		}

		private string HtmlReceipt(Order order)
		{
			var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", order.Company));

			if (order.Lines.Any())
			{
				result.Append("<ul>");
				order.Lines.ToList().ForEach(line => result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Bike.Brand, line.Bike.Model, order.CalculateLineTotal(line).ToString("C"))));
				result.Append("</ul>");
			}

			result.Append(string.Format("<h3>Sub-Total: {0}</h3>", order.CalculateSubTotal().ToString("C")));
			result.Append(string.Format("<h3>Tax: {0}</h3>", order.CalculateTax().ToString("C")));
			result.Append(string.Format("<h2>Total: {0}</h2>", order.CalculateTotal().ToString("C")));
			result.Append("</body></html>");

			return result.ToString();
		}

		private string XmlReceipt(Order order)
		{
			var result = new StringBuilder(string.Format("<Order><Company><![CDATA[{0}]]></Company>", order.Company));

			if (order.Lines.Any())
			{
				result.Append("<Lines>");
				order.Lines.ToList().ForEach(line => result.Append(string.Format("<Line><Quantity>{0}</Quantity><Brand><![CDATA[{1}]]></Brand><Model><![CDATA[{2}]]></Model><Total>{3}</Total></Line>", line.Quantity, line.Bike.Brand, line.Bike.Model, order.CalculateLineTotal(line).ToString("C"))));
				result.Append("</Lines>");
			}

			result.Append(string.Format("<SubTotal>{0}</SubTotal>", order.CalculateSubTotal().ToString("C")));
			result.Append(string.Format("<Tax>{0}</Tax>", order.CalculateTax().ToString("C")));
			result.Append(string.Format("<Total>{0}</Total>", order.CalculateTotal().ToString("C")));
			result.Append("</Order>");

			return result.ToString();
		}
	}
}
