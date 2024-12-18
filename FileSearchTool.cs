﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileSearch
{
    internal class FileSearchTool
    {
        // store results in a concurrent bag (a thread-safe collection)
        private ConcurrentBag<SearchResult> results = new();

        // create a ConcurrentBag to store the thread IDs
        private ConcurrentBag<int> threadIds = new();

        // class that contain the result of the search
        public class SearchResult
        {
            public required string FilePath { get; set; }
            public int MatchCount { get; set; }
        }


        // function to search a keyword in a file
        public void SearchKeywordInFile(string filePath, string keyword, IProgress<(string, TimeSpan, int)> progress)
        {
            // Record the current thread ID
            threadIds.Add(Thread.CurrentThread.ManagedThreadId);

            int matchCount = 0;
            int totalLines = 0;

            // start the timer to calculate the time that thread token 
            var stopwatch = Stopwatch.StartNew();

            // serach in the file line by line
            foreach (var line in File.ReadLines(filePath))
            {
                totalLines++;

                // check if the keword in the line and increament the matchCount
                if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    matchCount++;
                }

                // Report progress for each file
                int progressPercentage = (int)(((double)totalLines / File.ReadLines(filePath).Count()) * 100);
                ReportProgress(filePath, matchCount, stopwatch.Elapsed, progressPercentage, progress);
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

            progress.Report(($"{GetFileName(filePath)}, Match count: {matchCount}", stopwatch.Elapsed, 100));
        }

        // function to report progress
        private void ReportProgress(
            string filePath,
            int matchCount,
            TimeSpan elapsed,
            int progressPercentage,
            IProgress<(string, TimeSpan, int)> progress)
        {
            progress.Report(($"{GetFileName(filePath)}, Match count: {matchCount}", elapsed, progressPercentage));
        }

        // asynchronous function to handle the search in multiple files in parallel
        public async Task SearchKeywordInFilesAsync(string[] filePaths, string keyword, IProgress<(string, TimeSpan, int)> progress)
        {
            int totalFiles = filePaths.Length;

            // create a task (thread) for each file and run the SearchFile function
            var tasks = filePaths.Select((file, index) => Task.Run(() =>
            {
                SearchKeywordInFile(file, keyword, progress);
            }));

            try
            {
                // wait for all tasks to complete
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log the error, display an error message, etc.
                Console.WriteLine("An error occurred during the search: " + ex.Message);
            }
        }

        // collect the results from all threads and return them
        public ConcurrentBag<SearchResult> GetResults() => results;

        // extract file name from the file path
        static string GetFileName(string filePath) => Path.GetFileName(filePath);

        // get the number of threads
        public int GetThreadCount() => threadIds.Distinct().Count();
    }
}