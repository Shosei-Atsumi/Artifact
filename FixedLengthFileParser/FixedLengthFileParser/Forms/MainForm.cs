using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using FixedLengthFileParser.Utility;

namespace FixedLengthFileParser.Forms
{
    public partial class MainForm : MetroForm
    {

        private ParserUtility parser = new ParserUtility();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoadFomat_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkCheckFormatLoad.Checked)
                {
                    DialogResult result = MessageBox.Show("フォーマットをロードしますか？",
                        "確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                string inputText = "";
                // クリップボードからオブジェクトを取得する。
                IDataObject clipboard = Clipboard.GetDataObject();

                // テキストデータかどうか確認する。
                if (clipboard.GetDataPresent(DataFormats.Text))
                {
                    // オブジェクトからテキストを取得する。
                    inputText = (string)clipboard.GetData(DataFormats.Text);
                }
                else
                {
                    MessageBox.Show("コピーしたデータが文字列ではありません。");
                }

                var rawBlocks = inputText.Split('\n').ToList();
                List<(int, string)> blocks = new List<(int, string)>();

                rawBlocks.Remove(rawBlocks[rawBlocks.Count - 1]);

                foreach (var rawBlock in rawBlocks)
                {
                    var block = rawBlock.Split('\t');
                    blocks.Add((int.Parse(block[1]), block[0]));
                }

                parser.LoadFileFormat(blocks);

                MessageBox.Show("ロードしました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                string inputText = "";
                // クリップボードからオブジェクトを取得する。
                IDataObject clipboard = Clipboard.GetDataObject();

                // テキストデータかどうか確認する。
                if (clipboard.GetDataPresent(DataFormats.Text))
                {
                    // オブジェクトからテキストを取得する。
                    inputText = (string)clipboard.GetData(DataFormats.Text);
                }
                else
                {
                    MessageBox.Show("コピーしたデータが文字列ではありません。");
                }

                inputText = inputText.TrimEnd('\n');

                var lines = inputText.Split('\n').ToList();

                string res = String.Empty;

                foreach (var block in parser.fileFormat)
                {
                    res += block.name + "\t";
                }

                res += "\n";

                foreach (var line in lines)
                {
                    int starLength = 0;
                    foreach (var block in parser.fileFormat)
                    {
                        res += line.Substring(starLength, block.length) + "\t";
                        starLength += block.length;
                    }

                    if (line.Length > starLength) res += line.Substring(starLength) + "\t";
                    res = res.TrimEnd('\t');
                    res += "\n";
                }

                Clipboard.Clear();
                Clipboard.SetData(DataFormats.Text, res as object);

                MessageBox.Show("クリップボードへ出力しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
