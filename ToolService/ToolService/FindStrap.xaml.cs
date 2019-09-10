using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToolService
{
    /// <summary>
    /// FindStrap.xaml 的交互逻辑
    /// </summary>
    public partial class FindStrap : Window
    {
        //SQLBulkCopy
        Random rd = new Random();
        string sqlstr = "Data Source=127.0.0.1;User ID=root;Password=123456;DataBase=concentrator;Charset=utf8;";
        MySqlConnection con;
        MySqlDataAdapter adapter;
        public FindStrap()
        {
            InitializeComponent();
            if (con == null)
            {
                con = new MySql.Data.MySqlClient.MySqlConnection(sqlstr);
                con.Open();
            }
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = null;
            string logicAddress = txtBox.Text;
            string sql = "select type from strapmap where strapaddress = '" + logicAddress + "'";
            DataTable dt = findValue(sql);
            string sqlstr = "";
            string str = dt.Rows[0][0].ToString().Trim();
            if (str == "测温")
            {
                data.Binding = new Binding($"[{"temperture"}]");
                sqlstr = "select strapaddress,straptype,temperture,voltage from concentratortempdata where strapaddress = '" + logicAddress + "'";
            }
            else
            {
                data.Binding = new Binding($"[{"isfire"}]");
                sqlstr = "select strapaddress,straptype,isfire,voltage from concentratorfiredata where strapaddress = '" + logicAddress + "'";
            }
            dt = findValue(sqlstr);
            txtNum.Text = dt.Rows.Count.ToString();
            dg.ItemsSource = dt.DefaultView;
        }
        private DataTable findValue(string sql)
        {
            DataTable dta = new DataTable();
            DataSet dst = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql,con);
            dst.Clear();
            adapter.Fill(dst, "data");

            dta = dst.Tables["data"];
            
            return dta;
        }
    }
}
