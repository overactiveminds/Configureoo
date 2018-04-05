using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configureoo.Core;
using Microsoft.VisualStudio.Shell.Interop;

namespace Configureoo.VisualStudioTools
{
    public class OutputWindowLog : ILog
    {
        private readonly IVsOutputWindowPane _outputPane;

        public OutputWindowLog(IVsOutputWindowPane outputPane)
        {
            _outputPane = outputPane;
        }
        public void Debug(string message)
        {
            _outputPane.OutputString(message + Environment.NewLine);
        }

        public void Error(string message)
        {
            _outputPane.OutputString("ERROR: " + message + Environment.NewLine);
        }
    }
}
