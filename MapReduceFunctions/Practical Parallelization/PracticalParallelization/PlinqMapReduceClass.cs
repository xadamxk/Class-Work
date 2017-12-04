using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    public class PlinqMapReduceClass
    {
        public static IDictionary<string, uint> GetTopWordsPLINQMapReduce(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Return ordered dictionary
            return File.ReadLines(InputFile.FullName)
                .AsParallel()
                // Let C# decide max degree
                //.WithDegreeOfParallelism(12)
                .Aggregate(
                    () => new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase), //#1
                    (localDic, line) => //#2
                    {
                        // Ignore seperator characters
                        foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                        {
                            // Check word is blacklist
                            if (!TrackWordsClass.IsValidWord(word)) { continue; }
                            // Track word
                            TrackWordsClass.TrackWordsOccurrence(localDic, word);
                        }
                        return localDic;
                    },
                    // Take result and sort by key/value pair
                    (finalResult, localDic) => //#3
                    {
                        foreach (var pair in localDic)
                        {
                            var key = pair.Key;
                            if (finalResult.ContainsKey(key))
                            {
                                finalResult[key] += pair.Value;
                            }
                            else
                            {
                                finalResult[key] = pair.Value;
                            }
                        }
                        return finalResult;
                    },
                    // Return ordered dictionary
                    finalResult => finalResult //#4
                        .OrderByDescending(kv => kv.Value)
                        .Take((int)TopCount)
                        .ToDictionary(kv => kv.Key, kv => kv.Value)
                );
        }
    }
}
