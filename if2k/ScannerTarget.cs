using System;
using System.IO;
using System.Diagnostics;

namespace InternetFilter.If2k.Kernel

{
    public interface ScannerTarget

    {
        void matchFound(Scanner s, TreeNode match_item);
    }
}
