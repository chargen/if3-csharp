using System;
using System.IO;
using System.Diagnostics;

namespace InternetFilter.If2k.Kernel

{
    public class ScannerTargetDumping : ScannerTarget

    {
        public void matchFound(Scanner s, TreeNode match_item)

        {
            String match_string = match_item.extractWord();

            System.Console.Out.WriteLine("Found Match: " + match_string);
            System.Console.Out.WriteLine("Flags: " + match_item.getFlags());
        }
    }
}
