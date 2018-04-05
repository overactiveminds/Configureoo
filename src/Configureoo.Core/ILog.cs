using System;
using System.Collections.Generic;
using System.Text;

namespace Configureoo.Core
{
    public interface ILog
    {
        void Debug(string message);

        void Error(string message);
    }
}
