using iTextSharp.text;
using iTextSharp.text.pdf;
using proWin_Iti.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace proWin_Iti.Order
{
    public partial class BillSallfromResources : Form
    {
        List<Category> c = catigory.clist();
        List<Product> p = Form1.fillproduct();
        List<seller> s = sellers.clist();
        List<BiilBuyPro> bills = new List<BiilBuyPro>();
        public BillSallfromResources()
        {
            InitializeComponent();
        }
        private void BillSallfromResources_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bills;
            foreach (Category ci in c)
            {
                combCAtegory.Items.Add(ci.Name);
            }
            foreach(Product pi in p )
            {
                combProduct.Items.Add(pi.name);
            }
            foreach(seller si in s)
            {
                ComboSuppliers.Items.Add(si.Name);
            }
        }
        private void ComboSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void combProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string proname = combProduct.SelectedItem.ToString();
            foreach(Product pi in p)
            {
                if(pi.name==proname)
                {
                    txtPrice.Text = Convert.ToString(pi.price);
                    txtQountity.Text = Convert.ToString(pi.quantity);
                    combCAtegory.SelectedItem = pi.categoray;
                }
            }
        }
        int counter = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQountity.Text == " " || txtPrice.Text == " " || TxtSum.Text == " ")
                {
                    throw new Exception();
                }
                if (txtQountity.Text == Convert.ToString(0) || TxtSum.Text == Convert.ToString(0)) return;
                if (combCAtegory.SelectedIndex <= -1 || combProduct.SelectedIndex <= -1 || ComboSuppliers.SelectedIndex <= -1) return;

                BiilBuyPro bill = new BiilBuyPro(++counter, Convert.ToDouble(txtPrice.Text), Convert.ToInt32(txtQountity.Text), Convert.ToDouble(TxtSum.Text), combProduct.Text, ComboSuppliers.Text, combCAtegory.Text, dateTimePicker1.Value);
                bills.Add(bill);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = bills;
                foreach (Product pi in p)
                {
                    if (pi.name == combProduct.SelectedItem.ToString())
                    {
                        pi.quantity += Convert.ToInt32(txtQountity.Text);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("please enter valid data");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPrice.Text = txtQountity.Text = TxtSum.Text = "";
            dataGridView1.Refresh();
        }
        private void txtQountity_KeyPress(object sender, KeyPressEventArgs e)
      {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 || e.KeyChar <=0)
            {
                e.Handled = true;
            }
        }
        private void txtQountity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TxtSum.Text = (Convert.ToDouble(txtPrice.Text) * Convert.ToInt32(txtQountity.Text)).ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("please enter valid data");
            }
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            var res = bills.Where(x => x.date_Bill.Day == dateTimePicker2.Value.Day).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = res;
        }
    }
}
