using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _15_Game;
using System.Runtime.Remoting.Lifetime;

namespace Client_15
{
    public partial class Form4 : Form
    {
        Hello obj = new Hello();
        public Form4(string data)
        {
            InitializeComponent();

            ILease lease = obj.MyInitializeLifetimeService();
            MySponsor sponsor = new MySponsor();
            lease.Register(sponsor);

            this.data = data;
            //label2.Text = ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
            label2.Text = obj.Time_pVuvod(data);
        }
        string data;

        private void button1_Click(object sender, EventArgs e)
        {
            Rating newForm = new Rating();
            newForm.Show();
        }
    }
}
