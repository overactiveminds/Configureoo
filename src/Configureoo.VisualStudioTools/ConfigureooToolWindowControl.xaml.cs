using System;
using System.Collections;
using System.Collections.Generic;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureooToolWindowControl"/> class.
        /// </summary>
        public ConfigureooToolWindowControl()
        {
            this.InitializeComponent();
        }

        private void MyToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
           RebindKeys();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = NewKeyName.Text;
            string envKey = ConfigureooKeyPrefix + name;
            if (Environment.GetEnvironmentVariable(envKey) != null)
            {
                MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Configureoo key {0} already set", envKey), "Configureoo");
                return;
            }

            var cyrpto = new Configureoo.Core.Crypto.CryptoStrategies.AesCryptoStrategy();
            string key = cyrpto.GenerateKey();
            Environment.SetEnvironmentVariable(envKey, key);
            RebindKeys();
        }

        private void RebindKeys()
        {
            var configureooVariables = new List<ConfigureooKey>();
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                string key = de.Key.ToString();
                if (!key.StartsWith(ConfigureooKeyPrefix))
                {
                    continue;
                }
                configureooVariables.Add(new ConfigureooKey { Name = key.Substring(12), Value = de.Value.ToString() });
            }
            DataContext = configureooVariables;
        }
    }

    public class ConfigureooKey
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}