using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    public class ParallelForEachMapReduceClass
    {
        public static IDictionary<string, uint> GetTopWordsParallelForEachMapReduce(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Initalize result dictionary
            var result = new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            // Loop through lines in parallel
            Parallel.ForEach(
                File.ReadLines(InputFile.FullName),
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                () => new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase),
                (line, state, index, localDic) =>
                {
                    // Loop through words, filter seperators
                    foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        // Valid word
                        if (!TrackWordsClass.IsValidWord(word)) { continue; }
                        // Update word list
                        TrackWordsClass.TrackWordsOccurrence(localDic, word);
                    }
                    return localDic;
                },
                localDic =>
                {
                    lock (result)
                    {
                        // Organize pairs
                        foreach (var pair in localDic)
                        {
                            var key = pair.Key;
                            // Increment matching keys
                            if (result.ContainsKey(key))
                            {
                                result[key] += pair.Value;
                            }
                            else
                            {
                                result[key] = pair.Value;
                            }
                        }
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
