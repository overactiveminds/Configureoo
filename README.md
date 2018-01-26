# Configureoo

## Building

To build Configureoo, just open Configureoo.sln and hit build.  

## What does it do?

As the DevOps community move towards source control as the single source of truth for storing code, infrastructure and configuration some teams will face resistance due to security concerns around storing sensitive config values in source control.  Configureoo attempts to address that by providing easy to use encryption of specific values in configration which can then safely be stored in version control (even open source projects).

In a nutshell, you first setup a key and store that key in an environment variable by invoking:

```bash
Configureoo -k default
```

This command will create a 128 bit random key and store it in the environment variable CONFIGUREOO_default. You can then enter values into a configuration file (e.g. appsettings.json) in the Configuroo format for example:

```JSON
{
  "someSensitiveValue": "<CFGO>some private key</CFGO>"
}
```

Then invoking:

```bash
Configureoo encrypt -f appsettings.json
```

Results in a file similar to:

```Json
{
  "someSensitiveValue": "<CFGO>XFgySLm78ezB3OTwbNp1fpTnLN2ewI+SnPVQZqTxxd6GlTwL/UhDiJnALc5eGcHN</CFGO>"
}
```

You can then commit this file to source control.  

Decrytping the data is acheived by first setting the same environment variable on the box / image / server running your dependent code and then load it via the Overactiveminds.Configureoo.JsonConfigurationProvider.  Simply call the AddConfigureooJsonFile extension method when initializing configuration instead of the normal AddJsonFile and Configureoo will decrypt the sensitive values and present them to your application in via normal configuration.

