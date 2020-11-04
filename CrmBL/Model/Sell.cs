namespace CrmBL.Model
{
    /// <summary>
    /// Промежуточная таблица для связи многие ко многим: Check и Product.
    /// </summary>
    public class Sell
    {
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public int CheckId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Check Check { get; set; }
    }
}
