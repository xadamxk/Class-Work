using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    class PlinqNaiveClass
    {
        public static IDictionary<string, uint> GetTopWordsPLINQNaive(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Initalize words array by reading from file in paralll
            var words = File.ReadLines(InputFile.FullName)
                .AsParallel()
                .SelectMany(l => l.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                .Where(TrackWordsClass.IsValidWord);
            // Initialize results dictionary
            var result = new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var word in words)
            {
                // Track word
                TrackWordsClass.TrackWordsOccurrence(result, word);
            }

            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
