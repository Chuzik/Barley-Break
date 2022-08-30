using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Lifetime;
using _15_Game;

namespace Client_15
{
    public partial class Form2 : Form
    {
        Hello obj = new Hello();
        public Form2(string data)
        {
            InitializeComponent();
            this.data = data;
            ILease lease = obj.MyInitializeLifetimeService();
            MySponsor sponsor = new MySponsor();
            lease.Register(sponsor);
        }
        string data;
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3(data);
            newForm.Show();
        }
    }
}
