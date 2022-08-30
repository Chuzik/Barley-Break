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
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Remoting.Activation;


namespace Server_15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Регистрация tcp канала через код
            RegisterTCP();
            RemotingConfiguration.Configure("C:\\Users\\Guzel\\source\\repos\\Server_15\\Server_15.config", false);
            MessageBox.Show("Сервер работает");

        }
        public void Add_log(string msg)
        {
            textBox1.Text += "[" + DateTime.Now + "] " + msg + Environment.NewLine;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Add_log("Server Start");
        }
        static void RegisterTCP()
        {
            try
            {
                Dictionary<string, string> properties_1 = new Dictionary<string, string>();

                properties_1["port"] = "8086";
                SoapServerFormatterSinkProvider srvPrvd_1 = new SoapServerFormatterSinkProvider();
                srvPrvd_1.TypeFilterLevel = TypeFilterLevel.Full;
                SoapClientFormatterSinkProvider clntPrvd_1 = new SoapClientFormatterSinkProvider();

                TcpChannel tcpChannel = new TcpChannel(properties_1, clntPrvd_1, srvPrvd_1);
                ChannelServices.RegisterChannel(tcpChannel, false);

                //RemotingConfiguration.ApplicationName = "Hello";
                RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Hello),
                "Hi",
                WellKnownObjectMode.Singleton);
            }
            catch (Exception)
            {
                Console.WriteLine("Упс, что-то пошло не так2");
                Console.ReadLine();//Удерживаем сервер в рабочем состоянии
            }
        }
    }
}
