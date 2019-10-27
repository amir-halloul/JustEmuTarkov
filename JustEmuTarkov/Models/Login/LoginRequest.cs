namespace JustEmuTarkov.Models.Login
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Pass { get; set; }
        public Version Version { get; set; }
        public string DeviceId { get; set; }
        public bool Develop { get; set; }
        public int Sec { get; set; }
    }
}
