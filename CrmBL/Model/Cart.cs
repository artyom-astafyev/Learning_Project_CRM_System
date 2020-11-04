using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CrmBL.Model
{
    /// <summary>
    /// Корзина покупателя.
    /// </summary>
    public class Cart : IEnumerable
    {
        public Customer Customer { get; set; }

        /// <summary>
        /// Ключ - сам продукт, значение - кол-во товара в корзине.
        /// </summary>
        public Dictionary<Product, int> Products { get; set; }
        public decimal Price => GetAll().Sum(p => p.Price);

        public Cart(Customer customer)
        {
            Customer = customer;
            Products = new Dictionary<Product, int>();
        }

        public void Add(Product product)
        {
            // Если товар существует, то увеличиваем его кол-во,
            // если нет, то добавляем.
            if(Products.TryGetValue(product, out int count))
            {
                Products[product] = ++count;
            }
            else
            {
                Products.Add(product, 1);
            }
        }

        public void Remove(Product product)
        {
            if (Products.TryGetValue(product, out int count))
            {
                if (Products[product] == 1)
                {
                    Products.Remove(product);
                }
                else
                {
                    Products[product] = --count;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach(var product in Products.Keys)
            {
                for(int i = 0; i < Products[product]; i++)
                {
                    yield return product;
                }
            }
        }

        public List<Product> GetAll()
        {
            var result = new List<Product>();
            foreach(Product p in this)
            {
                result.Add(p);
            }
            return result;
        }
    }
}
