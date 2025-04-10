using FileReaderApp.FileDto;
using System.Xml;

namespace FileReaderApp.ReadFiles
{
    public class FileReader
    {
        //This method isn't work
        public static List<DataFile> ReadFromFile(string filePath)
        {
            List<DataFile> dataFiles = new();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                DataFile dataFile = new()
                {
                    //Date = parts[0],
                    Open = double.Parse(parts[1]),
                    High = double.Parse(parts[2]),
                    Low = double.Parse(parts[3]),
                    Close = double.Parse(parts[4]),
                    Volume = int.Parse(parts[5])
                };

                dataFiles.Add(dataFile);
            }
            return dataFiles;
        }

        public async static IAsyncEnumerable<DataFile> ReadFromFileAsEnumerable(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            char delimiter = fileExtension switch
            {
                ".csv" => ',',
                ".txt" => ';',
                _ => throw new NotSupportedException("File extension not supported.")
            };

            using StreamReader reader = new(filePath);
            string line;
            while ((line = await reader.ReadLineAsync()) is not null)
            {
                string[] parts = line.Split(delimiter);
                if (parts.Length != 6)
                {
                    // Log or handle invalid line format
                    continue;
                }

                if (!DateOnly.TryParse(parts[0], out DateOnly date) ||
                    !double.TryParse(parts[1], out double open) ||
                    !double.TryParse(parts[2], out double high) ||
                    !double.TryParse(parts[3], out double low) ||
                    !double.TryParse(parts[4], out double close) ||
                    !int.TryParse(parts[5], out int volume))
                {
                    // Log or handle parsing errors
                    continue;
                }

                yield return new DataFile
                {
                    Date = date,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume
                };
            }
        }

        public static async IAsyncEnumerable<DataFileXml> ReadXmlAsync(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                throw new DirectoryNotFoundException($"Directory not found: {filePath}");
            }

            XmlReaderSettings settings = new()
            {
                Async = true
            };

            var files = Directory.EnumerateFiles(filePath, "*.xml");

            foreach (var file in files)
            {
                using XmlReader reader = XmlReader.Create(file, settings);
                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "value")
                    {
                        var date = reader.GetAttribute("date");
                        var open = reader.GetAttribute("open");
                        var high = reader.GetAttribute("high");
                        var low = reader.GetAttribute("low");
                        var close = reader.GetAttribute("close");
                        var volume = reader.GetAttribute("volume");

                        yield return new DataFileXml
                        {
                            Date = DateOnly.Parse(date),
                            Open = double.Parse(open),
                            High = double.Parse(high),
                            Low = double.Parse(low),
                            Close = double.Parse(close),
                            Volume = int.Parse(volume)
                        };
                    }
                }
            }
        }

    }
}