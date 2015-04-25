using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel
{
    public class TreePhrase : Tree
    {

        public TreePhrase() : base(
                                   new ComparatorCaseInsensitive(),
                                   new IgnorerWhiteSpace())
        {

        }
    }
}
