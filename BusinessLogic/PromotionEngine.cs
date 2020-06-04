using Microsoft.Extensions.Logging;
using System;

namespace PromotionEngineAPI
{
    public class PromotionEngine
    {     
        public decimal GetBestDiscountedPrice(Cart cart, ILogger<PromotionEngineController> logger)
        {
            var bestPrice = Decimal.MaxValue;
            var bestOffering = "";
            Promotions.ActivePromotions.ForEach(promotionOffered =>
            {
                var currentPromotionPrice = promotionOffered.Invoke(cart);
                if (currentPromotionPrice < bestPrice)
                {
                    bestPrice = currentPromotionPrice;
                    bestOffering = promotionOffered.Method.Name; // Check which active offering was applied for my sake.
                }
            });
            logger.LogInformation(bestOffering);
            return bestPrice;
        }
    }
}
