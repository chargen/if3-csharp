using System;
using System.IO;
using System.Diagnostics;


namespace InternetFilter.If2k.Kernel

{

    public class ScannerGoodUrl : Scanner

    {

        public ScannerGoodUrl(TreeLoader loader) :
        base(new TreeUrl(), new PrefixTesterUrl(), new PatternExpanderUrl(), loader)

        {
            GetPatternExpander().setTarget(GetTree());
        }
    }
}
