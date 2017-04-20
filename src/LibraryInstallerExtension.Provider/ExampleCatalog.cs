using Microsoft.Web.LibraryInstaller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryInstallerExtension.Provider
{
    public class ExampleCatalog : ILibraryCatalog
    {
        private string _providerId;
        private static string[] _ids = { "example1", "example2", "example3" };

        public ExampleCatalog(string providerId)
        {
            _providerId = providerId;
        }

        public Task<string> GetLatestVersion(string libraryId, bool includePreReleases, CancellationToken cancellationToken)
        {
            return Task.FromResult(libraryId);
        }

        public Task<ILibrary> GetLibraryAsync(string libraryId, CancellationToken cancellationToken)
        {
            var library = new ExampleLibrary
            {
                Name = libraryId,
                ProviderId = _providerId,
                Files = new Dictionary<string, bool>()
                {
                    { $"{libraryId}).txt", true }
                }
            };

            return Task.FromResult<ILibrary>(library);
        }

        public Task<CompletionSet> GetLibraryCompletionSetAsync(string value, int caretPosition)
        {
            var set = new CompletionSet
            {
                Start = 0,
                Length = value.Length,
                Completions = _ids.Select(id => new CompletionItem { DisplayText = id, InsertionText = id })
            };

            return Task.FromResult(set);
        }

        public Task<IReadOnlyList<ILibraryGroup>> SearchAsync(string term, int maxHits, CancellationToken cancellationToken)
        {
            var hits = _ids.Where(id => id.IndexOf(term, StringComparison.OrdinalIgnoreCase) > -1);

            var groups = new List<ILibraryGroup>();
            groups.AddRange(hits.Select(id => new ExampleLibraryGroup(id)));

            return Task.FromResult<IReadOnlyList<ILibraryGroup>>(groups);
        }
    }
}
