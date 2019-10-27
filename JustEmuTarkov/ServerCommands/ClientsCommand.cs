using JustEmuTarkov.Diagnostics;

namespace JustEmuTarkov.ServerCommands
{
    public class ClientsCommand : IServerCommand
    {
        public void Execute(string[] paramList)
        {
            Log.Raw($"Connected clients({Server.GetClientCount()}):");
            foreach (var client in Server.GetClients)
            {
                string authenticated = client.Authenticated ? "Authenticated" : "Not authenticated";
                Log.Raw($"{client.UniqueSession}: {client.Name} ({authenticated})");
            }
        }
    }
}
