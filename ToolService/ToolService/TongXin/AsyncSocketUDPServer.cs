using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DBoperation;

namespace DataOprate
{
    class AsyncSocketUDPServer
    {
        #region Fields

        public Socket _serverSock;

        private bool disposed = false;

        private AsyncSocketUDPState so;//连接信息

        DataAnalyze op = new DataAnalyze();

        Queue<byte[]> revDataQueue = new Queue<byte[]>();

        #endregion

        #region Properties

        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }
        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }
        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// 通信使用的编码
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 异步Socket UDP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public AsyncSocketUDPServer()
            : this(IPAddress.Any, getPort())
        {
        }

        /// <summary>
        /// 异步Socket UDP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public AsyncSocketUDPServer(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port)
        {
        }

        /// <summary>
        /// 异步Socket UDP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        /// <param name="maxClient">最大客户端数量</param>
        public AsyncSocketUDPServer(IPAddress localIPAddress, int listenPort)
        {
            this.Address = localIPAddress;
            this.Port = listenPort;
            this.Encoding = Encoding.Default;

            _serverSock = new Socket(localIPAddress.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            so = new AsyncSocketUDPState();
        }

        #endregion

        #region Method
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <returns>异步UDP服务器</returns>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _serverSock.Bind(new IPEndPoint(this.Address, this.Port));
                so.workSocket = _serverSock;

                _serverSock.BeginReceiveFrom(so.buffer, 0, so.buffer.Length, SocketFlags.None,
                     ref so.remote, new AsyncCallback(ReceiveDataAsync), so.remote);
                /*
                Console.WriteLine("Listen...(Press any key to terminate the server process)");
                Console.ReadLine();
                Stop();
                 */
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Idata"></param>
        private void SendData(IAsyncResult Idata)
        {
            _serverSock.EndSend(Idata);

        }

        private void ReceiveDataAsync(IAsyncResult Idata)
        {
            int recv = _serverSock.EndReceiveFrom(Idata, ref so.remote);
            byte[] receiveData = new byte[recv];
            Array.Copy(so.buffer, receiveData, recv);
            if(receiveData.Length > 7)
            {
                op.DataAnalysis(receiveData); 
            }
                                            
            so.ClearBuffer();
            _serverSock.BeginReceiveFrom(so.buffer, 0, so.buffer.Length, SocketFlags.None, ref so.remote, new AsyncCallback(ReceiveDataAsync), so.remote);
        }

        //获取客户端信息列表中指定远程端点的项
        private AsyncSocketClientMsg GetClientMsg(List<AsyncSocketClientMsg> clientMsg, EndPoint endpoint)
        {
            AsyncSocketClientMsg cm = null;
            foreach(var item in clientMsg)
            {
                if (item.remote.ToString() == endpoint.ToString())
                {
                    cm = item;
                    break;
                }
            }
            return cm;
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                _serverSock.Close();
                //TODO 关闭对所有客户端的连接

            }
        }

        private static int getPort()
        {
            int port;
            FileInfoRead read = new FileInfoRead();
            port = int.Parse(read.port);
            return port;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 接收到数据事件
        /// </summary>
        public event EventHandler<AsyncSocketUDPEventArgs> DataReceived;

        private void RaiseDataReceived(AsyncSocketUDPState state)
        {
            if (DataReceived != null)
            {
                DataReceived(this, new AsyncSocketUDPEventArgs(state));
            }
        }

        /// <summary>
        /// 发送数据前的事件
        /// </summary>
        public event EventHandler<AsyncSocketUDPEventArgs> PrepareSend;

        /// <summary>
        /// 触发发送数据前的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaisePrepareSend(AsyncSocketUDPState state)
        {
            if (PrepareSend != null)
            {
                PrepareSend(this, new AsyncSocketUDPEventArgs(state));
            }
        }

        /// <summary>
        /// 数据发送完毕事件
        /// </summary>
        public event EventHandler<AsyncSocketUDPEventArgs> CompletedSend;

        /// <summary>
        /// 触发数据发送完毕的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseCompletedSend(AsyncSocketUDPState state)
        {
            if (CompletedSend != null)
            {
                CompletedSend(this, new AsyncSocketUDPEventArgs(state));
            }
        }
        #endregion
        #region 释放
        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> 
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        if (_serverSock != null)
                        {
                            _serverSock = null;
                        }
                    }
                    catch (SocketException)
                    {
                        //TODO
                        
                    }
                }
                disposed = true;
            }
        }
        #endregion
    }

}