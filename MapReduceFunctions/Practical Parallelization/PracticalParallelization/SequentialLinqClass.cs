using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    class SequentialLinqClass
    {
        public static IDictionary<string, uint> GetTopWordsSequentialLINQ(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Return ordered dictionary
            return File.ReadLines(InputFile.FullName)
                .SelectMany(l => l.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                .Where(TrackWordsClass.IsValidWord)
                .ToLookup(x => x, StringComparer.InvariantCultureIgnoreCase)
                .Select(x => new { Word = x.Key, Count = (uint)x.Count() })
                .OrderByDescending(kv => kv.Count)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Word, kv => kv.Count);
        }
    }
}
