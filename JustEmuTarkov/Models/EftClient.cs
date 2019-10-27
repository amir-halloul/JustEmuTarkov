namespace JustEmuTarkov.Models
{
    public class EftClient
    {
        public string Name { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public string UniqueSession { get; set; }
        public string Email { get; set; }
        public bool Authenticated { get; set; }
        public string ClientVersion { get; set; }
        public EftClient(string uniqueSession, string uniqueIdentifier)
        {
            UniqueSession = uniqueSession;
            UniqueIdentifier = uniqueIdentifier;
        }
    }
}
