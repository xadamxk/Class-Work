using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace PracticalParallelization
{
    public class PLINQConcurrentDictionaryClass
    {
        
        public static IDictionary<string, uint> GetTopWordsPLINQConcurrentDictionary(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Initalize result dictionary
            var result = new ConcurrentDictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            // Read from file by line in parallel
            File.ReadLines(InputFile.FullName)
                .AsParallel()
                .ForAll(line =>
                {
                    // Loop through each word, filter seperators
                    foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        // Valid word
                        if (!TrackWordsClass.IsValidWord(word)) { continue; }
                        // Update word list
                        result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                    }
                });
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
