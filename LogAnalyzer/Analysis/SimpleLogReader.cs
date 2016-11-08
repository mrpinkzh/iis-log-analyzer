using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer.Analysis
{
    public class SimpleLogReader : IReadLog
    {
        public IReadOnlyList<string> ReadLog(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}