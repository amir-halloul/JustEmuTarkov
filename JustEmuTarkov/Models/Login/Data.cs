namespace JustEmuTarkov.Models.Login
{
    public class Data
    {
        public string Token { get; set; }
        public int Aid { get; set; }
        public string Lang { get; set; }
        public Languages Languages { get; set; }
        public bool NdaFree { get; set; }
        public bool Queued { get; set; }
        public int Taxonomy { get; set; }
        public string ActiveProfileId { get; set; }
        public Backend Backend { get; set; }
        public uint UtcTime { get; set; }
        public int TotalInGame { get; set; }
        public bool TwitchEventMember { get; set; }
    }
}
