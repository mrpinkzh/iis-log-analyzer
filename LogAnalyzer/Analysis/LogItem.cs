namespace LogAnalyzer.Analysis
{
    public class LogItem
    {
        private readonly string ip;

        public LogItem(string ip)
        {
            this.ip = ip;
        }

        public string Ip => ip;
    }
}