using System.Collections.Generic;
using System.Linq;
using ExAs;
using FluentAssertions;
using Xunit;

namespace LogAnalyzer.Analysis
{
    public class W3cExtendedLogFileFormatParserTests
    {
        private static readonly IReadOnlyList<string> twoLines
            = new List<string>
            {
                "2016-02-15 09:30:24 10.10.2.18 GET / - 443 - 212.120.32.82 Mozilla/5.0+(Macintosh;+Intel+Mac+OS+X+10_11_3)+AppleWebKit/601.4.4+(KHTML,+like+Gecko) - 200 0 995 40478",
                "2016-02-15 09:58:20 10.10.2.18 GET /Scripts/jquery.validate.unobtrusive.min.js - 443 - 83.150.38.202 Mozilla/5.0+(Windows+NT+6.3;+WOW64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/48.0.2564.109+Safari/537.36"
            };

        private readonly IParseLog parser;

        public W3cExtendedLogFileFormatParserTests()
        {
            parser = new W3cExtendedLogFileFormatParser();
        }

        [Fact]
        public void Parse_WithTwoValidLines_ShouldReturnBothItemsWithCorrectIp()
        {
            var logItems = parser.Parse(twoLines);

            logItems.Should().NotBeNullOrEmpty();
            logItems.Should().Contain(item => item.Ip == "212.120.32.82")
                         .And.Contain(item => item.Ip == "83.150.38.202")
                         .And.HaveCount(2);
        }

        [Fact]
        public void Parse_WithThreeValidLines_ShouldReturnAllItemsWithCorrectIps()
        {
            var logItems = parser.Parse(threeLines());

            logItems.Should().Contain(item => item.Ip == "212.120.32.82")
                         .And.Contain(item => item.Ip == "83.150.38.202")
                         .And.Contain(item => item.Ip == "85.17.156.76")
                         .And.HaveCount(2);
        }

        private static IReadOnlyList<string> threeLines()
        {
            return new List<string>(twoLines)
            {
                "2016-02-15 09:58:21 10.10.2.18 GET /content/images/thumbs/0004425_fruchte-gemuse_450.jpeg - 443 - 85.17.156.76 Mozilla/5.0+(Windows+NT+6.3;+WOW64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/48.0.2564.109+Safari/537.36 https://10.10.10.10/ 200 0 0 24"
            };
        }
    }
}