namespace PromotionEngineAPI
{
    public abstract class StockKeepingUnit
    {
        public abstract string Name { get; }
        public abstract decimal Price { get; }
        public int Quantity { get; set; }
    }
}
