using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUi
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart cart;
        Customer customer;
        CashDesk cashDesk;
        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(), db)
            {
                IsModel = false
            };
            cart = new Cart(customer);
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products, db);
            catalogProduct.ActionWithItem += UpdateProductList;
            catalogProduct.Show();
        }

        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers, db);
            catalogSeller.Show();
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers, db);
            catalogCustomer.Show();
        }

        private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCheck = new Catalog<Check>(db.Checks, db);
            catalogCheck.Show();
        }

        /// <summary>
        /// Добавление новых покупателей и т.д. производится с помощью DialogResult
        /// Он становится OK, после нажатия на кнопку в соответсвующих окнах CustomerForm и т.п.
        /// </summary>
        private void CustomerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }

        private void SellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }
        }

        private void ProductAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
                UpdateProductList();
            }
        }

        private void modelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelingForm();
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Task.Run(() => 
            {
                listBox1.Invoke((Action) delegate             
                {
                    UpdateProductList();
                    UpdateCartList();
                });
            });
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem is Product product)
            {
                cart.Add(product);
                listBox2.Items.Add(product);
                UpdateCartList();
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is Product product)
            {
                cart.Remove(product);
                listBox2.Items.Remove(product);
                UpdateCartList();
            }
        }

        private void UpdateProductList()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(db.Products.ToArray());
        }

        private void UpdateCartList()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(cart.GetAll().ToArray());
            LabelCartPrice.Text = "Итого: " + cart.Price;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new LoginForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var tempCustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if ( tempCustomer != null )
                {
                    customer = tempCustomer;
                }
                else
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    customer = form.Customer;
                }
            }

            linkLabel1.Text = $"Здравствуй, {customer.Name}";
            cart.Customer = customer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (customer != null)
            {
                if ( listBox2.Items.Count == 0 )
                {
                    MessageBox.Show($"Корзина пуста!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cashDesk.Enqueue(cart);
                    var price = cashDesk.Dequeue();
                    listBox2.Items.Clear();
                    cart = new Cart(customer);
                    MessageBox.Show($"Покупка на сумму {price} выполнена успешно.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, пожалуйста.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }        
    }
}
