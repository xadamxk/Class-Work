namespace PracticalParallelization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class Program
    {
        private const uint SanityCountToMatch = 4230497;
        private const uint TopCount = 20;

        //public static readonly FileInfo InputFile = new FileInfo(@"C:\Users\Adam\Desktop\Blog.Samples-master\Large Data\Dummys Guide to the Internet.txt");
        public static readonly FileInfo InputFile = new FileInfo(@"C:\Users\Adam\Desktop\Blog.Samples-master\Large Data\Test Data 1.txt");
        public static readonly char[] Separators = { ' ', '.', ',' };
        public static readonly HashSet<char> InvalidCharacters = new HashSet<char>(new[] { '®', '"', '\'', '!', '(', ')', '{', '}', '<', '>', '|', '?', '-', '_', '&' });
        public static readonly HashSet<string> StopWords = new HashSet<string>(new[] {
            "a", "about", "above", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along",
            "already", "also", "although", "always", "am", "among", "amongst", "amoungst", "amount", "an", "and", "another", "any",
            "anyhow", "anyone", "anything", "anyway", "anywhere", "are", "around", "as", "at", "back", "be", "became", "because",
            "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "between",
            "beyond", "bill", "both", "bottom", "but", "by", "call", "can", "cannot", "cant", "co", "con", "could", "couldnt", "cry", "de",
            "describe", "detail", "do", "done", "down", "due", "during", "each", "eg", "eight", "either", "eleven", "else", "elsewhere",
            "empty", "enough", "etc", "even", "ever", "every", "everyone", "everything", "everywhere", "except", "few", "fifteen", "fify",
            "fill", "find", "fire", "first", "five", "for", "former", "formerly", "forty", "found", "four", "from", "front", "full",
            "further", "get", "give", "go", "had", "has", "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein",
            "hereupon", "hers", "herself", "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "inc", "indeed",
            "interest", "into", "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less", "ltd", "made", "many",
            "may", "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my", "myself",
            "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "none", "noone", "nor", "not", "nothing",
            "now", "nowhere", "of", "off", "often", "on", "once", "one", "only", "onto", "or", "other", "others", "otherwise", "our",
            "ours", "ourselves", "out", "over", "own", "part", "per", "perhaps", "please", "put", "rather", "re", "same", "see", "seem",
            "seemed", "seeming", "seems", "serious", "several", "she", "should", "show", "side", "since", "sincere", "six", "sixty", "so",
            "some", "somehow", "someone", "something", "sometime", "sometimes", "somewhere", "still", "such", "system", "take", "ten",
            "than", "that", "the", "their", "them", "themselves", "then", "thence", "there", "thereafter", "thereby", "therefore",
            "therein", "thereupon", "these", "they", "thickv", "thin", "third", "this", "those", "though", "three", "through",
            "throughout", "thru", "thus", "to", "together", "too", "top", "toward", "towards", "twelve", "twenty", "two", "un", "under",
            "until", "up", "upon", "us", "very", "via", "was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever",
            "where", "whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither",
            "who", "whoever", "whole", "whom", "whose", "why", "will", "with", "within", "without", "would", "yet", "you", "your",
            "yours", "yourself", "yourselves", "the", "pl:", "mv:", "by:", "Anonymous", "he's", "it's", "she's", "i'm", "I"
        }, StringComparer.InvariantCultureIgnoreCase);

        private static void Main()
        {
            var stopWatch = Stopwatch.StartNew();
            IDictionary<string, uint> words;

            words = SequentialClass.GetTopWordsSequential(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("1. Sequential: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = SequentialLinqClass.GetTopWordsSequentialLINQ(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n2. Sequential LINQ: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = PlinqNaiveClass.GetTopWordsPLINQNaive(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n3. Parallel LINQ Native: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = PlinqClass.GetTopWordsPLINQ(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n4. Parallel LINQ: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = PlinqMapReduceClass.GetTopWordsPLINQMapReduce(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n5. Parallel LINQ Map/Reduce: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = PLINQConcurrentDictionaryClass.GetTopWordsPLINQConcurrentDictionary(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n6. Parallel LINQ Concurrent Dictionary: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = PLINQProducerConsumerClass.GetTopWordsPLINQProducerConsumer(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n7. Parallel LINQ Producer/Consumer: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = ParallelForEachMapReduceClass.GetTopWordsParallelForEachMapReduce(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n8. Parallel ForEach Map/Reduce: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = ParallelForEachConcurrentDictionaryClass.GetTopWordsParallelForEachConcurrentDictionary(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n9. Parallel ForEach Concurrent Dictionary: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = ProducerConsumerClass.GetTopWordsProducerConsumer(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.Write("\n10. Producer/Consumer: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = ProducerConsumerEasierClass.GetTopWordsProducerConsumerEasier(InputFile, Separators, TopCount);
            stopWatch.Stop();
            Console.WriteLine("\n11. Producer/Consumer Simplified: " + stopWatch.ElapsedMilliseconds + " ms");
            stopWatch.Reset();

            stopWatch.Start();
            words = DataFlowClass.GetTopWordsDataFlow(InputFile, Separators, TopCount);
            stopWatch.Stop();
            stopWatch.Reset();


            /*
            var sanityCount = (uint)words.Sum(pair => pair.Value);
            if (sanityCount != sanityCount)
            {
                using (var process = Process.GetCurrentProcess())
                {
                    Console.WriteLine("\nExecution time: {0}\r\nGen-0: {1}, Gen-1: {2}, Gen-2: {3}\r\nPeak WrkSet: {4}",
                        stopWatch.Elapsed.ToString(),
                        GC.CollectionCount(0).ToString(),
                        GC.CollectionCount(1).ToString(),
                        GC.CollectionCount(2).ToString(),
                        process.PeakWorkingSet64.ToString("n0"));
                }
            }
            else
            {
                Console.WriteLine("Invalid results, expected a sanity count of: {0} but got: {1}",
                    SanityCountToMatch.ToString("n0"), sanityCount.ToString("n0"));
            }
            */

            // Print top 15 words
            Console.WriteLine("Word: Count");
            foreach (KeyValuePair<string, uint> kvp in words)
            {
                Console.WriteLine(string.Format("{0}: {1}", kvp.Key, kvp.Value));
            }
            Console.Write("\n\nDone.");
            Console.ReadKey();
        }

        



        /*
        private static IDictionary<string, uint> GetTopWordsSequential()
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
                    if (!IsValidWord(word)) { continue; }
                    // Track word
                    TrackWordsOccurrence(result, word);
                }
            }
            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        */

        /*
        private static IDictionary<string, uint> GetTopWordsSequentialLINQ()
        {
            // Return ordered dictionary
            return File.ReadLines(InputFile.FullName)
                .SelectMany(l => l.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                .Where(IsValidWord)
                .ToLookup(x => x, StringComparer.InvariantCultureIgnoreCase)
                .Select(x => new { Word = x.Key, Count = (uint)x.Count() })
                .OrderByDescending(kv => kv.Count)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Word, kv => kv.Count);
        }
        */

        /*
        private static IDictionary<string, uint> GetTopWordsPLINQNaive()
        {
            // Initalize words array by reading from file in paralll
            var words = File.ReadLines(InputFile.FullName)
                .AsParallel()
                .SelectMany(l => l.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                .Where(IsValidWord);
            // Initialize results dictionary
            var result = new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var word in words)
            {
                // Track word
                TrackWordsOccurrence(result, word);
            }

            // Return ordered dictionary
            return result
                .OrderByDescending(kv => kv.Value)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        */

        /*
        private static IDictionary<string, uint> GetTopWordsPLINQ()
        {
            // Return ordered dictionary
            return File.ReadLines(InputFile.FullName)
                .AsParallel()
                .SelectMany(l => l.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
                .Where(IsValidWord)
                .ToLookup(x => x, StringComparer.InvariantCultureIgnoreCase)
                .AsParallel()
                .Select(x => new { Word = x.Key, Count = (uint)x.Count() })
                .OrderByDescending(kv => kv.Count)
                .Take((int)TopCount)
                .ToDictionary(kv => kv.Word, kv => kv.Count);
        }
        */

        /*
    private static IDictionary<string, uint> GetTopWordsPLINQMapReduce()
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
                        if (!IsValidWord(word)) { continue; }
                        // Track word
                        TrackWordsOccurrence(localDic, word);
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
    */

        /*
    private static IDictionary<string, uint> GetTopWordsPLINQConcurrentDictionary()
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
                    if (!IsValidWord(word)) { continue; }
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
    */

        /*
    private static IDictionary<string, uint> GetTopWordsPLINQProducerConsumer()
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
                if (!IsValidWord(word)) { continue; }
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
    */
        /*
        private static IDictionary<string, uint> GetTopWordsParallelForEachMapReduce()
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
                        if (!IsValidWord(word)) { continue; }
                        // Update word list
                        TrackWordsOccurrence(localDic, word);
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
        */

        /*
    private static IDictionary<string, uint> GetTopWordsParallelForEachConcurrentDictionary()
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
                    if (!IsValidWord(word)) { continue; }
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
    */

        /*
        private static IDictionary<string, uint> GetTopWordsProducerConsumer()
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
                        if (!IsValidWord(word)) { continue; }
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
        */

        /*
        private static IDictionary<string, uint> GetTopWordsProducerConsumerEasier()
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
                    if (!IsValidWord(word)) { continue; }
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
        */

        /*
    private static IDictionary<string, uint> GetTopWordsDataFlow()
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
                    if (!IsValidWord(word)) { continue; }
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
    */


    }
}
