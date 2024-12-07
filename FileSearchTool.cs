using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    internal class FileSearchTool
    {
        ConcurrentBag<SearchResult> results = new();
        public CancellationTokenSource cts = new CancellationTokenSource();

        public class SearchResult
        {
            public string FileName { get; set; }
            public int MatchCount { get; set; }
        }

        public void SearchFile(string filePath, string keyword, IProgress<string> progress, CancellationToken token)
        {
            int matchCount = 0;

            foreach (var line in File.ReadLines(filePath))
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    matchCount++;
                }
            }
            if (matchCount > 0)
            {
                results.Add(new SearchResult
                {
                    FileName = filePath,
                    MatchCount = matchCount
                });
            }
            progress.Report($"Processed: {filePath}, Match count: {matchCount}");
        }

        public async Task SearchFilesAsync(string[] filePaths, string keyword, IProgress<string> progress)
        {
            var token = cts.Token;

            var tasks = filePaths.Select(file => Task.Run(() => SearchFile(file, keyword, progress, token), token));

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException)
            {
                progress.Report("Search canceled");
            }
        }

        public void CancelSearch() => new CancellationTokenSource().Cancel();

        public ConcurrentBag<SearchResult> GetResults() => results;
    }
}
