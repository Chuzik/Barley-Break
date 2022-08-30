using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using _15_Game;
using System.Runtime.Remoting.Lifetime;

namespace Client_15
{
    public partial class Form5 : Form
    {
        Hello obj = new Hello();
        // строка подключения к MS Access
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Game_15.mdb;";
       
        // поле - ссылка на экземпляр класса OleDbConnection для соединения с БД
        private OleDbConnection myConnection;
        Hello odj = new Hello();
        public Form5()
        {
            InitializeComponent(); 
            ILease lease = obj.MyInitializeLifetimeService();
            MySponsor sponsor = new MySponsor();
            lease.Register(sponsor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hello obj = new Hello();


            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите логин");
                return;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (obj.checkUser(textBox1.Text) == true)
            {
                MessageBox.Show("Такой логин уже существует, введите другой");
                return;
            }

            int a = Int32.Parse(obj.Register(textBox1.Text, textBox2.Text));



            if (a == 1)
            {
                MessageBox.Show("Аккаунт был создан");
            }
            else
                MessageBox.Show("Аккаунт не был создан");
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
