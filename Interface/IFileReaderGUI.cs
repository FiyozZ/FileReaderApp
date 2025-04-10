
namespace FileReaderApp.Interface
{
    public interface IFileReaderGUI
    {
        void CheckFolder(string folderPath);
        void AddLog(string fileName);
        void CopyFilesToLocalDirectory(string[] files, string destinationFolder);
        Task SelectedIndexChanged(int index);
        void ReadPlugins();
    }
}
