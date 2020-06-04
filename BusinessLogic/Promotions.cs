using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineAPI
{
    public static class Promotions
    {
        private static decimal BundledOfferPriceFor3ASet = 130;
        private static decimal BundledOfferPriceFor2BSet = 45;
        private static decimal BundledOfferPriceForCDSet = 30;

        //Extendable set of Applicable Promotions
        public static List<Func<Cart,decimal>> ActivePromotions => new List<Func<Cart, decimal>> { PromotionOnQuantityOfSku, PromotionOnSkuCombo };

        private static Func<Cart, decimal> PromotionOnQuantityOfSku => (Cart cart) =>
                    {
                        var totalPriceOnQuantityOffering = 0.0m;
                        cart.OrderedItems.ForEach(sku =>
                        {
                            var bundledOfferForA = 0.0m;
                            var bundledOfferForB = 0.0m;

                            if (sku.Name == "A" && sku.Quantity / 3 > 0)
                            {
                                var perSetA = sku.Quantity / 3;
                                bundledOfferForA = perSetA * BundledOfferPriceFor3ASet;
                                sku.Quantity -= 3 * perSetA;
                            }
                            if (sku.Name == "B" && sku.Quantity / 2 > 0)
                            {
                                var perSetB = sku.Quantity / 2;
                                bundledOfferForB = perSetB * BundledOfferPriceFor2BSet;
                                sku.Quantity -= 2 * perSetB;
                            }
                            var remainingBauPriceForThisSku = sku.Quantity * sku.Price;
                            totalPriceOnQuantityOffering = bundledOfferForA + bundledOfferForB + remainingBauPriceForThisSku;
                            sku.Quantity = 0;
                        });

                        return totalPriceOnQuantityOffering;
                    };

        private static Func<Cart, decimal> PromotionOnSkuCombo => (Cart cart) =>
                {
                    var totalPriceOnComboOffering = 0.0m;

                    var cSku = cart.OrderedItems.Find(x => x.Name == "C");
                    var dSku = cart.OrderedItems.Find(x => x.Name == "D");

                    var quantityOfBundledCDSets = new List<StockKeepingUnit> { cSku, dSku }.Min(x => x.Quantity);
                    totalPriceOnComboOffering += quantityOfBundledCDSets * BundledOfferPriceForCDSet;
                    cSku.Quantity -= quantityOfBundledCDSets;
                    dSku.Quantity -= quantityOfBundledCDSets;

                    cart.OrderedItems.ForEach(sku =>
                    {
                        totalPriceOnComboOffering += sku.Quantity * sku.Price;
                        sku.Quantity = 0;
                    });

                    return totalPriceOnComboOffering;
                };
    }
}
