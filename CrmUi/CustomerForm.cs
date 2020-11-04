using System;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUi
{
    public partial class CustomerForm : Form
    {
        public Customer Customer { get; set; }
        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(Customer customer): this()
        {
            Customer = customer;
            NameTextBox.Text = customer.Name;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Customer == null)
                Customer = new Customer();
            Customer.Name = NameTextBox.Text;
            Close();
        }
    }
}
