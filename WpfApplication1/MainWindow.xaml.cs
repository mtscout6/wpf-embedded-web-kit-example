using System;
using System.Windows;
using Awesomium.Core;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            browser.CreateObject("AppGlobal");
            browser.SetObjectCallback("AppGlobal", "Execute", (sender, args) =>
            {
                Console.WriteLine("Called From Browser");
                foreach (var argument in args.Arguments)
                {
                    SendMessageToBrowser("Message Received By Application: " + argument.ToString());
                }
            });

            browser.LoadCompleted += (sender, args) => SendMessageToBrowser("Browser wired to application!");

            browser.JSConsoleMessageAdded += (sender, args) =>
            {
                Console.WriteLine("Browser Console {0}-> {1}\n\t{2}", args.LineNumber, args.Message, args.Source);
            };

            browser.Crashed += (sender, args) => Console.WriteLine(args);

            browser.LoadURL("file:///C:/dev/Learning/WPF-EmbeddedBrowser/WpfApplication1/bin/Debug/test.html");
        }

        private void SendMessageToBrowser(string message)
        {
            browser.CallJavascriptFunction("AppGlobal", "sendMessage", new JSValue(message));
        }
    }
}
