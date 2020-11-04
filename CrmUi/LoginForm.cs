using CrmBL.Model;
using System;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class LoginForm : Form
    {
        public Customer Customer { get; set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer = new Customer
            {
                Name = textBox1.Text
            };
            DialogResult = DialogResult.OK;
        }
    }
}
