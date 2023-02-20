using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.convert;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test
{
    public partial class Form1 : Form
    {
        private const uint IMF_DUALFONT = 0x80;
        private const uint WM_USER = 0x0400;
        private const uint EM_SETLANGOPTIONS = WM_USER + 120;
        private const uint EM_GETLANGOPTIONS = WM_USER + 121;
        [System.Runtime.InteropServices.DllImport("USER32.dll")]
        private static extern uint SendMessage(System.IntPtr hWnd, uint msg, uint wParam, uint lParam);
        public Form1()
        {
            InitializeComponent();
            uint lParam;
            lParam = SendMessage(this.richTextBox1.Handle, EM_GETLANGOPTIONS, 0, 0);
            lParam &= ~IMF_DUALFONT;
            SendMessage(this.richTextBox1.Handle, EM_SETLANGOPTIONS, 0, lParam);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConvertManagement c = new ConvertManagement(richTextBox1.Lines.ToList());
            string a = c.Convert();
            using (ScriptManagement s = new ScriptManagement(a, DConsole))
            {
                if (!s.hasError)
                {
                    s.Run();
                }
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {


            switch (e.KeyChar)
            {
                case (char)Keys.Enter: 
                    e.Handled = true;

                    //改行時に前行のインデントを維持する
					int spaces = 0;
					string line = richTextBox1.Lines[richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart-1)];
					foreach(char c in line)
					{
						if(char.IsWhiteSpace(c))
						{
							spaces++;
						}
						else
						{
							break;
						}
					}

					// 次の行に同じ数のスペースをコピーする
					richTextBox1.SelectedText = new string(' ', spaces);
					break;

                //自動的に置換する
                case (char)Keys.Tab: 
                    e.Handled = true;
                    richTextBox1.SelectedText = new string(' ', 4);
                    break;

                case ',':
                    e.Handled = true;
                    richTextBox1.SelectedText = "，";
                    break;

                case '+':
                    e.Handled = true;
                    richTextBox1.SelectedText = "＋";
                    break;

                case '-':
                    e.Handled = true;
                    richTextBox1.SelectedText = "－";
                    break;

                case '/':
                    e.Handled = true;
                    richTextBox1.SelectedText = "÷";
                    break;

                case '*':
                    e.Handled = true;
                    richTextBox1.SelectedText = "×";
                    break;

                case '[':
                    e.Handled = true;
                    richTextBox1.SelectedText = "［";
                    break;

                case ']':
                    e.Handled = true;
                    richTextBox1.SelectedText = "］";
                    break;

                case '{':
                    e.Handled = true;
                    richTextBox1.SelectedText = "｛";
                    break;

                case '}':
                    e.Handled = true;
                    richTextBox1.SelectedText = "｝";
                    break;

                case '(':
                    e.Handled = true;
                    richTextBox1.SelectedText = "（";
                    break;

                case ')':
                    e.Handled = true;
                    richTextBox1.SelectedText = "）";
                    break;

                case ':':
                    e.Handled = true;
                    richTextBox1.SelectedText = "：";
                    break;

                default:
                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DConsole.Text = "";
        }
    }
}