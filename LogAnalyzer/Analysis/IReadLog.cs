using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer.Analysis
{
    /// <summary>
    /// An implementer of <see cref="IReadLog"/> is capable of reading log information from
    /// a stream.
    /// Log information is: text, line by line.
    /// </summary>
    public interface IReadLog
    {
        /// <summary>
        /// Reads the log information from the specified <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The stream containing the log information.</param>
        /// <returns>A read only list of strings.</returns>
        IReadOnlyList<string> ReadLog(Stream stream);
    }
}