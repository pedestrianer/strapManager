using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolService
{
    class SetConcentratorNet
    {
        private string concentratorAddress;
        private string localHostIP;
        private string localHostPort;
        private string localHostSubnetMsak;
        private string remoteHostIP;
        private string remoteHostPort;
        private string gatewayIP;

        public SetConcentratorNet(string concentratorAddress_, string localHostIP_, string localHostPort_, string localHostSubnetMsak_, string remoteHostIP_, string remoteHostPort_, string gatewayIP_)
        {
            concentratorAddress = concentratorAddress_;
            localHostIP = localHostIP_;
            localHostPort = localHostPort_;
            localHostSubnetMsak = localHostSubnetMsak_;
            remoteHostIP = remoteHostIP_;
            remoteHostPort = remoteHostPort_;
            gatewayIP = gatewayIP_;
        }

        public byte[] getSettingOrder()
        {
            byte[] order = new byte[28];
            
            try
            {
                order[0] = Convert.ToByte(concentratorAddress, 16);
                order[1] = 0x10;
                order[2] = 0x00;
                order[3] = 0x08;
                order[4] = 0x00;
                order[5] = 0x14;
                //本机IP字节
                for(int i = 6; i<10; i++)
                {
                    order[i] = Convert.ToByte(localHostIP.Split('.')[i-6]);
                }
                order[10] = (byte)(Convert.ToInt32(localHostPort) / 256);
                order[11] = (byte)(Convert.ToInt32(localHostPort) % 256);
                //本机子网掩码字节
                for (int i = 12; i < 16; i++)
                {
                    order[i] = Convert.ToByte(localHostSubnetMsak.Split('.')[i - 12]);
                }
                //远程主机IP字节
                for (int i = 16; i < 20; i++)
                {
                    order[i] = Convert.ToByte(remoteHostIP.Split('.')[i - 16]);
                }
                order[20] = (byte)(Convert.ToInt32(remoteHostPort) / 256);
                order[21] = (byte)(Convert.ToInt32(remoteHostPort) % 256);
                //路由网关字节
                for (int i = 22; i < 26; i++)
                {
                    order[i] = Convert.ToByte(gatewayIP.Split('.')[i - 22]);
                }
                order[26] = 0x00;
                order[27] = 0x00;
                return order;
            }
            catch
            {
                order[1] = 0x00;//数据有异常功能代码置零
                return order;
            }
            
              
        }
    }
}
