using System.Collections.Generic;
using LogAnalyzer.Analysis;

namespace LogAnalyzer.Models
{
    public class AnalysisResult
    {
        private readonly IReadOnlyCollection<ClientResult> clientResults;
        private readonly string message;

        public AnalysisResult(IReadOnlyCollection<ClientResult> clientResults, string message)
        {
            this.clientResults = clientResults;
            this.message = message;
        }

        public IReadOnlyCollection<ClientResult> ClientResults => clientResults;

        public string Message => message;
    }
}