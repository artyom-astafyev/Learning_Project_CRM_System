using System;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUi
{
    public partial class ProductForm : Form
    {
        public Product Product { get; set; }
        public ProductForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор для редактирования существующей формы.
        /// </summary>
        public ProductForm(Product product) : this()
        {
            Product = product ?? new Product();
            NameTextBox.Text = Product.Name;
            PriceNumericUpDown.Value = Product.Price;
            CountNumericUpDown.Value = Product.Count;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // Если продукт равен null, то выполняется добавление нового,
            // иначе редактируется существующий.
            Product = Product ?? new Product();
            Product.Name = NameTextBox.Text;
            Product.Price = PriceNumericUpDown.Value;
            Product.Count = Convert.ToInt32(CountNumericUpDown.Value);
            Close();
        }
    }
}
