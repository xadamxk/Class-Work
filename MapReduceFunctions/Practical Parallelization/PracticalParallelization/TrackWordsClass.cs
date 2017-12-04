using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    public class TrackWordsClass
    {
        public static void TrackWordsOccurrence(IDictionary<string, uint> wordCounts, string word)
        {
            uint count;
            // If existing, increment count
            if (wordCounts.TryGetValue(word, out count))
            {
                wordCounts[word] = count + 1;
            }
            // Else, make count 1
            else
            {
                wordCounts[word] = 1;
            }
        }

        public static bool IsValidWord(string word) => !Program.InvalidCharacters.Contains(word[0]) && !Program.StopWords.Contains(word);
    }
}
