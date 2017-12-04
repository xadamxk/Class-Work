using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace PracticalParallelization
{
    public class PLINQProducerConsumerClass
    {
        public static IDictionary<string, uint> GetTopWordsPLINQProducerConsumer(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Limtations
            const int WorkerCount = 12;
            const int BoundedCapacity = 10000;
            // Initalize result dictionary
            var result = new ConcurrentDictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);

            // Setup the queue
            var blockingCollection = new BlockingCollection<string>(BoundedCapacity);

            // Declare the worker
            Action<string> work = line =>
            {
                // Loop words and filter seperators
                foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Valid word
                    if (!TrackWordsClass.IsValidWord(word)) { continue; }
                    // Update word list
                    result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                }
            };

            Task.Run(() =>
            {
                // Begin producing
                foreach (var line in File.ReadLines(InputFile.FullName))
                {
                    blockingCollection.Add(line);
                }
                blockingCollection.CompleteAdding();
            });

            // Start consuming
            blockingCollection
                .GetConsumingEnumerable()
                .AsParallel()
                .WithDegreeOfParallelism(WorkerCount)
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .ForAll(work);
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
