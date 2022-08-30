using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Runtime.Remoting.Lifetime;

namespace _15_Game
{
    public class Hello : MarshalByRefObject
    {
        // строка подключения к MS Access
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Guzel\\source\\repos\\Server_15\\bin\\Debug\\Game_15.mdb;";
        //public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Game_15.mdb;";

        // поле - ссылка на экземпляр класса OleDbConnection для соединения с БД
        private OleDbConnection myConnection;

        public Hello()
        {
        }
        ~Hello()
        {
        }
        public ILease MyInitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(7);
                lease.SponsorshipTimeout = TimeSpan.FromMinutes(9);
                lease.RenewOnCallTime = TimeSpan.FromMinutes(11);

            }   
            return lease;
        }
        public string Avtor(string login, string password)
        {
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();

            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand("SELECT * FROM Game WHERE login = '" + login + "' AND password = '" + password + "'", myConnection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            string rez = "";
            if (table.Rows.Count > 0)
                rez = "Yes";
            else
                rez = "No";

            myConnection.Close();

            return rez;            
        }
        public string Register(string login, string password)
        {
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand("INSERT INTO [Game] ([login], [password], [hours_best],[minutes_best],[seconds_best]) VALUES ( @login, @password, @hours_best, @minutes_best, @seconds_best)", myConnection);

            command.Parameters.Add("@login", OleDbType.VarChar).Value = login;
            command.Parameters.Add("@password", OleDbType.VarChar).Value = password;
            command.Parameters.Add("@hours_best", OleDbType.VarChar).Value = 500;
            command.Parameters.Add("@minutes_best", OleDbType.VarChar).Value = 0;
            command.Parameters.Add("@seconds_best", OleDbType.VarChar).Value = 0;
            int enq = command.ExecuteNonQuery();
            myConnection.Close();

            return enq.ToString();
        }

        public Boolean checkUser(string login)
        {
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();

            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand("SELECT * FROM Game WHERE login = '" + login + "'", myConnection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            myConnection.Close();
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public string Time_pVvod(int hours, int minutes, int seconds, string login)
        {
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            //OleDbCommand command = new OleDbCommand("INSERT INTO [Game] ([hours_p], [minutes_p], [seconds_p]) VALUES ( @hours, @minutes, @seconds)", myConnection);
            OleDbCommand command = new OleDbCommand("UPDATE Game SET hours_p = @hours, minutes_p = @minutes, seconds_p = @seconds WHERE login = '" + login + "'", myConnection);

            command.Parameters.Add("@hours", OleDbType.VarChar).Value = hours;
            command.Parameters.Add("@minutes", OleDbType.VarChar).Value = minutes;
            command.Parameters.Add("@seconds", OleDbType.VarChar).Value = seconds;
            command.Parameters.Add("@login", OleDbType.VarChar).Value = login;
            command.ExecuteNonQuery();





            OleDbCommand command1 = new OleDbCommand("SELECT seconds_best FROM Game WHERE login = '" + login + "'", myConnection);
            OleDbCommand command2 = new OleDbCommand("SELECT minutes_best FROM Game WHERE login = '" + login + "'", myConnection);
            OleDbCommand command3 = new OleDbCommand("SELECT hours_best FROM Game WHERE login = '" + login + "'", myConnection);

            if (hours == Int32.Parse(command3.ExecuteScalar().ToString()))
            {
                if (minutes == Int32.Parse(command2.ExecuteScalar().ToString()))
                {
                    if (seconds < Int32.Parse(command1.ExecuteScalar().ToString()))
                    {
                        OleDbCommand command_R = new OleDbCommand("UPDATE Game SET hours_best = @hours, minutes_best = @minutes, seconds_best = @seconds WHERE login = '" + login + "'", myConnection);

                        command_R.Parameters.Add("@hours", OleDbType.VarChar).Value = hours;
                        command_R.Parameters.Add("@minutes", OleDbType.VarChar).Value = minutes;
                        command_R.Parameters.Add("@seconds", OleDbType.VarChar).Value = seconds;
                        command_R.Parameters.Add("@login", OleDbType.VarChar).Value = login;
                        command_R.ExecuteNonQuery();
                    }
                }
                else if (minutes < Int32.Parse(command2.ExecuteScalar().ToString()))
                {
                    OleDbCommand command_R = new OleDbCommand("UPDATE Game SET hours_best = @hours, minutes_best = @minutes, seconds_best = @seconds WHERE login = '" + login + "'", myConnection);

                    command_R.Parameters.Add("@hours", OleDbType.VarChar).Value = hours;
                    command_R.Parameters.Add("@minutes", OleDbType.VarChar).Value = minutes;
                    command_R.Parameters.Add("@seconds", OleDbType.VarChar).Value = seconds;
                    command_R.Parameters.Add("@login", OleDbType.VarChar).Value = login;
                    command_R.ExecuteNonQuery();
                }
            }
            if (hours < Int32.Parse(command3.ExecuteScalar().ToString()))
            {
                OleDbCommand command_R = new OleDbCommand("UPDATE Game SET hours_best = @hours, minutes_best = @minutes, seconds_best = seconds WHERE login = '" + login + "'", myConnection);

                command_R.Parameters.Add("@hours", OleDbType.VarChar).Value = hours;
                command_R.Parameters.Add("@minutes", OleDbType.VarChar).Value = minutes;
                command_R.Parameters.Add("@seconds", OleDbType.VarChar).Value = seconds;
                command_R.Parameters.Add("@login", OleDbType.VarChar).Value = login;
                command_R.ExecuteNonQuery();
            }




            myConnection.Close();

            return login;
        }
        public string Time_pVuvod(string login)
        {
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            //OleDbCommand command = new OleDbCommand("INSERT INTO [Game] ([hours_p], [minutes_p], [seconds_p]) VALUES ( @hours, @minutes, @seconds)", myConnection);
            OleDbCommand command = new OleDbCommand("SELECT seconds_p FROM Game WHERE login = '" + login + "'", myConnection);
            OleDbCommand command2 = new OleDbCommand("SELECT minutes_p FROM Game WHERE login = '" + login + "'", myConnection);
            OleDbCommand command3 = new OleDbCommand("SELECT hours_p FROM Game WHERE login = '" + login + "'", myConnection);
            
            string time;
            time = command3.ExecuteScalar().ToString() + ":" + command2.ExecuteScalar().ToString() + ":" + command.ExecuteScalar().ToString();
            myConnection.Close();
            return time;
        }


        public string Rating ()
        {
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();
            
            string[] Res = new string[15];
            for (int i = 0; i < Res.Length; i++)
            {
                Res[i] = null;
            }
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand("SELECT id, login, hours_best, minutes_best, seconds_best FROM Game WHERE hours_best <> 500 ORDER BY id", myConnection);

            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();


            int j = 0;
            string Ress = null;
            //OleDbCommand cmd = connectString.CreateCommand();
            // в цикле построчно читаем ответ от БД
            while (reader.Read())
            {
                j++;
                for (int i = 0; i < 5; i++)
                    Res[j] = Res[j] + "      " + reader[i].ToString() + "\t";
                Res[j] = Res[j] + "\n";
            }
            for (int i = 0; i < Res.Length; i++)
            {
                Ress = Ress + Res[i];
            }
            // закрываем OleDbDataReader
            reader.Close();
            return Ress;
        }
    }

}
