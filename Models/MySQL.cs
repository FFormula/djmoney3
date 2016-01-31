using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.Configuration;


namespace djMoney3.Models
{
    public class MySQL
    {
        protected string connect; //connectionstirng;
        protected MySqlCommand cmd;
        public string error { get; protected set; }
        public string query { get; protected set; }
        protected MySqlConnection myconnection;

        public MySQL(string connect)
        {
            this.connect = connect;
        }

        public MySQL()
        {
            this.connect = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }

        public MySQL(string host, string baze, string user, string pass)
            : this(
                "SERVER=" + host
                + ";DATABASE=" + baze
                + ";UID=" + user
                + ";PASSWORD=" + pass
                + ";CHARSET=utf8"
                )
        { }

        protected bool Open()
        {
            error = "";

            try
            {
                myconnection = new MySqlConnection(connect);
                myconnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                query = "CONNECT TO MYSQL (OPEN)";
                return false;
            }

        }
        protected bool Close()
        {

            try
            {
                myconnection.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public string Scalar(string query) //возвращает одно число / строку - одно значение
        {
            error = "";
            this.query = query;
            string scalar = "";
            if (!Open()) return "";

            try
            {
                cmd = new MySqlCommand(query, myconnection);
                scalar = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                scalar = "";
            }

            Close();

            return scalar;
        }
        public DataTable Select(string query)
        {
            DataTable table;
            error = "";
            this.query = query;

            if (!Open()) return null;

            try
            {
                cmd = new MySqlCommand(query, myconnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                table = null;
            }

            Close();
            return table;

        }
        public long Insert(string query)//добавление одной строки - возвр ппорядковый номер строки (или -1);
        {
            int result = this.Update(query);

            if (result > 0)
                return cmd.LastInsertedId;
            else return 0;

        }
        public int Update(string query)//update or delete return afterred rows -скока строк изменено
        {

            int result = 0;

            error = "";
            this.query = query;

            if (!Open()) return result;

            try
            {
                cmd = new MySqlCommand(query, myconnection);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                error = ex.Message;
                return -1;
            }
            Close();
            return result;
        }

        public bool Modify(string query)//запросы на изменение структуру БД - create, drop, alter table
        {
            int result = this.Update(query);
            if (result == -1) return false;
            else return true;

        }
        public string addslashes(string text)
        {
            return text.Replace("'", "\\'");
        }
        public bool SqlError()
        {
            if (error == "") return false;
            else
            {
                return true;
            }
        }

    }
}
