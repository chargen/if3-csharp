using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel
{
    public class TreeUrl : Tree
    {

        public TreeUrl() : base(
                                new ComparatorCaseInsensitive(),
                                new IgnorerWhiteSpace() )
        {
        }
    }
}

