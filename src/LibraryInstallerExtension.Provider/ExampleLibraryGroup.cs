using Microsoft.Web.LibraryInstaller.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryInstallerExtension.Provider
{
    public class ExampleLibraryGroup : ILibraryGroup
    {
        public ExampleLibraryGroup(string groupName)
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
