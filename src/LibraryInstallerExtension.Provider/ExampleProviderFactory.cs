using Microsoft.Web.LibraryInstaller.Contracts;
using System.IO;

#if NET46
using System.ComponentModel.Composition;
#endif

namespace LibraryInstallerExtension.Provider
{
#if NET46
    [Export(typeof(IProviderFactory))]
#endif
    public class ExampleProviderFactory : IProviderFactory
    {
        public IProvider CreateProvider(IHostInteraction hostInteraction)
        {
            var provider = new ExampleProvider();
            string storePath = Path.Combine(hostInteraction.CacheDirectory, provider.Id);
            provider.HostInteraction = hostInteraction;
            return provider;
        }
    }
}
