using FileReaderApp.Plugins.MethodInterface;
using System.Reflection;

namespace FileReaderApp.Plugins
{
    public class PluginManager : IPluginManager
    {
        public List<IPlugin> LoadPlugins(string directory)
        {
            try
            {
                List<IPlugin> plugins = new();

                foreach (string dll in Directory.GetFiles(directory, "*.dll"))
                {
                    Assembly assembly = Assembly.LoadFile(dll);
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.GetInterface("IPlugin") is not null)
                        {
                            IPlugin? plugin = Activator.CreateInstance(type) as IPlugin;
                            plugins.Add(plugin);
                        }
                    }
                }

                return plugins;
            }
            catch (ReflectionTypeLoadException ex)
            {

                throw ex.InnerException;
            }
        }
    }
}
