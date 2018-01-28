using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Configureoo.VisualStudioTools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(VSPackageCommandSubmenu.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class VSPackageCommandSubmenu : Package
    {
        public const string PackageGuidString = "d2cc7f26-42fb-4c8d-858f-05f3fc3599d5";

        public VSPackageCommandSubmenu()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            EncryptCommand.Initialize(this);
            DecryptCommand.Initialize(this);
        }
    }
}
