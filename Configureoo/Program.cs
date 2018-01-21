using Configureoo.Core.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace Configureoo
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            var files = app.Option("-f", "The list of files to transform", CommandOptionType.MultipleValue);
            var outputs = app.Option("-o", "The list of output files or an output directory", CommandOptionType.MultipleValue);
            app.OnExecute(() =>
            {
                var parser = new FileParser(new Parser());
                parser.Parse(files.Values);
                return 0;
            });
            app.Execute(args);
        }
    }
}
