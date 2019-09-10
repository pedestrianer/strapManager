using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
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
using DBoperation;
using System.Data;
using System.Net;


namespace ToolService
{
    /// <summary>
    /// ConcentratorNetSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ConcentratorNetSetting : Window
    {
        private Socket socketSetting;
        FileInfoRead SetFile;
        public ConcentratorNetSetting()
        {
            InitializeComponent();
        }
        public ConcentratorNetSetting(Socket socket_)
        {
            socketSetting = socket_;
            InitializeComponent();
            SetFile = new FileInfoRead();
            
            //服务器参数设置
            remoteHostIPText.Text = GetAddressIP();
            remoteHostPortText.Text = SetFile.port;
            
            //显示集中器初始网络参数
            concentratorIPText.Text = SetFile.concentratorIP;
            concentratorPortText.Text = SetFile.concentratorPORT;
            //服务器网络参数设为只读
            remoteHostPortText.IsReadOnly = true;
            //本机网络参数文本框背景变暗
            remoteHostIPText.Background = Brushes.Gray;
            remoteHostPortText.Background = Brushes.Gray;
            
        }
        private void excuteBtn_Click(object sender, RoutedEventArgs e)
        {
            SetFile = new FileInfoRead();
            DBManager db = new DBManager();        
            IPEndPoint remote = new IPEndPoint(IPAddress.Parse(SetFile.concentratorIP), Convert.ToInt32(SetFile.concentratorPORT));
            SetConcentratorNet set = new SetConcentratorNet(concentratorText.Text, concentratorIPText.Text, concentratorPortText.Text, localHostSubnetMsakText.Text, remoteHostIPText.Text, remoteHostPortText.Text, gatewayIPText.Text);
            byte[] order = set.getSettingOrder();
            if (order[1] == 0x10)
            {
              socketSetting.SendTo(order, remote);
            }
            else
             {
                 MessageBox.Show("检查输入后重试");
                 return;
              }

            //延时等待集中器回复，等待最大时长5秒
            DateTime now = DateTime.Now;
            int s;
            bool success = false;
            do
            {
                TimeSpan span = DateTime.Now - now;
                s = span.Seconds;
                if (DataAnalyze.setConcentratorResponse)
                {
                    success = true;
                    DataAnalyze.setConcentratorResponse = false;
                    break;
                }

            }
            while (s < 5);
            if (success)
            {
                if (new FileInfoRead().WriteIniData(concentratorIPText.Text, concentratorPortText.Text))
                {
                    MessageBox.Show("配置成功");
                }
                else
                {
                    MessageBox.Show("配置成功但写入配置文件失败");
                }
                
            }
            else
            {
                MessageBox.Show("配置失败");
            }       
            
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //获取本机网络参数，包括IP，子网掩码，网关
        private  string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
    
    }
}
