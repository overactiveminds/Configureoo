using System.Collections.Generic;

namespace Configureoo.Core.IO
{
    public interface IParser
    {
        List<Tag> Parse(string input);
    }
}