using System;
using System.Collections.Generic;

namespace CrmBL.Model
{
    /// <summary>
    /// Касса.
    /// Обеспечивает продажи, очередь.
    /// </summary>
    public class CashDesk
    {
        CrmContext db;
        public event EventHandler<Check> CheckClosed;

        /// <summary>
        /// Номер кассы.
        /// </summary>
        public int Number { get; set; }
        public Seller Seller { get; set; }

        /// <summary>
        /// В очереди ориентируемся на корзины, т.к. Cart хранит Customer
        /// и каждый покупатель имеет только одну корзину.
        /// </summary>
        public Queue<Cart> Queue { get; set; }
        public int MaxQueueLenght { get; set; }

        /// <summary>
        /// Подсчет кол-ва людей, которые ушли не попав в очередь.
        /// </summary>
        public int ExitCustomer { get; set; }
        public int Count => Queue.Count; 
        public bool IsModel { get; set; }
        public decimal Price { get; set; }

        public CashDesk(int number, Seller seller, CrmContext db)
        {
            this.db = db ?? new CrmContext();
            Seller = seller;
            Number = number;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLenght = 10;
        }

        public void Enqueue(Cart cart)
        {
            if (Queue.Count < MaxQueueLenght)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        public decimal Dequeue()
        {            
            decimal sum = 0;
            if(Queue.Count == 0)
            {
                return 0;
            }

            var cart = Queue.Dequeue();
            if(cart != null)
            {
                var check = new Check
                {
                    SellerId = Seller.SellerId,
                    Seller = Seller,
                    CustomerId = cart.Customer.CustomerId,
                    Customer = cart.Customer,
                    Created = DateTime.Now
                };

                if (!IsModel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.CheckId = 0;
                }

                var sells = new List<Sell>();

                foreach (Product product in cart)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };
                        sells.Add(sell);

                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                        }

                        product.Count--;
                        sum += product.Price;
                    }
                }

                check.Price = sum;

                if (!IsModel)
                {
                    db.SaveChanges();
                }

                CheckClosed?.Invoke(this, check);
            }

            return sum;
        }

        public override string ToString()
        {
            return $"Касса №{Number}";
        }
    }
}
