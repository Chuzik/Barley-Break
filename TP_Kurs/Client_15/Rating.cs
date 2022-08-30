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
    public partial class Rating : Form
    {
        Hello obj = new Hello();
        public Rating()
        {
            InitializeComponent();
            ILease lease = obj.MyInitializeLifetimeService();
            MySponsor sponsor = new MySponsor();
            lease.Register(sponsor);
            // очищаем listBox1
            label1.Text=obj.Rating();
        }
    }
}
