using JustEmuTarkov.Models;
using JustEmuTarkov.Models.Login;
using JustEmuTarkov.Utility;

namespace JustEmuTarkov.Services
{
    public class AuthenticationService
    {
        public static string Authenticate(ref EftClient client, string data)
        {
            string result = string.Empty;
            var login = Serializer.Deserialize<LoginRequest>(data);
            var sid = StringUtil.GenerateRandomString(16, true);
            if (client == null)
                client = new EftClient(sid, login.DeviceId);
            else
            {
                client.UniqueIdentifier = login.DeviceId;
                client.UniqueSession = sid;
            }
            client.Authenticated = true;
            client.Email = login.Email;
            client.Password = login.Pass;
            Server.AddClient(client);
            return Serializer.Read("./Templates/Account.json");
        }
    }
}
