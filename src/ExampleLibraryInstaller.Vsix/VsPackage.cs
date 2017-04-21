using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace ExampleLibraryInstaller.Vsix
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [Guid("8a1873d7-c7cb-4c68-a798-d930ac073b49")]
    public sealed class VsPackage : AsyncPackage
    {
    }
}
