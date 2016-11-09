using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LogAnalyzer.Analysis
{
    public class W3cExtendedLogFileFormatParserTests
    {
        private static readonly IReadOnlyList<string> twoLines
            = new List<string>
            {
                "#Fields: date time s-ip cs-method cs-uri-stem cs-uri-query s-port cs-username c-ip cs(User-Agent) cs(Referer) sc-status sc-substatus sc-win32-status time-taken",
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
                         .And.HaveCount(3);
        }

        [Fact]
        public void Parse_WithCommentedLine_ShouldNotParseCommentedLine()
        {
            var withCommentedLine = new List<string>(twoLines)
            {
                "#Any comment"
            };

            var logItems = parser.Parse(withCommentedLine);

            logItems.Should().Contain(item => item.Ip == "212.120.32.82")
                         .And.Contain(item => item.Ip == "83.150.38.202")
                         .And.HaveCount(2);
        }

        [Fact]
        public void Parse_WithSimpleLog_ShouldParseClientIpOnCorrectPlace()
        {
            var simpleLogLines = new List<string>
            {
                "#Fields: cs-method c-ip sc-status",
                "GET 192.168.1.110 200",
                "POST 10.0.0.1 404"
            };

            var logItems = parser.Parse(simpleLogLines);

            logItems.Should().Contain(item => item.Ip == "192.168.1.110")
                         .And.Contain(item => item.Ip == "10.0.0.1")
                         .And.HaveCount(2);
        }

        [Fact]
        public void Parse_WithTwoDifferentLogFormats_ShouldParseBothAndUnifyOutput()
        {
            var twoDifferentFormats = new List<string>
            {
                "#Fields: cs-method c-ip sc-status",
                "GET 192.168.1.110 200",
                "#Fields: c-ip date",
                "10.0.0.1 16.11.2016"
            };

            var logItems = parser.Parse(twoDifferentFormats);

            logItems.Should().Contain(item => item.Ip == "192.168.1.110")
                         .And.Contain(item => item.Ip == "10.0.0.1")
                         .And.HaveCount(2);
        }

        [Fact]
        public void Parse_WithoutFieldsDefinition_ShouldThrow()
        {
            var lines = new List<string>
            {
                "GET 192.168.1.110 200",
                "POST 10.0.0.1 404"
            };

            parser.Invoking(p => p.Parse(lines)).ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Parse_WithLinesNoPrecededByFieldsDefinition_ShouldIgnoreTheseLines()
        {
            var lines = new List<string>
            {
                "GET 192.168.1.110 200",
                "#Fields: c-ip date",
                "10.0.0.1 16.11.2016"
            };

            var logItems = parser.Parse(lines);

            logItems.Should().NotContain(item => item.Ip == "192.168.1.110")
                         .And.Contain(item => item.Ip == "10.0.0.1")
                         .And.HaveCount(1);
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