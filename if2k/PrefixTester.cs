using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{

    /**
     *
     * @author jeffk
     */
    public interface PrefixTester
    {

        bool isPrefix(char a);
    }

}
