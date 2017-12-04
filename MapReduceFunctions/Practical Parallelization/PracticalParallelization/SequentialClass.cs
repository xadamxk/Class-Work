using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    public class SequentialClass
    {

        public static IDictionary<string, uint> GetTopWordsSequential(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Initialize Result Dictionary
            var result = new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            // Loop through lines in file
            foreach (var line in File.ReadLines(InputFile.FullName))
            {
                // Loop through words in lines
                foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Check word in blacklist
                    if (!TrackWordsClass.IsValidWord(word)) { continue; }
                    // Track word
                    TrackWordsClass.TrackWordsOccurrence(result, word);
                }
            }
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

    }
}
