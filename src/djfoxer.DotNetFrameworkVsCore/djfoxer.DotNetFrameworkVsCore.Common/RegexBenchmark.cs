﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.IO;
using System.Text.RegularExpressions;

namespace djfoxer.DotNetFrameworkVsCore.Common
{
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
   // [RPlotExporter]
    [CsvMeasurementsExporter]
    [MarkdownExporterAttribute.GitHub]
    public class RegexBenchmark
    {
        string _commonInput = string.Empty;
        Regex _regexEmail, _regexStrongPassword, _regexSpanSearching, _regexBackTracking;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            _commonInput = File.ReadAllText("InputFiles/CSharpUseDeconstructionDiagnosticAnalyzer.cs.txt");

            _regexEmail = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.Compiled);
            _regexStrongPassword = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", RegexOptions.Compiled);
            _regexSpanSearching = new Regex("([ab]cd|ef[g-i])jklm", RegexOptions.Compiled);
            _regexBackTracking = new Regex("a*a*a*a*a*a*a*b", RegexOptions.Compiled);
        }

        [Benchmark]
        public void Regex_Email()
        {
            _regexEmail.IsMatch(_commonInput);
        }

        [Benchmark]
        public void Regex_StrongPassword()
        {
            _regexStrongPassword.IsMatch(_commonInput);
        }

        [Benchmark]
        public void Regex_SpanSearching()
        {
            _regexSpanSearching.IsMatch(_commonInput);
        }

        [Benchmark]
        public void Regex_BackTracking()
        {
            _regexBackTracking.IsMatch("aaaaaaaaaaaaaaaaaaaaa");
        }
    }
}
