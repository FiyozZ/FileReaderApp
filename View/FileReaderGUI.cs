using FileReaderApp.Interface;
using FileReaderApp.Plugins;
using FileReaderApp.ReadFiles;

namespace FileReaderApp
{
    public partial class FileReaderGUI : Form, IFileReaderGUI
    {
        private System.Windows.Forms.Timer timer;
        private readonly string folderPath;
        private readonly string DestinationFolder;
        long downloadLimit;
        private readonly HashSet<string> processedFiles = new();
        int selectedIndex;
        public FileReaderGUI()
        {
            //Readme
            //Change the dynamic switches from launchSettings.json
            InitializeComponent();
            CreatePluginsFolder();
            folderPath = Environment.GetEnvironmentVariable("FolderPath");
            DestinationFolder = Environment.GetEnvironmentVariable("DestinationFolder");
            downloadLimit = long.Parse(Environment.GetEnvironmentVariable("DownloadLimit"));
            timer = new System.Windows.Forms.Timer
            {
                Interval = int.Parse(Environment.GetEnvironmentVariable("LoopInterval"))
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void CreatePluginsFolder()
        {
            string pluginDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginDirectory))
            {
                Directory.CreateDirectory(pluginDirectory);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            CheckFolder(folderPath);
        }

        public void CopyFilesToLocalDirectory(string[] files, string destinationFolder)
        {
            long totalSize = 0;
            foreach (string file in files)
            {
                FileInfo fileInfo = new(file);
                totalSize += fileInfo.Length;
                if (totalSize <= downloadLimit)
                {
                    string fileName = Path.GetFileName(file);
                    string destinationFilePath = Path.Combine(destinationFolder, fileName);

                    File.Copy(file, destinationFilePath, true);
                }
                else
                {
                    MessageBox.Show("Passed the limit", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }

        private async void FileReaderGUI_Load(object sender, EventArgs e)
        {
            ReadPlugins();
            CheckFolder(folderPath);
            listBoxLog.SelectedIndex = 0;
            await SelectedIndexChanged(listBoxLog.SelectedIndex);
        }

        public async Task SelectedIndexChanged(int index)
        {
            string? filepath = listBoxLog.Items[index].ToString()?[22..];
            var path = $"{folderPath + "\\" + filepath}";
            string fileExtension = Path.GetExtension(path);
            if (fileExtension.Equals(".xml"))
            {
                DatasViw.Rows.Clear();
                var file = FileReader.ReadXmlAsync(folderPath);

                await foreach (var data in file)
                {
                    await Task.Delay(500);
                    DatasViw.Rows.Add(data.Date, data.Open, data.High, data.Low, data.Close, data.Volume);
                }
            }
            else
            {
                DatasViw.Rows.Clear();
                var file = FileReader.ReadFromFileAsEnumerable(path);
                await foreach (var data in file)
                {
                    await Task.Delay(500);
                    DatasViw.Rows.Add(data.Date, data.Open, data.High, data.Low, data.Close, data.Volume);
                }
            }
        }

        public void CheckFolder(string folderPath)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    if (!processedFiles.Contains(fileName))
                    {
                        AddLog(fileName);
                        processedFiles.Add(fileName);
                        CopyFilesToLocalDirectory(files, DestinationFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error occurred while checking folder: " + ex.Message);
            }
        }

        public void AddLog(string fileName)
        {
            string logMessage = $"[{DateTime.Now:dd:MM:yyyy HH:mm:ss}] {fileName}";
            // Invoke to update UI from a background thread
            Invoke(new Action(() =>
            {
                listBoxLog.Items.Add(logMessage);
                listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
            }));

            // Write to file
            File.AppendAllText("Log.txt", logMessage + Environment.NewLine);
        }

        private void FileReaderGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
        }

        private async void listBoxLog_Click(object sender, EventArgs e)
        {
            selectedIndex = listBoxLog.SelectedIndex;
            await SelectedIndexChanged(selectedIndex);
        }

        public void ReadPlugins()
        {
            string pluginDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            PluginManager pluginManager = new();
            List<IPlugin> plugins = pluginManager.LoadPlugins(pluginDirectory);

            foreach (IPlugin plugin in plugins)
            {
                var name = plugin.Name;
            }
        }

    }
}