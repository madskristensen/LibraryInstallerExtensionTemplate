using Microsoft.Web.LibraryInstaller.Contracts;
using System.Collections.Generic;

namespace ExampleLibraryInstaller.Provider
{
    public class Library : ILibrary
    {
        public string Name { get; set; }
        public string ProviderId { get; set; }
        public string Version => "1.0";
        public IReadOnlyDictionary<string, bool> Files { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
