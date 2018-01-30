using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Configureoo.Core.Crypto;
using Configureoo.KeyStore.EnvironmentVariables;

namespace Configureoo.VisualStudioTools
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ConfigureooToolWindowControl.
    /// </summary>
    public partial class ConfigureooToolWindowControl : UserControl
    {
        private const string ConfigureooKeyPrefix = "CONFIGUREOO_";
        private IKeyStore _keyStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureooToolWindowControl"/> class.
        /// </summary>
        public ConfigureooToolWindowControl()
        {
            this.InitializeComponent();
            _keyStore = new EnvironmentVariablesKeyStore();
        }

        private void MyToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
           RebindKeys();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = NewKeyName.Text;
            string envKey = ConfigureooKeyPrefix + name;
            if (_keyStore.Exists(name))
            {
                MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Configureoo key {0} already set", envKey), "Configureoo");
                return;
            }

            var cyrpto = new Configureoo.Core.Crypto.CryptoStrategies.AesCryptoStrategy();
            string key = cyrpto.GenerateKey();
            _keyStore.Add(new CryptoKey(name, true, key));
            RebindKeys();
        }

        private void DeleteKeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var key = ((Button)sender).Tag as ConfigureooKey;
            if (MessageBox.Show($"Warning, you are about to delete the key {key.Name}.  You will no longer be able to decrypt values unless you have another copy of this key.  Are you sure you want to permanently delete it from this machine?.", "Configureoo", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
            {
                return;
            }
            _keyStore.Delete(new CryptoKey(key.Name, true, key.Value));
            RebindKeys();
        }

        private void RebindKeys()
        {
            var allKeys = _keyStore.GetAll().Select(x => new ConfigureooKey
            {
                Name = x.Name,
                Value = x.Key
            }).ToList();
            DataContext = allKeys;
        }
    }

    public class ConfigureooKey
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}