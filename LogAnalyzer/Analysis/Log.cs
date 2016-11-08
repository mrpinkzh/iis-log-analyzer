using System.Collections.Generic;

namespace LogAnalyzer.Analysis
{
    public class Log
    {
        private readonly IReadOnlyCollection<LogItem> items;

        public Log(IReadOnlyCollection<LogItem> items)
        {
            this.items = items;
        }

        public IReadOnlyCollection<LogItem> Items => items;
    }
}