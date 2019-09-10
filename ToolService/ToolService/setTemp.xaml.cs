using DBoperation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    /// setTemp.xaml 的交互逻辑
    /// </summary>
    public partial class setTemp : Window
    {
        List<string[]> list = new List<string[]>();
        ObservableCollection<string[]> showdata = new ObservableCollection<string[]>();
        private Socket socketSetting;
        FileInfoRead SetFile;

        public setTemp(Socket socket_)
        {
            InitializeComponent();
            socketSetting = socket_;
            AddSource();
            GetShowData();
        }
        //---原始数据源到显示数据源
        private void GetShowData()
        {
            showdata.Clear();
            foreach (var a in list)
            {
                showdata.Add(a);
            }
            dtgShow.ItemsSource = showdata;
        }
        //---显示数据到原始数据
        private void GetRawData()
        {
            list.Clear();
            foreach (var a in showdata)
            {
                list.Add(a);
            }
        }
        //---添加 DataGrid 数据源
        private void AddSource()
        {
            list.Add(new string[] { "", "" });
            string head = "设备名称";
            dtgShow.Columns.Add(new DataGridTextColumn
            {
                Width = (260 - 38) / 2,
                Header = $"{head}",
                Binding = new Binding($"[{0.ToString()}]")
            });
            head = "表带地址";
            dtgShow.Columns.Add(new DataGridTextColumn
            {
                Width = (260 - 38) / 2,
                Header = $"{head}",
                Binding = new Binding($"[{1.ToString()}]")
            });


        }
        //---自动添加行号
        private void dtgShow_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        //---Enter 达到 Tab 的效果
        private void dtgShow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }
      
        //---获取所有的选中cell 的值
        private string GetSelectedCellsValue(DataGrid dg)
        {
            var cells = dg.SelectedCells;
            StringBuilder sb = new StringBuilder();
            if (cells.Any())
            {
                foreach (var cell in cells)
                {
                    sb.Append((cell.Column.GetCellContent(cell.Item) as TextBlock).Text);
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        private void SetRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = int.Parse(strapNum.Text.Trim());
                //先删除所有至表格只剩下1行
                while (showdata.Count > 1)
                {
                    showdata.RemoveAt(showdata.Count - 1);
                }
                for (int i = 0; i < num - 1; i++)
                {
                    showdata.Add(new string[dtgShow.Columns.Count]);
                }
            }
            catch
            {
                MessageBox.Show("检查输入是否为数字");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dtgShow.SelectAllCells();//选中所有行
            var cells = dtgShow.SelectedCells;
            StringBuilder sb = new StringBuilder();
            if (cells.Any())
            {
                foreach (var cell in cells)
                {
                    sb.Append((cell.Column.GetCellContent(cell.Item) as TextBlock).Text);
                    sb.Append(" ");
                }
            }
            string[] str = sb.ToString().Split(' ');
            byte[] data = exeData(str);
            if (data == null)
                MessageBox.Show("请检查输入完整");
            else
            {
                SetFile = new FileInfoRead();                
                IPEndPoint remote = new IPEndPoint(IPAddress.Parse(SetFile.concentratorIP), Convert.ToInt32(SetFile.concentratorPORT));
                socketSetting.SendTo(data, remote);

            }
            //延时等待集中器回复，等待最大时长5秒
            DateTime now = DateTime.Now;
            int s;
            bool success = false;
            do
            {
                TimeSpan span = DateTime.Now - now;
                s = span.Seconds;
                if (DataAnalyze.setTempStrapAddressResponse)
                {
                    success = true;
                    DataAnalyze.setTempStrapAddressResponse = false;
                    break;
                }

            }
            while (s < 5);
            if (success)
            {
                DBManager db = new DBManager();
                string sql = "insert into strapmap (devName,strapaddress,type) values ";
                for(int i=0;i<str.Length/2;i++)
                {
                    sql = sql + "('" + str[2*i] + "','" + str[2*i+1] + "',' 测温 '),";
                }
                sql = sql.Substring(0, sql.Length - 1);
                if (db.Insert(sql))
                {
                    MessageBox.Show("配置成功");
                }
                else
                {
                    MessageBox.Show("配置成功但写入数据库失败");
                }

            }
            else
            {
                MessageBox.Show("配置失败");
            }

        }

        //---获取表格数据返回控制字节流
        private byte[] exeData(string[] str)
        {   
            try
            {
                int strapN = int.Parse(strapNum.Text.Trim());
                byte[] data = new byte[strapN * 5 + 8];
                data[0] = 0x01;
                data[1] = 0x10;
                data[2] = data[3] = 0x00;
                data[4] = (byte)(strapN * 5);
                int j = 5;
                for (int i = 0; 2 * i < str.Length - 2; i++)
                {
                    string node = str[2 * i + 1];
                    if (node.Length == 10)
                    {
                        int k = 0;
                        while (k < 10)
                        {
                            data[j++] = byte.Parse(node.Substring(k, 2));
                            k += 2;
                        }
                    }
                    else
                    {
                        data = null;
                        break;
                    }
                        
                }
                data[j++] = 0x00;
                data[j] = 0x00;
                //输入不完整
                if (j != strapN * 5 + 6)
                    data = null;
                return data;
            }
            catch
            {

                return null;
            }
            
        }
    }
}
