using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer.Analysis
{
    public class SimpleLogReader : IReadLog
    {
        public IReadOnlyList<string> ReadLog(Stream stream)
        {
            var lines = new List<string>();
            string line;

            var streamReader = new StreamReader(stream);
            while ((line = streamReader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            streamReader.Close();
            stream.Seek(0, SeekOrigin.Begin);
            return lines;
        }
    }
}