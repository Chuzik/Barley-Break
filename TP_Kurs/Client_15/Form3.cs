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
    public partial class Form3 : Form
    {
        Button[] B = new Button[16];

        Hello obj = new Hello();
        
        public Form3(string data)
        {
            InitializeComponent();

            ILease lease = obj.MyInitializeLifetimeService();
            MySponsor sponsor = new MySponsor();
            lease.Register(sponsor);

            B[0] = button1;
            B[1] = button2;
            B[2] = button3;
            B[3] = button4;
            B[4] = button5;
            B[5] = button6;
            B[6] = button7;
            B[7] = button8;
            B[8] = button9;
            B[9] = button10;
            B[10] = button11;
            B[11] = button12;
            B[12] = button13;
            B[13] = button14;
            B[14] = button15;
            B[15] = button16;
            panel1_Resize(null, null);
            this.data = data;
        }
        string data;

        private void panel1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                int x, y;
                y = i / 4;
                x = i - y * 4;
                B[i].Top = y * panel1.Height / 4;
                B[i].Left = x * panel1.Width / 4;
                B[i].Height = panel1.Height / 4;
                B[i].Width = panel1.Width / 4;
               // B[i].Font = new Font("Arial", panel1.Width / 10, FontStyle.Bold);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button bclick = (Button)sender;
            int n = (int)Convert.ToInt64(bclick.Tag.ToString());//номер кнопки
            n--;
            int y = n / 4;
            int x = n - y * 4;
            int yt, yb;//кнопка сверху, кнопка снизу
            int xl, xr;
            yt = y - 1;
            yb = y + 1;
            xl = x - 1;
            xr = x + 1;
            if (xr<4)
            {
                int nr = y * 4 + xr;
                if (!B[nr].Visible)
                {
                    B[nr].Visible = true;
                    B[n].Visible = false;
                    B[nr].Text = B[n].Text;
                }
            }
            if (xl > -1)
            {
                int nl = y * 4 + xl;
                if (!B[nl].Visible)
                {
                    B[nl].Visible = true;
                    B[n].Visible = false;
                    B[nl].Text = B[n].Text;
                }
            }
            if (yt > -1)
            {
                int nt = yt * 4 + x;
                if (!B[nt].Visible)
                {
                    B[nt].Visible = true;
                    B[n].Visible = false;
                    B[nt].Text = B[n].Text;
                }
            }
            if (yb < 4)
            {
                int nb = yb * 4 + x;
                if (!B[nb].Visible)
                {
                    B[nb].Visible = true;
                    B[n].Visible = false;
                    B[nb].Text = B[n].Text;
                }
            }
            int k = 0;
            for (int i = 0; i < 15; i++)
            {
                if(Int32.Parse(B[i].Text) == i+1)
                {
                    k++;                    
                }
            }
            if (k == 15)
            {

                timer1.Enabled = false;
                t3 = DateTime.Now;
                TimeSpan ts = t3 - t1;
                obj.Time_pVvod(ts.Hours, ts.Minutes, ts.Seconds, data);

                Form4 newForm = new Form4(data);
                newForm.Show();
            }
        }
        DateTime t1, t2, t3;
        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            t1 = DateTime.Now;
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            t2 = DateTime.Now;
            TimeSpan ts = t2 - t1;
            label1.Text = ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
        }


    }
}
public class MySponsor : MarshalByRefObject, ISponsor
{
    private DateTime lastRenewal;
    public MySponsor()
    {
        MessageBox.Show("MyClientSponsor.ctor called");
        lastRenewal = DateTime.Now;
    }

    public TimeSpan Renewal(ILease lease)
    {
        MessageBox.Show("I've been asked to renew the lease.");
        MessageBox.Show("Time since last renewal:" + (DateTime.Now - lastRenewal).ToString());

        lastRenewal = DateTime.Now;
        return TimeSpan.FromSeconds(20);
    }
}
