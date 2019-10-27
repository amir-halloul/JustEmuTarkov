using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustEmuTarkov.ServerCommands;

namespace JustEmuTarkov
{
    public class ServerCommandProcessor
    {
        public static Dictionary<string, IServerCommand> Commands = new Dictionary<string, IServerCommand>();
        private Thread _cmdThread;
        private bool _disposed = false;

        public ServerCommandProcessor()
        {
            Commands.Add("invalid", new InvalidCommand());
            Commands.Add("clients", new ClientsCommand());
            this._cmdThread = new Thread(ProcessInput);
            this._cmdThread.Start();
        }

        private void ProcessInput()
        {
            while (true)
            {
                string cmd = Console.ReadLine();
                string[] paramList = cmd.Split(' ');
                if(Commands.ContainsKey(paramList[0].ToLower()))
                    ProcessCommand(Commands[paramList[0].ToLower()], paramList);
                else
                    ProcessCommand(Commands["invalid"], paramList);
            }
        }

        private void ProcessCommand(IServerCommand cmd, string[] paramList)
        {
            Task.Run(() => cmd.Execute(paramList));
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed)
                return;

            if (disposing)
            {
                if (this._cmdThread != null)
                {
                    this._cmdThread.Abort();
                    this._cmdThread = null;
                }
            }
            this._disposed = true;
        }
    }
}
