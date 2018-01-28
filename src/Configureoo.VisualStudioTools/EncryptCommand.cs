using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Configureoo.Core;
using Configureoo.Core.Crypto.CryptoStrategies;
using Configureoo.Core.Parsing;
using Configureoo.KeyStore.EnvironmentVariables;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace Configureoo.VisualStudioTools
{
    internal sealed class EncryptCommand
    {
        public const int CommandId = 0x0101;

        public static readonly Guid CommandSet = new Guid("bea4976d-811a-4dd0-9745-0bed7a658b5d");

        private readonly Package package;

        private EncryptCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        public static EncryptCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new EncryptCommand(package);
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            var selectedItems = ((UIHierarchy)((DTE2)this.ServiceProvider.GetService(typeof(DTE))).Windows.Item("{3AE79031-E1BC-11D0-8F78-00A0C9110057}").Object).SelectedItems as object[];
            if (selectedItems == null) return;

            var files = (IEnumerable<string>) (from t in selectedItems
                where (t as UIHierarchyItem)?.Object is ProjectItem
                select ((ProjectItem) ((UIHierarchyItem) t).Object).FileNames[1]).ToList();

            var service = new ConfigurationFileService(new ConfigurationService(new Parser(), new EnvironmentVariablesKeyStore(), new AesCryptoStrategyFactory()));
            service.Encrypt(files);
        }
    }
}
