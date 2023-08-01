using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UtilityTCP
{
    public class Server
    {
        private static readonly IPAddress ALLOW_IP_ADRESS = IPAddress.Any;
        private static readonly int DEFAULT_LISTEN_PORT = 22222;

        private static object lockObj = new object(); //ロック処理に必要

        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();

        // 許可するIP
        private IPAddress allowIpAdress;
        // ポート
        private int listenPort;

        private List<string> receiveMessageTmp;

        public Server(IPAddress ipAddress, int listenPort)
        {
            this.allowIpAdress = ipAddress;
            this.listenPort = listenPort;

            Init();
        }

        public Server(IPAddress ipAddress)
            : this(ipAddress, DEFAULT_LISTEN_PORT)
        {

        }

        public Server(int listenPort)
            : this(ALLOW_IP_ADRESS, listenPort)
        {

        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Init()
        {
            this.listener = new TcpListener(this.allowIpAdress, this.listenPort);
            this.receiveMessageTmp = new List<string>();
        }

        /// <summary>
        /// Listenを開始する
        /// </summary>
        public async void Start()
        {
            this.listener.Start();
            receiveMessageTmp.Clear();
            await Task.Run(() => this.Run());
        }

        /// <summary>
        /// Listenを終了する
        /// </summary>
        public void Stop()
        {
            lock (lockObj)
            {
                this.listener.Stop();
                this.listener = null;
            }
        }

        /// <summary>
        /// 受信したメッセージを取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetMessage()
        {
            var receiveMessage = new List<string>(receiveMessageTmp);
            receiveMessageTmp.Clear();
            return receiveMessage;
        }

        private async void Run()
        {
            while (true)
            {
                await Task.Run(() => AcceptTcpClient());
                await Task.Run(() => ReceiveMessage());
            }
        }

        private void AcceptTcpClient()
        {
            while (true)
            {
                lock (lockObj)
                {
                    if (this.listener == null) return;

                    if (this.listener.Pending())
                    {
                        clients.Add(this.listener.AcceptTcpClient());
                        break;
                    }
                }
            }
        }

        private void ReceiveMessage()
        {
            clients = clients.Where(x => x != null).ToList();
            foreach (var client in clients) {
                //NetworkStreamを取得
                NetworkStream ns = client.GetStream();

                //クライアントから送られたデータを受信する
                Encoding enc = System.Text.Encoding.UTF8;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                byte[] resBytes = new byte[256];
                int resSize = 0;
                do
                {
                    //データの一部を受信する
                    resSize = ns.Read(resBytes, 0, resBytes.Length);
                    //Readが0を返した時はクライアントが切断したと判断
                    if (resSize == 0)
                    {
                        clients[clients.IndexOf(client)] = null;
                    }
                    //受信したデータを蓄積する
                    ms.Write(resBytes, 0, resSize);
                    //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                    // 受信を続ける
                } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');
                //受信したデータを文字列に変換
                string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
                ms.Close();
                //末尾の\nを削除
                resMsg = resMsg.TrimEnd('\n');

                this.receiveMessageTmp.Add(resMsg);
            }
        }
    }
}
