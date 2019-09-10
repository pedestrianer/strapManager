using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataOprate
{
    class AsyncSocketClientMsg
    {
        
        public EndPoint remote; //客户端远程端口
        public byte[] sendMeg;
        

        public AsyncSocketClientMsg(EndPoint re, byte[] send)
        {
            remote = re;
            sendMeg = send; 
        }


    }
}
