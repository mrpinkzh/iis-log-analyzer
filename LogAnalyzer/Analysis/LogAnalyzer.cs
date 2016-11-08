using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer.Analysis
{
    /// <summary>
    /// The <see cref="LogAnalyzer"/> is capable of reading a stream with log information,
    /// analyze the content, parse it and return the information in form of a <see cref="Log"/>.
    /// </summary>
    public class LogAnalyzer
    {
        private readonly IReadLog reader;

        public LogAnalyzer(IReadLog reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Analyzes the information from the specified <paramref name="stream"/>, 
        /// parses it and returns it as <see cref="Log"/>.
        /// </summary>
        /// <param name="stream">A stream containing log information.</param>
        /// <returns>A <see cref="Log"/> instance.</returns>
        public Log Analyze(Stream stream)
        {
            IReadOnlyList<string> lines = this.reader.ReadLog(stream);
            return null;
        }
    }
}