using System;
using System.Net;

namespace LogAnalyzer.Analysis
{
    public class NameResolver
    {
        public string ResolveName(string ip)
        {
            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostEntry(ip);
                if (ipHostEntry == null)
                    return "not resolvable";
                return ipHostEntry.HostName;
            }
            catch (Exception)
            {
                return "error on resolve";
            }
        }
    }
}