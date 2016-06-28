using System;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
	public class TextReceiptGenerator : IReceiptGenerator
	{
		public string Generate(Order order)
		{
			var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", order.Company, Environment.NewLine));

			if (order.Lines.Any())
				order.Lines.ToList().ForEach(line => result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, order.CalculateLineTotal(line).ToString("C"))));

			result.AppendLine(string.Format("Sub-Total: {0}", order.CalculateSubTotal().ToString("C")));
			result.AppendLine(string.Format("Tax: {0}", order.CalculateTax().ToString("C")));
			result.Append(string.Format("Total: {0}", order.CalculateTotal().ToString("C")));

			return result.ToString();
		}
	}
}
