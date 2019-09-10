using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace DataOprate
{
    //保存连接信息
    class AsyncSocketUDPState
    {
        public Socket workSocket = null;
        public const int BufferSize = 2048;
        public byte[] buffer;
        // Received data string.
        public StringBuilder sb = new StringBuilder();

        public EndPoint remote; 

        public AsyncSocketUDPState()
        {
            buffer = new byte[BufferSize];
            remote = new IPEndPoint(IPAddress.Any, 0);//0指代任意可用端口;
        }

        //清空缓存buffer
        public void ClearBuffer()
        {
            buffer = new byte[BufferSize];
        }
    }

}
