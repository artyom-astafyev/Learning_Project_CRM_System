using System;
using System.Data.Entity;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUi
{
    public partial class Catalog<T> : Form
        where T: class
    {
        CrmContext db;
        DbSet<T> Set;
        public event Action ActionWithItem;
        public Catalog(DbSet<T> set, CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            Set = set;
            Set.Load();
            dataGridView.DataSource = Set.Local.ToBindingList();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if(typeof(T) == typeof(Product))
            {
                var form = new ProductForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Products.Add(form.Product);
                    db.SaveChanges();
                    ActionWithItem?.Invoke();
                }
            }
            else if (typeof(T) == typeof(Customer))
            {
                var form = new CustomerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    ActionWithItem?.Invoke();
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                var form = new SellerForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.Sellers.Add(form.Seller);
                    db.SaveChanges();
                    ActionWithItem?.Invoke();
                }
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;
            if (typeof(T) == typeof(Product))
            {
                // Выделенная строка находится в БД, затем создается соответствующая
                // форма на основе данных текущего объекта, после редактирования
                // данные обновляются.
                var product = Set.Find(id) as Product;
                if(product != null)
                {
                    var form = new ProductForm(product);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        product = form.Product;
                        db.SaveChanges();
                        dataGridView.Update();
                        ActionWithItem?.Invoke();
                    }
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                // Выделенная строка находится в БД, затем создается соответствующая
                // форма на основе данных текущего объекта, после редактирования
                // данные обновляются.
                var seller = Set.Find(id) as Seller;
                if (seller != null)
                {
                    var form = new SellerForm(seller);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        seller = form.Seller;
                        db.SaveChanges();
                        dataGridView.Update();
                        ActionWithItem?.Invoke();
                    }
                }
            }
            else if (typeof(T) == typeof(Customer))
            {
                // Выделенная строка находится в БД, затем создается соответствующая
                // форма на основе данных текущего объекта, после редактирования
                // данные обновляются.
                var customer = Set.Find(id) as Customer;
                if (customer != null)
                {
                    var form = new CustomerForm(customer);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        customer = form.Customer;
                        db.SaveChanges();
                        dataGridView.Update();
                        ActionWithItem?.Invoke();
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;
            if (typeof(T) == typeof(Product))
            {
                var product = Set.Find(id) as Product;
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    dataGridView.Update();
                    ActionWithItem?.Invoke();
                }
            }
            else if (typeof(T) == typeof(Customer))
            {
                var customer = Set.Find(id) as Customer;
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    dataGridView.Update();
                    ActionWithItem?.Invoke();
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                var seller = Set.Find(id) as Seller;
                if (seller != null)
                {
                    db.Sellers.Remove(seller);
                    db.SaveChanges();
                    dataGridView.Update();
                    ActionWithItem?.Invoke();
                }
            }
        }
    }
}
