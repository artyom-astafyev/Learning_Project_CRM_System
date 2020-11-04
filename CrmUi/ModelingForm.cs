using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ModelingForm : Form
    {
        ShopComputerModel model = new ShopComputerModel();
        public ModelingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cashDesks = new List<CashDeskView>();
            for (int i = 0; i < model.CashDesks.Count; i ++)
            {
                var cashDesk = new CashDeskView(model.CashDesks[i], i, 10, 26 * i);
                cashDesks.Add(cashDesk);
                Controls.Add(cashDesk.CashDeskName);
                Controls.Add(cashDesk.LeaveCustomersCount);
                Controls.Add(cashDesk.Price);
                Controls.Add(cashDesk.QueueLenght);
            }

            model.Start();
        }

        private void ModelingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.Stop();
        }

        private void ModelingForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = model.CustomerSpeed;
            numericUpDown2.Value = model.CashDeskSpeed;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.CustomerSpeed = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.CashDeskSpeed = (int)numericUpDown2.Value;
        }

    }
}
