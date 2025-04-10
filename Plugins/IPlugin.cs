namespace FileReaderApp.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        void Run();
    }
}
