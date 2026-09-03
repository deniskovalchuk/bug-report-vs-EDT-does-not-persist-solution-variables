using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace VisualStudioReport
{
    internal sealed class GetVariableCommand
    {
        public const int CommandId = 4129;
        public static readonly Guid CommandSet = new Guid("51e099f6-f729-4a4e-8b15-e6108eebecfa");
        private readonly AsyncPackage package;
        private readonly DTE2 dte;

        private GetVariableCommand(AsyncPackage package, OleMenuCommandService commandService, DTE2 dte)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            this.dte = dte;

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static GetVariableCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            DTE2 dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            Instance = new GetVariableCommand(package, commandService, dte);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            string value;
            if (dte.Solution.Globals.get_VariableExists("Var"))
            {
                value = (string) dte.Solution.Globals["Var"];
            }
            else
            {
                value = "Not found";
            }

            VsShellUtilities.ShowMessageBox(this.package,
                                            string.Format("Var: {0}", value),
                                            string.Empty,
                                            OLEMSGICON.OLEMSGICON_INFO,
                                            OLEMSGBUTTON.OLEMSGBUTTON_OK,
                                            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
