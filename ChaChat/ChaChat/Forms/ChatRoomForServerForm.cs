using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using UtilityTCP;

namespace ChaChat.Forms
{
    public partial class ChatRoomForServerForm : ChatRoomFormBase
    {
        private Server server;
        private CancellationTokenSource cancellationTokenSource;
        private Task updateTask;

        private string txt = string.Empty;

        public ChatRoomForServerForm(Server server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void ChatRoomForServerForm_Load(object sender, EventArgs e)
        {
            server.Start();

            cancellationTokenSource = new CancellationTokenSource();
            var cancelToken = cancellationTokenSource.Token;
            // 更新処理開始
            updateTask = Task.Run(() => StartUpdate(cancelToken));
        }

        /// <summary>
        /// 更新処理開始
        /// </summary>
        /// <param name="cancelToken"></param>
        private void StartUpdate(CancellationToken cancelToken)
        {
            //更新
            while (true)
            {
                //　終了？
                if (cancelToken.IsCancellationRequested)
                {
                    // キャンセルされたらTaskを終了する.
                    return;
                }

                // サーバー稼働状態更新
                this.Invoke(new Action(this.GetServerReceivedMessage));
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 受信したメッセージがあれば取得
        /// </summary>
        public void GetServerReceivedMessage()
        {
            var getMsg = server.GetMessage();
            if (!getMsg.Any()) return;
            base.txtChat.Text += "\n" + string.Join("\n", getMsg);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChatRoomForServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
            // タスクを終了
            cancellationTokenSource.Cancel();
        }
    }
}
