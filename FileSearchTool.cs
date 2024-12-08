using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    internal class FileSearchTool
    {
        // store results in a concurrent bag (a thread-safe collection)
        ConcurrentBag<SearchResult> results = new();

        // create a CancellationTokenSource
        public CancellationTokenSource cts = new CancellationTokenSource();

        // class that contain the result of the search
        public class SearchResult
        {
            public string FilePath { get; set; }
            public int MatchCount { get; set; }
        }

        // function to search a keyword in a file
        public void SearchKeywordInFile(string filePath, string keyword, IProgress<(string, TimeSpan)> progress, CancellationToken token)
        {
            int matchCount = 0;

            // start the timer to calculate the time that thread token 
            var stopwatch = Stopwatch.StartNew();

            // serach in the file line by line
            foreach (var line in File.ReadLines(filePath))
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                // check if the keword in the line and increament the matchCount
                if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    matchCount++;
                }
            }
            stopwatch.Stop();

            // check if the matchCount > 0, add the file to results (concurrent bag)
            if (matchCount > 0)
            {
                results.Add(new SearchResult
                {
                    FilePath = filePath,
                    MatchCount = matchCount
                });
            }
            Console.WriteLine($"Reporting progress for: {filePath}, Match count: {matchCount}, Elapsed time: {stopwatch.Elapsed.TotalMilliseconds}ms");

            // use IProgress<T> to report the progress
            progress.Report(($"Processed: {filePath}, Match count: {matchCount}", stopwatch.Elapsed));
        }

        // asynchronous function to handle the search in multiple files in parallel
        public async Task SearchKeywordInFilesAsync(string[] filePaths, string keyword, IProgress<(string, TimeSpan)> progress)
        {
            var token = cts.Token;

            // create a task (thread) for each file and run the SearchFile function
            var tasks = filePaths.Select(file => Task.Run(() => SearchKeywordInFile(file, keyword, progress, token), token));

            try
            {
                // wait for all tasks to complete
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException)
            {
                progress.Report(("Search canceled", TimeSpan.Zero));
            }
        }

        public void CancelSearch() => new CancellationTokenSource().Cancel();

        // collect the results from all threads and return them
        public ConcurrentBag<SearchResult> GetResults() => results;
    }
}
