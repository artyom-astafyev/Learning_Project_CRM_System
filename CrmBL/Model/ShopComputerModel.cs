using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBL.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random rnd = new Random();
        bool IsWorking = false;
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public Queue<Sell> Sells { get; set; } = new Queue<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();

        public int CustomerSpeed { get; set; } = 100;
        public int CashDeskSpeed { get; set; } = 100;

        public ShopComputerModel()
        {
            var sellers = Generator.GetNewSellers(20);
            Generator.GetNewProducts(1000);
            Generator.GetNewCustomers(100);

            foreach ( var seller in sellers )
            {
                Sellers.Enqueue(seller);
            }

            for ( int i = 0; i < 3; i ++ )
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue(), null));
            }
        }

        public void Start()
        {
            IsWorking = true;
            Task.Run(() => CreateCarts(10));

            // Работа каждой кассы выносится в отдельный поток.
            var CashDesksTasks = CashDesks.Select( c => new Task(() => CashDeskWork(c)));
            foreach ( var task in CashDesksTasks)
            {
                task.Start();
            }
        }       

        public void Stop()
        {
            IsWorking = false;
        }

        /// <param name="sleepTime">
        /// Нужен для замедления потока, 
        /// т.к. вычисления могут производиться слишком быстро. 
        /// </param>
        private void CreateCarts(int customerCount)
        {
            while (IsWorking)
            {
                var customers = Generator.GetNewCustomers(customerCount);
                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);
                    foreach (var product in Generator.GetRandomProducts(10,30))
                    {
                        cart.Add(product);
                    }

                    // Из списка всех касс покупатель выбирает случайную и встает там в очередь.
                    var cash = CashDesks[rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(CustomerSpeed);
            }
        }

        private void CashDeskWork( CashDesk cashDesk)
        {
            while (IsWorking)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(CashDeskSpeed);
                }
            }
        }
    }    
}
