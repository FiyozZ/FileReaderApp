using System.Xml.Serialization;

namespace FileReaderApp.FileDto
{
    /*[XmlRoot("values")]
    public class DataFilesXml
    {
        [XmlElement("value")]
        public List<DataFileXml> DataFiles { get; set; }
    }*/

    public class DataFileXml
    {
        public DateOnly Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
    }

}
