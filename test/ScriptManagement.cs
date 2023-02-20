using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using test.convert;

namespace test
{
    internal class ScriptManagement:IDisposable
    {
        private ScriptOptions options { get; set; }
        private SharedData global { get; set; }

        private  Script csharpScript { get; set; }

        public bool hasError { get; set; } = false;


        public ScriptManagement(String ScriptText,TextBox Console)
        {
            options = ScriptOptions.Default.AddImports("System.Threading").AddReferences(typeof(CancellationToken).Assembly);
            global = new SharedData(Console);
            csharpScript = CSharpScript.Create(ScriptText, options, typeof(SharedData));

            Console.AppendText("\r\n");
            Console.AppendText("スクリプトをロードしました。\r\n");
            global.Console.AppendText("-----------------------------------------------\r\n");
            global.Console.AppendText(ScriptText + "\r\n") ;
            global.Console.AppendText("-----------------------------------------------\r\n");

            Console.AppendText("コンパイル開始...");

            try
            {
                //Compile
                ImmutableArray<Diagnostic> compileResult = csharpScript.Compile();                

                if (compileResult.IsEmpty)
                {
                    Console.AppendText("完了\r\n");
                }
                else
                {
                    foreach (Diagnostic item in compileResult)
                    {
                        if (item.ToString().Contains("error"))
                        {
                            hasError = true;
                            Console.AppendText("失敗\r\n");
                            break;
                        }
                    }
                    if (!hasError)
                    {
                        Console.AppendText("警告\r\n");
                    }

                    foreach (Diagnostic item in compileResult)
                    {
                        Console.AppendText(item.ToString() + "\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.AppendText("\r\n" +ex.Message + "\r\n");
            }
            finally
            {
                Console.AppendText("\r\n");
            }
        }
        public async void Run()
        {
            try
            {
                global.Console.AppendText("スクリプト実行\r\n");
                global.Console.AppendText("-----------------------------------------------\r\n");

                await csharpScript.RunAsync(globals:global);

                global.Console.AppendText("-----------------------------------------------\r\n");
                global.Console.AppendText("完了\r\n");
            }
            catch (OperationCanceledException ex)
            {
                global.Console.AppendText("タイムアウトしました\r\n");
            }
            catch (Exception ex)
            {
                global.Console.AppendText(ex.Message + "\r\n");
            }
            finally
            {
                global.Console.AppendText("\r\n");
            }
        }

        public void Dispose()
        {
            global.Dispose();
        }
    }

    public class SharedData : IDisposable
    {
        public SharedData(TextBox Console) 
        {
            this.Console = Console;
            this.cts = new CancellationTokenSource();
            this.Token = cts.Token;

            this.cts.CancelAfter(5000);
        }

        private CancellationTokenSource cts;
        public CancellationToken Token { get; }
        public TextBox Console { get; }

        public void Dispose()
        { 
            this.cts.Dispose();
        }
    }
}
