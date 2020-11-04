using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            // Arrange
            var customer1 = new Customer
            {
                Name = "testName1",
                CustomerId = 1
            };

            var customer2 = new Customer
            {
                Name = "testName2",
                CustomerId = 2
            };

            var seller = new Seller
            {
                Name = "testSellerName",
                SellerId = 1
            };

            var product1 = new Product
            {
                ProductId = 1,
                Name = "testProduct1",
                Price = 100,
                Count = 10
            };

            var product2 = new Product
            {
                ProductId = 2,
                Name = "testProduct2",
                Price = 80,
                Count = 20
            };

            var cart1 = new Cart(customer1);
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product2);
            var cart2 = new Cart(customer2);
            cart2.Add(product1);
            cart2.Add(product2);
            cart2.Add(product2);
            var cashDesk = new CashDesk(1, seller, null);
            cashDesk.MaxQueueLenght = 10;
            cashDesk.Enqueue(cart1);
            cashDesk.Enqueue(cart2);
            var cart1ExpectedResult = 280;
            var cart2ExpectedResult = 260;

            // Act
            var cart1ActualResult = cashDesk.Dequeue();
            var cart2ActualResult = cashDesk.Dequeue();

            // Assert
            Assert.AreEqual(cart1ExpectedResult, cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ActualResult);
            Assert.AreEqual(7,product1.Count);
            Assert.AreEqual(17, product2.Count);
        }
    }
}