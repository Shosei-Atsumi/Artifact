using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTCP
{
    public class Client
    {
        private static readonly string DEFAULT_CONNECT_IP_ADRESS = "127.0.0.1";
        private static readonly int DEFAULT_CONNECT_PORT = 22222;

        TcpClient client;

        // 許可するIP
        private string connectIpAdress;
        // ポート
        private int connectPort;

        public Client(string ipAddress, int listenPort)
        {
            this.connectIpAdress = ipAddress;
            this.connectPort = listenPort;
        }

        public Client(string connectIpAdress)
            : this(connectIpAdress, DEFAULT_CONNECT_PORT)
        {

        }

        public Client(int connectPort)
            : this(DEFAULT_CONNECT_IP_ADRESS, connectPort)
        {

        }

        /// <summary>
        /// Listenを開始する
        /// </summary>
        public void Send(string sendMsg)
        {


            if (sendMsg == null || sendMsg.Length == 0)
            {
                return;
            }

            //TcpClientを作成し、サーバーと接続する
           if(client == null) client = new TcpClient(this.connectIpAdress, this.connectPort);


            //NetworkStreamを取得する
            NetworkStream ns = client.GetStream();

            try
            {
                //読み取り、書き込みのタイムアウトを10秒にする
                //デフォルトはInfiniteで、タイムアウトしない
                //(.NET Framework 2.0以上が必要)
                ns.ReadTimeout = 10000;
                ns.WriteTimeout = 10000;

                //サーバーにデータを送信する
                //文字列をByte型配列に変換
                System.Text.Encoding enc = System.Text.Encoding.UTF8;
                byte[] sendBytes = enc.GetBytes(sendMsg + '\n');
                //データを送信する
                ns.Write(sendBytes, 0, sendBytes.Length);
            }
            finally
            {
                //閉じる
                ns.Close();
            }
        }
    }
}
