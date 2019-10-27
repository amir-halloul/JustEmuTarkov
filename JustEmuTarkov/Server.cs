using System;
using System.Collections.Generic;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.Models;

namespace JustEmuTarkov
{
    class Server
    {
        private static List<EftClient> Clients;

        static void Main(string[] args)
        {
            Log.Initialize("server.log", LogLevel.All, false);
            Clients = new List<EftClient>();
            Log.Raw("========= JustEmuTarkov-NXT v0.0.1a =========", ConsoleColor.Cyan);
            HttpServer server = new HttpServer("http://192.168.154.1:1337/");
            server.Start();
            ServerCommandProcessor cmdProcessor = new ServerCommandProcessor();
        }

        public static int GetClientCount() => Clients.Count;

        public static List<EftClient> GetClients => Clients;

        public static EftClient GetClient(string sId)
        {
            if (string.IsNullOrEmpty(sId))
                return null;
            foreach (var client in Clients)
                if (client.UniqueSession.ToLower().Equals(sId.ToLower()))
                    return client;
            return null;
        }

        public static void AddClient(EftClient client)
        {
            if(client == null)
                return;
            if (GetClient(client.UniqueSession) != null)
                RemoveClient(client);
            Clients.Add(client);
        }

        public static void RemoveClient(EftClient client)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].UniqueSession == client.UniqueSession)
                {
                    Clients.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
