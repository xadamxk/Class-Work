using Easy.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    class ProducerConsumerEasierClass
    {
        public static IDictionary<string, uint> GetTopWordsProducerConsumerEasier(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Limitations
            const int WorkerCount = 12;
            const int BoundedCapacity = 10000;
            var result = new ConcurrentDictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);

            // Declare the worker
            Action<string> work = line =>
            {
                // Loop through words in line, filter seperators
                foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Valid word
                    if (!TrackWordsClass.IsValidWord(word)) { continue; }
                    // Update word list
                    result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                }
            };

            // Setup the queue
            var pcq = new ProducerConsumerQueue<string>(work, WorkerCount, BoundedCapacity);
            pcq.OnException += (sender, ex) => Console.WriteLine("Oooops: " + ex.Message);

            // Begin producing
            foreach (var line in File.ReadLines(InputFile.FullName))
            {
                pcq.Add(line);
            }
            pcq.CompleteAdding();
            // End of producing

            // Wait for workers to finish their work
            pcq.Completion.Wait();
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
