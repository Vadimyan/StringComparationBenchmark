using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StringComparationBenchmark
{
	class Program
	{
		private const int dataLength = 1000000;
		private const int iterationsCount = 1000;
		private static Random random = new Random(42);

		static void Main(string[] args)
		{
			string[] data = Enumerable.Repeat(0, dataLength).Select(e => GetRandomString(100)).ToArray();
			string[] nonAsciiData = Enumerable.Repeat(0, dataLength).Select(e => GetRandomNonAsciiString(100)).ToArray();

			Warming(data, nonAsciiData);

			Stopwatch sw = new Stopwatch();

			StartCase(sw, "string.Equals");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for(int i = 0; i < dataLength; i++)
					string.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			IEqualityComparer<string> comparer;

			comparer = EqualityComparer<string>.Default;
			StartCase(sw, "EqualityComparer<string>.Default");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.Ordinal;
			StartCase(sw, "StringComparer.Ordinal");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.OrdinalIgnoreCase;
			StartCase(sw, "StringComparer.OrdinalIgnoreCase");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.OrdinalIgnoreCase;
			StartCase(sw, "StringComparer.OrdinalIgnoreCase non ASCII");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.CurrentCulture;
			StartCase(sw, "StringComparer.CurrentCulture");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.CurrentCulture;
			StartCase(sw, "StringComparer.CurrentCulture non ASCII");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.CurrentCultureIgnoreCase;
			StartCase(sw, "StringComparer.CurrentCultureIgnoreCase");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.CurrentCultureIgnoreCase;
			StartCase(sw, "StringComparer.CurrentCultureIgnoreCase non ASCII");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.InvariantCulture;
			StartCase(sw, "StringComparer.InvariantCulture");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.InvariantCulture;
			StartCase(sw, "StringComparer.InvariantCulture non ASCII");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.InvariantCultureIgnoreCase;
			StartCase(sw, "StringComparer.InvariantCultureIgnoreCase");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(data[i], data[dataLength - i - 1]);
			SummarizeCase(sw);

			comparer = StringComparer.InvariantCultureIgnoreCase;
			StartCase(sw, "StringComparer.InvariantCultureIgnoreCase non ASCII");
			for (int iterations = 0; iterations < iterationsCount; iterations++)
				for (int i = 0; i < dataLength; i++)
					comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);
			SummarizeCase(sw);

			Console.ReadKey();
		}

		private static void SummarizeCase(Stopwatch sw)
		{
			sw.Stop();
			Console.WriteLine("Total time: " + sw.ElapsedMilliseconds);
			Console.WriteLine("-----------------------------");
		}

		private static void StartCase(Stopwatch sw, string caseName)
		{
			Console.WriteLine(caseName);
			sw.Restart();
		}

		private static string GetRandomString(int length)
		{
			const string chars = "      ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private static string GetRandomNonAsciiString(int length)
		{
			const string chars = "      ¤¥µ¼½¾ξπℜ⇐⇑∞⊇المفاتيح العربيةW李克强bטקסטים מנוקדיםijklm叔父さんstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private static void Warming(string[] data, string[] nonAsciiData)
		{
			// string.Equals
			for (int i = 0; i < dataLength; i++)
				string.Equals(data[i], data[dataLength - i - 1]);

			IEqualityComparer<string> comparer;
			// EqualityComparer<string>.Default
			comparer = EqualityComparer<string>.Default;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.Ordinal
			comparer = StringComparer.Ordinal;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.OrdinalIgnoreCase
			comparer = StringComparer.OrdinalIgnoreCase;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.OrdinalIgnoreCase non ASCII
			comparer = StringComparer.OrdinalIgnoreCase;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);

			// StringComparer.CurrentCulture
			comparer = StringComparer.CurrentCulture;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.CurrentCulture non ASCII
			comparer = StringComparer.CurrentCulture;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);

			// StringComparer.CurrentCultureIgnoreCase
			comparer = StringComparer.CurrentCultureIgnoreCase;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.CurrentCultureIgnoreCase non ASCII
			comparer = StringComparer.CurrentCultureIgnoreCase;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);

			// StringComparer.InvariantCulture
			comparer = StringComparer.InvariantCulture;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.InvariantCulture non ASCII
			comparer = StringComparer.InvariantCulture;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);

			// StringComparer.InvariantCultureIgnoreCase
			comparer = StringComparer.InvariantCultureIgnoreCase;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(data[i], data[dataLength - i - 1]);

			// StringComparer.InvariantCultureIgnoreCase non ASCII
			comparer = StringComparer.InvariantCultureIgnoreCase;
			for (int i = 0; i < dataLength; i++)
				comparer.Equals(nonAsciiData[i], nonAsciiData[dataLength - i - 1]);
		}
	}
}
