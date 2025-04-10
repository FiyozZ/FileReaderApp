namespace FileReaderApp.Plugins.MethodInterface
{
    public interface IPluginManager
    {
        List<IPlugin> LoadPlugins(string directory);
    }
}
