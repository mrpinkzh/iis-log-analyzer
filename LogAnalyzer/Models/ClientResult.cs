namespace LogAnalyzer.Models
{
    public class ClientResult
    {
        private readonly string ip;
        private readonly int count;

        public ClientResult(string ip, int count)
        {
            this.ip = ip;
            this.count = count;
        }

        public string Ip => ip;

        public int Count => count;
    }
}