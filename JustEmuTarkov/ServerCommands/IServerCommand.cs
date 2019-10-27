namespace JustEmuTarkov.ServerCommands
{
    public interface IServerCommand
    {
        void Execute(string[] paramList);
    }
}
