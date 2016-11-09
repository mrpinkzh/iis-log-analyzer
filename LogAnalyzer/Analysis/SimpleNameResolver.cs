using System.Net;

namespace LogAnalyzer.Analysis
{
    /// <summary>
    /// The <see cref="SimpleNameResolver"/> is capable of resolving FQDNs for
    /// given ip addresses using the <see cref="Dns"/> component.
    /// </summary>
    public class SimpleNameResolver : IResolveNames
    {
        /// <summary>
        /// Resolves the name for the specified <paramref name="ip"/>.
        /// </summary>
        /// <param name="ip">The requested ip address.</param>
        /// <returns>A domain name or an exception message.</returns>
        public string ResolveName(string ip)
        {
            IPHostEntry ipHostEntry = Dns.GetHostEntry(ip);
            if (ipHostEntry == null)
                return "not resolvable";
            return ipHostEntry.HostName;
        }
    }
}