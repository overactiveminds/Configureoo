using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Configureoo.VisualStudioTools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(VSPackageCommandSubmenu.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(Configureoo.VisualStudioTools.ConfigureooToolWindow))]
    public sealed class VSPackageCommandSubmenu : Package
    {
        public const string PackageGuidString = "d2cc7f26-42fb-4c8d-858f-05f3fc3599d5";

        public VSPackageCommandSubmenu()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();


            // Create our oupt pane for the log
            IVsOutputWindow outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            Guid customGuid = new Guid("F1B02DC8-A0A3-4866-BEF4-E4D370F33CF2");
            const string customTitle = "Configureoo";
            outWindow.CreatePane(ref customGuid, customTitle, 1, 1);
            outWindow.GetPane(ref customGuid, out var _outputPane);

            EncryptCommand.Initialize(this, _outputPane);
            DecryptCommand.Initialize(this, _outputPane);
            Configureoo.VisualStudioTools.ConfigureooToolWindowCommand.Initialize(this);
        }
    }
}
