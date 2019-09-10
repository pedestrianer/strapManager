using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBoperation
{
    class DBManager
    {
        private MySqlConnection conn = null;
        private DataTable dt;//存储select方法的查询信息

        public DBManager()
        {
            FileInfoRead read = new FileInfoRead();
            string connStr = "server= 127.0.0.1; user=root; password=123456; database=concentrator; charset=utf8";
            if (conn == null)
            {
                conn = new MySqlConnection();

                conn.ConnectionString = connStr;
            }  
        }
        #region 增删改查
        public bool Update(string sqlstr)
        {
            try
            {
                if (conn == null)
                {
                    return false;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (MySqlCommand command = new MySqlCommand(sqlstr, conn))
                {
                    command.ExecuteNonQuery();
                    return true;
                }

            }
            catch
            {
                conn.Close();
                return false;
            }
        }

        public bool Delete(string sqlstr)
        {
            try
            {
                if (conn == null)
                {
                    return false;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (MySqlCommand command = new MySqlCommand(sqlstr, conn))
                {
                    command.ExecuteNonQuery();
                    return true;
                }

            }
            catch
            {
                conn.Close();
                return false;
            }
        }

        public DataTable Select(string sqlstr)
        {
            try
            {
                if (conn == null)
                {
                    return null;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (MySqlCommand command = new MySqlCommand(sqlstr, conn))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    dt = new DataTable();
                    dt.Clear();
                    dt.Load(reader);

                    reader.Close();
                    return dt;
                }

            }
            catch
            {
                conn.Close();
                return null;
            }
        }

        public bool Insert(string sqlstr)
        {
            try
            {
                if (conn == null)
                {
                    return false;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (MySqlCommand command = new MySqlCommand(sqlstr, conn))
                {
                    command.ExecuteNonQuery();
                    return true;
                }

            }
            catch
            {
                conn.Close();
                return false;
            }
        }      
        #endregion
    }
}
