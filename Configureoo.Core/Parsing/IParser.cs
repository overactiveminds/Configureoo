using System.Collections.Generic;

namespace Configureoo.Core.Parsing
{
    public interface IParser
    {
        List<Tag> Parse(string input);
    }
}