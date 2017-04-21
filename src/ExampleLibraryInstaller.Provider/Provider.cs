using Microsoft.Web.LibraryInstaller.Contracts;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleLibraryInstaller.Provider
{
    public class Provider : IProvider
    {
        public string Id => "example";

        public IHostInteraction HostInteraction { get; set; }

        public ILibraryCatalog GetCatalog()
        {
            return new Catalog(Id);
        }

        public async Task<ILibraryInstallationResult> InstallAsync(ILibraryInstallationState desiredState, CancellationToken cancellationToken)
        {
            try
            {
                foreach (string file in desiredState.Files)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return LibraryInstallationResult.FromCancelled(desiredState);
                    }

                    string path = Path.Combine(desiredState.DestinationPath, file);
                    var func = new Func<Stream>(() => GetStream(desiredState));
                    bool writeOk = await HostInteraction.WriteFileAsync(path, func, desiredState, cancellationToken).ConfigureAwait(false);

                    if (!writeOk)
                    {
                        return new LibraryInstallationResult(desiredState, PredefinedErrors.CouldNotWriteFile(file));
                    }
                }
            }
            catch (Exception ex) when (ex is InvalidLibraryException || ex.InnerException is InvalidLibraryException)
            {
                return new LibraryInstallationResult(desiredState, PredefinedErrors.UnableToResolveSource(desiredState.LibraryId, desiredState.ProviderId));
            }
            catch (Exception ex)
            {
                HostInteraction.Logger.Log(ex.ToString(), LogLevel.Error);
                return new LibraryInstallationResult(desiredState, PredefinedErrors.UnknownException());
            }

            return LibraryInstallationResult.FromSuccess(desiredState);
        }

        private Stream GetStream(ILibraryInstallationState desiredState)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(desiredState.LibraryId);
            writer.Flush();
            stream.Position = 0;
            return stream;

        }
    }
}
