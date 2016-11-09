namespace LogAnalyzer.Analysis
{
    /// <summary>
    /// An implementer of <see cref="IResolveNames"/> is capable of resolving
    /// names for ip addresses.
    /// </summary>
    public interface IResolveNames
    {
        /// <summary>
        /// Resolves the name for the specified <paramref name="ip"/>.
        /// </summary>
        /// <param name="ip">The requested ip address.</param>
        /// <returns>A domain name.</returns>
        string ResolveName(string ip);
    }
}