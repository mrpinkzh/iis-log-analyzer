﻿using System;
using System.Collections.Generic;
using System.Linq;
using LogAnalyzer.Infrastructure;

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
            Func<string, bool> fieldsIdentifier = line => line.StartsWith("#Fields");
            var logSections = lines.Sectionize(fieldsIdentifier);
            if (!logSections.Any())
            {
                throw new InvalidOperationException(
                            $"Tried to parse log information from text file.{Environment.NewLine}" +
                            $"The system could not find a fields declaration line.{Environment.NewLine}" +
                            $"In order to analyse the log file the system needs to know which fields are logged. Please add a line starting with '#Fields: '.");
            }
            return logSections.SelectMany(logSection =>
                {
                    List<string> logSectionLines = logSection.ToList();
                    int indexOfClientIp = logSectionLines.First(fieldsIdentifier)
                                                         .Split(new[] {' '}, StringSplitOptions.None)
                                                         .Skip(1)
                                                         .IndexOfFirst("c-ip");
                    var logItems = logSectionLines.Where(line => !line.StartsWith("#"))
                                                  .Select(line => line.Split(new[] {' '}, StringSplitOptions.None))
                                                  .Select(fields => new LogItem(fields[indexOfClientIp]));
                    return new List<LogItem>(logItems);
                }
            ).ToList();
        }
    }
}