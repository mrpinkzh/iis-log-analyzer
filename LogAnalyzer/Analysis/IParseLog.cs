using System.Collections.Generic;

namespace LogAnalyzer.Analysis
{
    /// <summary>
    /// An implementer of <see cref="IParseLog"/> is capable of parsing lines
    /// with log information to <see cref="LogItem"/>s.
    /// </summary>
    public interface IParseLog
    {
        /// <summary>
        /// Parses the information in the specified <paramref name="lines"/> and returns it
        /// in form of  <see cref="LogItem"/>s.
        /// </summary>
        /// <param name="lines">A collection of strings containing log information.</param>
        /// <returns>A read only list of strings.</returns>
        IReadOnlyList<LogItem> Parse(IReadOnlyCollection<string> lines);
    }
}