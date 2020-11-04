using System.Collections.Generic;

namespace CrmBL.Model
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Связь один ко многим с таблицей Check.
        /// </summary>
        public virtual ICollection<Check> Checks { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
