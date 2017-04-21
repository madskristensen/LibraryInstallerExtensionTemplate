using Microsoft.Web.LibraryInstaller.Contracts;
using System.IO;

#if NET46
using System.ComponentModel.Composition;
#endif

namespace ExampleLibraryInstaller.Provider
{
#if NET46
    [Export(typeof(IProviderFactory))]
#endif
    public class ProviderFactory : IProviderFactory
    {
        public IProvider CreateProvider(IHostInteraction hostInteraction)
        {
            var provider = new Provider();
            string storePath = Path.Combine(hostInteraction.CacheDirectory, provider.Id);
            provider.HostInteraction = hostInteraction;
            return provider;
        }
    }
}
