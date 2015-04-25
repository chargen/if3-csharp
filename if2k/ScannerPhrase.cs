using System;
using System.IO;
using System.Diagnostics;

namespace InternetFilter.If2k.Kernel

{
    public class ScannerPhrase : Scanner

    {
        public ScannerPhrase(TreeLoader loader) :
        base(new TreePhrase(), new PrefixTesterUrl(), new PatternExpanderPhrase(), loader)

        {
            this.GetPatternExpander().setTarget(GetTree());
        }
    }
}
