using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace PracticalParallelization
{
    class ParallelForEachConcurrentDictionaryClass
    {
        public static IDictionary<string, uint> GetTopWordsParallelForEachConcurrentDictionary(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Initialize result dictionary
            var result = new ConcurrentDictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            // Loop each line in parallel
            Parallel.ForEach(
                File.ReadLines(InputFile.FullName),
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                (line, state, index) =>
                {
                    // Loop each word, filter seperators
                    foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        // Valid word
                        if (!TrackWordsClass.IsValidWord(word)) { continue; }
                        // Update word list
                        result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                    }
                }
            );
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
