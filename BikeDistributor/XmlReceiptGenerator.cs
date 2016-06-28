using System.Linq;
using System.Text;

namespace BikeDistributor
{
	public class XmlReceiptGenerator : IReceiptGenerator
	{
		public string Generate(Order order)
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
