using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PracticalParallelization
{
    class DataFlowClass
    {
        public static IDictionary<string, uint> GetTopWordsDataFlow(FileInfo InputFile, char[] Separators, uint TopCount)
        {
            // Limitations
            const int WorkerCount = 12;
            var result = new ConcurrentDictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            const int BoundedCapacity = 10000;

            // Buffer blocks
            var bufferBlock = new BufferBlock<string>(
                new DataflowBlockOptions { BoundedCapacity = BoundedCapacity });

            // Split blocks into lines
            var splitLineToWordsBlock = new TransformManyBlock<string, string>(
                line => line.Split(Separators, StringSplitOptions.RemoveEmptyEntries),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 1,
                    BoundedCapacity = BoundedCapacity
                });

            var batchWordsBlock = new BatchBlock<string>(5000);

            var trackWordsOccurrencBlock = new ActionBlock<string[]>(words =>
            {
                // Loop words in lines
                foreach (var word in words)
                {
                    // Valid word
                    if (!TrackWordsClass.IsValidWord(word)) { continue; }
                    // Update word list
                    result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                }
            },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = WorkerCount });

            var defaultLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            bufferBlock.LinkTo(splitLineToWordsBlock, defaultLinkOptions);
            splitLineToWordsBlock.LinkTo(batchWordsBlock, defaultLinkOptions);
            batchWordsBlock.LinkTo(trackWordsOccurrencBlock, defaultLinkOptions);

            // Begin producing
            foreach (var line in File.ReadLines(InputFile.FullName))
            {
                bufferBlock.SendAsync(line).Wait();
            }

            bufferBlock.Complete();
            // End of producing

            // Wait for workers to finish their work
            trackWordsOccurrencBlock.Completion.Wait();
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
