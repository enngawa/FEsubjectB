using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
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
                case (char)Keys.Tab: e.Handled = true;
                    richTextBox1.SelectedText = new string(' ', 4);
                    break;

                case ',':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�C";
                    break;

                case '+':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�{";
                    break;

                case '-':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�|";
                    break;

                case '/':
                    e.Handled = true;
                    richTextBox1.SelectedText = "��";
                    break;

                case '*':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�~";
                    break;

                case '[':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�m";
                    break;

                case ']':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�n";
                    break;

                case '{':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�o";
                    break;

                case '}':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�p";
                    break;

                case '(':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�i";
                    break;

                case ')':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�j";
                    break;

                case ':':
                    e.Handled = true;
                    richTextBox1.SelectedText = "�F";
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