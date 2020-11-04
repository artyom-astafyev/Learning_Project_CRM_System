using System.Data.Entity;

namespace CrmBL.Model
{
    /// <summary>
    /// Класс для подключения к БД и работы с EF.
    /// </summary>
    public class CrmContext: DbContext
    {
        public CrmContext() : base("CrmConnection") { }

        /// <summary>
        /// Набор сущностей, хранящихся в бд.
        /// </summary>
        public DbSet<Check> Checks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Seller> Sellers { get; set; }
    }
}
