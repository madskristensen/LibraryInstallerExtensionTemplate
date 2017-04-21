using Microsoft.Web.LibraryInstaller.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleLibraryInstaller.Provider
{
    public class LibraryGroup : ILibraryGroup
    {
        public LibraryGroup(string groupName)
        {
            DisplayName = groupName;
        }

        public string DisplayName { get; }

        public string Description => string.Empty;

        public Task<IEnumerable<string>> GetLibraryIdsAsync(CancellationToken cancellationToken)
        {
            string[] ids = { DisplayName };

            return Task.FromResult<IEnumerable<string>>(ids);
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
