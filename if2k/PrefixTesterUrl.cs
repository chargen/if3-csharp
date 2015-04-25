using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class PrefixTesterUrl : PrefixTester
    {

        public bool isPrefix(char c)
        {
            return c == '.' || c == '/' || c == '=' || c == '&';
        }
    }
}
