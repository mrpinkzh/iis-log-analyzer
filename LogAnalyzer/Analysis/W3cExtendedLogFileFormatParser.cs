using System.Collections.Generic;

namespace LogAnalyzer.Analysis
{
    /// <summary>
    /// The <see cref="W3cExtendedLogFileFormatParser"/> is capable of parsing lines
    /// with log information in the extended log file format defined by the W3c.
    /// https://www.w3.org/TR/WD-logfile.html
    /// </summary>
    public class W3cExtendedLogFileFormatParser : IParseLog
    {
        /// <summary>
        /// Parses the information in the specified <paramref name="lines"/> and returns it
        /// in form of  <see cref="LogItem"/>s.
        /// </summary>
        /// <param name="lines">A collection of strings containing log information.</param>
        /// <returns>A read only list of strings.</returns>
        public IReadOnlyList<LogItem> Parse(IReadOnlyCollection<string> lines)
        {
            return null;
        }
    }
}