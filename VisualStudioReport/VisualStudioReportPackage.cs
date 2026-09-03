using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace VisualStudioReport
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(VisualStudioReportPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class VisualStudioReportPackage : AsyncPackage
    {
        public const string PackageGuidString = "11932e6d-4f36-43f9-804f-16266c27e58b";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await AddVariableCommand.InitializeAsync(this);
            await GetVariableCommand.InitializeAsync(this);
        }
    }
}
