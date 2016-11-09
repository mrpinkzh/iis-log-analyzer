namespace LogAnalyzer.Models
{
    public class ClientResult
    {
        private readonly string ip, name;
        private readonly int count;

        public ClientResult(string ip, string name, int count)
        {
            this.ip = ip;
            this.name = name;
            this.count = count;
        }

        public string Ip => ip;

        public string Name => name;

        public int Count => count;
    }
}