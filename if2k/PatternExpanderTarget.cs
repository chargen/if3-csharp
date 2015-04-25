using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{

    public interface PatternExpanderTarget
    {

        void add(String pattern, TreeFlag flags);
        bool remove( String pattern );
    }

}
