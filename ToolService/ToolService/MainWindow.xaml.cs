using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataOprate;
using System.Net;
using System.Net.Sockets;
using DBoperation;

namespace ToolService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private AsyncSocketUDPServer server;
        private DataAnalyze response;
        private FileInfoRead read;

        public MainWindow()
        {
            InitializeComponent();
            read = new FileInfoRead();
            concenIPText.Text = read.concentratorIP;
            concenPortText.Text = read.concentratorPORT;
            concenIPText.IsReadOnly = true;
            concenIPText.Background = Brushes.Gray;
            concenPortText.IsReadOnly = true;
            concenPortText.Background = Brushes.Gray;
            response = new DataAnalyze();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            server = new AsyncSocketUDPServer();
            server.Start();
            listenBtn.Content = "服务开启...";
        }

        private void tempSendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                read = new FileInfoRead();
                //更新集中器IP和PORT
                concenIPText.Text = read.concentratorIP;
                concenPortText.Text = read.concentratorPORT;
                int concentratoraddress = Convert.ToInt32(tempText.Text.Trim(), 16);
                IPEndPoint remote = new IPEndPoint(IPAddress.Parse(read.concentratorIP), Convert.ToInt32(read.concentratorPORT));
                Socket socketSend = server._serverSock;
                //构造发送命令：服务器主动申请数据
                byte[] orderMeg = RequestOrder((byte)concentratoraddress, Convert.ToInt32(strapNum1.Text), 0x05);
                socketSend.SendTo(orderMeg, remote);

                //等待获取回复， 最大延时5秒
                DateTime now = DateTime.Now;
                int s;
                bool success = false;
                do
                {
                    TimeSpan span = DateTime.Now - now;
                    s = span.Seconds;
                    if (DataAnalyze.tempStrapResponse)
                    {
                        success = true;
                        DataAnalyze.tempStrapResponse = false;//初始化请求回复标志为false
                        break;
                    }

                }
                while (s < 5);
                if (success)
                {
                    MessageBox.Show("请求成功");
                }
                else
                {
                    MessageBox.Show("请求失败");
                }
            }
            catch
            {
                MessageBox.Show("检查服务是否开启");
            }
            
        }

        private void fireSendBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                read = new FileInfoRead();
                //更新集中器IP和PORT
                concenIPText.Text = read.concentratorIP;
                concenPortText.Text = read.concentratorPORT;
                int concentratoraddress = Convert.ToInt32(fireText.Text.Trim(), 16);
                IPEndPoint remote = new IPEndPoint(IPAddress.Parse(read.concentratorIP), Convert.ToInt32(read.concentratorPORT));
                Socket socketSend = server._serverSock;
                //构造发送命令：服务器主动申请数据
                int strapNumber = Convert.ToInt32(strapNum2.Text);
                byte[] orderMeg = RequestOrder((byte)concentratoraddress, strapNumber, 0x06);
                socketSend.SendTo(orderMeg, remote);
                //等待获取回复， 最大延时5秒
                DateTime now = DateTime.Now;
                int s;
                bool success = false;
                do
                {
                    TimeSpan span = DateTime.Now - now;
                    s = span.Seconds;
                    if (DataAnalyze.fireStrapResponse)
                    {
                        success = true;
                        DataAnalyze.fireStrapResponse = false;
                        break;
                    }

                }
                while (s < 5);
                if (success)
                {
                    MessageBox.Show("请求成功");
                }
                else
                {
                    MessageBox.Show("请求失败");
                }
            }
            
            catch
            {
                MessageBox.Show("检查服务是否开启");
            }

        }

        public byte[] RequestOrder(byte concentratorAddress, int strapNum, byte registAddress)
        {
            byte[] order = new byte[8] {0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            order[0] = concentratorAddress;
            order[3] = registAddress;
            if(registAddress == 0x05)
            {
                order[4] = (byte)(strapNum*12 / 256);
                order[5] = (byte)(strapNum*12 % 256);
            }
            else if(registAddress == 0x06)
            {
                order[4] = (byte)(strapNum*10 / 256);
                order[5] = (byte)(strapNum*10 % 256);
            }
            
            return order;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ConcentratorNetSetting conNetSetting = new ConcentratorNetSetting(server._serverSock);
                conNetSetting.ShowDialog();
            }
                
            catch(NullReferenceException nullRefEx)
            {
                
                MessageBox.Show("检查服务是否开启！");
            }
                
            //this.Hide();
        }

        private void SetTempStrap_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                setTemp setT = new setTemp(server._serverSock);
                setT.ShowDialog();
            }

            catch (NullReferenceException nullRefEx)
            {

                MessageBox.Show("检查服务是否开启！");
            }
            
        }

        private void SetFireStrap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setFire setF = new setFire(server._serverSock);
                setF.ShowDialog();
            }

            catch (NullReferenceException nullRefEx)
            {

                MessageBox.Show("检查服务是否开启！");
            }
            
        }

        private void Btnfind_Click(object sender, RoutedEventArgs e)
        {
            FindStrap find = new FindStrap();
            find.ShowDialog();
        }
    }
}
