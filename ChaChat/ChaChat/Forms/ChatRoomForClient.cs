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
    public partial class ChatRoomForClient : ChatRoomFormBase
    {
        private Client client;
        private CancellationTokenSource cancellationTokenSource;
        private Task updateTask;

        private string txt = string.Empty;

        public ChatRoomForClient(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void ChatRoomForServerForm_Load(object sender, EventArgs e)
        {

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

                
                Thread.Sleep(1000);
            }
        }


        private void ChatRoomForServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // タスクを終了
            cancellationTokenSource.Cancel();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            client.Send(txtSendMessage.Text);
        }
    }
}
