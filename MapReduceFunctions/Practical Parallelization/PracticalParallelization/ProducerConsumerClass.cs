using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalParallelization
{
    public class ProducerConsumerClass
    {
        public static IDictionary<string, uint> GetTopWordsProducerConsumer(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Limitations
            const int WorkerCount = 12;
            const int BoundedCapacity = 10000;
            var result = new ConcurrentDictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);

            // Setup the queue
            var blockingCollection = new BlockingCollection<string>(BoundedCapacity);

            // Declare the worker
            Action work = () =>
            {
                // Each line in selected block
                foreach (var line in blockingCollection.GetConsumingEnumerable())
                {
                    // Each word in line, filter seperators
                    foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                    {
                        // Valid word
                        if (!TrackWordsClass.IsValidWord(word)) { continue; }
                        // Update word list
                        result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                    }
                }
            };

            // Start the workers
            var tasks = Enumerable.Range(1, WorkerCount).Select(n => Task.Factory.StartNew(work, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default))
                .ToArray();

            // Begin producing
            foreach (var line in File.ReadLines(InputFile.FullName))
            {
                blockingCollection.Add(line);
            }
            blockingCollection.CompleteAdding();
            // End of producing

            // Wait for workers to finish their work
            Task.WaitAll(tasks);
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
