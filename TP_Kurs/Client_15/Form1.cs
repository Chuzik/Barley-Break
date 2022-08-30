using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using _15_Game;
using System.Data.OleDb;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;



namespace Client_15
{
    
    public partial class Form1 : Form
    {
        Hello obj = new Hello();             

        public Form1()
        {
            InitializeComponent();
            //Регистрация tcp канала через код
            Dictionary<string, string> properties_1 = new Dictionary<string, string>();
            properties_1["port"] = "0";
            SoapServerFormatterSinkProvider srvPrvd_1 = new SoapServerFormatterSinkProvider();
            srvPrvd_1.TypeFilterLevel = TypeFilterLevel.Full;
            SoapClientFormatterSinkProvider clntPrvd_1 = new SoapClientFormatterSinkProvider();

            TcpChannel tcpChannel = new TcpChannel(properties_1, clntPrvd_1, srvPrvd_1);
            ChannelServices.RegisterChannel(tcpChannel, false);

            RemotingConfiguration.Configure("C:\\Users\\Guzel\\source\\repos\\Client_15\\Client_15.config", false);

            ILease lease = obj.MyInitializeLifetimeService();
            MySponsor sponsor = new MySponsor();
            lease.Register(sponsor);
            
            if (lease != null)
            {
                TimeSpan tm = new TimeSpan(0, 0, 5);
                lease.Renew(tm);
            }
            if (obj == null)
            {
                MessageBox.Show("Сервер не доступен");
                return;
            }


            label3.Text = "InitialLeaseTime: " + lease.InitialLeaseTime;
            label4.Text = "RenewOnCallTime: " + lease.RenewOnCallTime;
            label5.Text = "SponsorshipTimeout: " + lease.SponsorshipTimeout;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            if (textBox1.Text == "")
            {
                return;
            }
            if (textBox2.Text == "")
            {
                return;
            }

            string a = obj.Avtor(textBox1.Text, textBox2.Text);
            if (a == "Yes")
            {
                Form2 newForm = new Form2(this.textBox1.Text);
                newForm.Show();
            }
            else
                MessageBox.Show("Данных нет в БД", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5();
            newForm.Show();
        }
    }
}

