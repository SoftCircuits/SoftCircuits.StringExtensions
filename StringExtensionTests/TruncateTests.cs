﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.StringExtensions;
using System.Collections.Generic;

namespace StringExtensionTests
{
    [TestClass]
    public class TruncateTests
    {
        class TruncateTest
        {
            public string Input { get; set; }
            public bool SmartTrim { get; set; }
            public bool AppendEllipsis { get; set; }
            public List<TruncateResult> Results { get; set; }

            public TruncateTest(string input, bool smartTrim, bool appendEllipsis)
            {
                Input = input;
                SmartTrim = smartTrim;
                AppendEllipsis = appendEllipsis;
                Results = new();
            }
        }

        class TruncateResult
        {
            public int MaxLength { get; set; }
            public string Result { get; set; }

            public TruncateResult(int maxLength, string result)
            {
                MaxLength = maxLength;
                Result = result;
            }
        }

        private readonly List<TruncateTest> TruncateTestData = new()
        {
            new("This is a test.", true, true)
            {
                Results = new()
                {
                    new(15, "This is a test."),
                    new(14, "This is a..."),
                    new(13, "This is a..."),
                    new(12, "This is a..."),
                    new(11, "This is..."),
                    new(10, "This is..."),
                    new(9, "This..."),
                    new(8, "This..."),
                    new(7, "This..."),
                    new(6, "Thi..."),
                    new(5, "Th..."),
                    new(4, "T..."),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                }
            },
            new("This is a test.", false, true)
            {
                Results = new()
                {
                    new(15, "This is a test."),
                    new(14, "This is a t..."),
                    new(13, "This is a ..."),
                    new(12, "This is a..."),
                    new(11, "This is ..."),
                    new(10, "This is..."),
                    new(9, "This i..."),
                    new(8, "This ..."),
                    new(7, "This..."),
                    new(6, "Thi..."),
                    new(5, "Th..."),
                    new(4, "T..."),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                }
            },
            new("This is a test.", true, false)
            {
                Results = new()
                {
                    new(15, "This is a test."),
                    new(14, "This is a test"),
                    new(13, "This is a"),
                    new(12, "This is a"),
                    new(11, "This is a"),
                    new(10, "This is a"),
                    new(9, "This is a"),
                    new(8, "This is"),
                    new(7, "This is"),
                    new(6, "This"),
                    new(5, "This"),
                    new(4, "This"),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                }
            },
            new("This is a test.", false, false)
            {
                Results = new()
                {
                    new(15, "This is a test."),
                    new(14, "This is a test"),
                    new(13, "This is a tes"),
                    new(12, "This is a te"),
                    new(11, "This is a t"),
                    new(10, "This is a "),
                    new(9, "This is a"),
                    new(8, "This is "),
                    new(7, "This is"),
                    new(6, "This i"),
                    new(5, "This "),
                    new(4, "This"),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                }
            }
        };

        [TestMethod]
        public void TestTruncate()
        {
            foreach (TruncateTest test in TruncateTestData)
            {
                foreach (TruncateResult result in test.Results)
                {
                    Assert.AreEqual(result.Result, StringExtensions.Truncate(test.Input, result.MaxLength, test.SmartTrim, test.AppendEllipsis));
                }
            }
        }
    }
}
