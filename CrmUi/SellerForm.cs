using System;
using System.Windows.Forms;
using CrmBL.Model;

namespace CrmUi
{
    public partial class SellerForm : Form
    {
        public Seller Seller { get; set; }

        public SellerForm()
        {
            InitializeComponent();
        }

        public SellerForm(Seller seller) : this()
        {
            Seller = seller;
            NameTextBox.Text = seller.Name;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if( Seller == null )
                Seller = new Seller();
            Seller.Name = NameTextBox.Text;
            Close();
        }
    }
}
