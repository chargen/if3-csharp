using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class ScannerUrl : Scanner
    {

        public ScannerUrl(TreeLoader loader) : base( new TreeUrl(), new PrefixTesterUrl(), new PatternExpanderUrl(), loader )
        {
            this.GetPatternExpander().setTarget(GetTree());
        }
    }
}
