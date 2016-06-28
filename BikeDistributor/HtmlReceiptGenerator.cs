using System.Linq;
using System.Text;

namespace BikeDistributor
{
	public class HtmlReceiptGenerator : IReceiptGenerator
	{
		public string Generate(Order order)
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
	}
}
