using JustEmuTarkov.Diagnostics;

namespace JustEmuTarkov.ServerCommands
{
    class InvalidCommand : IServerCommand
    {
        public void Execute(string[] paramList)
        {
            Log.Error($"Unkown command {paramList[0]}!");
        }
    }
}
