using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PatternExpanderTargetDumper : PatternExpanderTarget
    {

        public void add(String pattern, TreeFlag flags)
        {
            System.Console.Out.WriteLine("ADD: " + flags.getFlag() + " " + pattern);
        }

        public bool remove(String pattern)
        {
            System.Console.Out.WriteLine("REMOVE: " + pattern );
            return true;
        }
    }
}
