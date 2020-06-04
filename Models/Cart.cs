using System.Collections.Generic;
using System.Text;

namespace PromotionEngineAPI
{
    public class Cart
    {
        public List<StockKeepingUnit> OrderedItems { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            OrderedItems.ForEach(oi =>
            {
                sb.Append("Name : " + oi.Name);
                sb.Append("Price : " + oi.Price);
                sb.Append("Quantity : " + oi.Quantity);
                sb.Append("-----");
                sb.Append("/n");
            });
            return sb.ToString();
        }
    }
}
