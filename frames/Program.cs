using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frames
{
    class Program
    {
        Core core = new Core();
        Config user;

        IConfigFactory configFactory = new ConfigFactory();
        IConfigHolder configHolder = new BinarySerializer();
        IConfigManager configManager;

        static void Main(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            Program p = new Program();
            p.Begin(args);
        }

        private void Begin(string[] args)
        {
            configManager = new ConfigManager(
                configFactory, 
                configHolder);

            Console.CancelKeyPress += Console_CancelKeyPress;
            Console.Title = "frames";
            Console.ForegroundColor = ConsoleColor.Cyan;

            core.CallBack += Core_CallBack;

            user = configManager.InitConfig();

            if (args.Length > 0)    //file mode
            {
                try
                {
                    core.Transform(args, (user.CreateFolder ? user.FolderName : null));

                    Console.WriteLine("done");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }
            SayHello();
            Loop();
        }

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            configManager.Save(user);
        }

        private void SayHello()
        {
            Console.WriteLine("frames");
            Console.WriteLine("cr2 => jpg converter");
            Console.WriteLine();
            Console.WriteLine("current settings:");
            Console.WriteLine("create folder: " + user.CreateFolder);
            Console.WriteLine("foldername: " + user.FolderName);
            Console.WriteLine();
            Console.WriteLine("help:");
            Console.WriteLine("'-c' turns on/off creating subfolder");
            Console.WriteLine("'-f ****' to change folder name");
            Console.WriteLine("or type filename and press enter");
        }

        private void Loop()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (CheckInput(input)) continue;    // waiting for commands to change user profile
                    core.Transform(input, (user.CreateFolder ? user.FolderName : null));              // transform single file mode
                    Console.WriteLine("done");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private bool CheckInput(string input)
        {
            if (input.ToLower() == "-c")
            {
                user.CreateFolder = !user.CreateFolder;
                Console.WriteLine("creating subfolder: " + user.CreateFolder);
                Console.WriteLine("subfolder name: " + user.FolderName);
                return true;
            }
            else if (input.ToLower().Contains("-f "))
            {
                input = input.Replace("-f ", "").Trim();
                user.FolderName = input != "" ? input : "jpg";  // to reset name
                Console.WriteLine("subfolder name: " + user.FolderName);
                return true;
            }
            return false;
        }

        private void Core_CallBack(object sender, MyEventArgs e)
        {
            if(e.Message != null)
                Console.WriteLine("processing file: " + e.Message);
        }
    }
}
