using System;
using System.IO;
using System.Diagnostics;

using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class TreeFileRequestorPlain : FileReadManager
    {

        String fname_prefix;

        public TreeFileRequestorPlain(String fname_prefix)
        {
            this.fname_prefix = fname_prefix;
        }

        public TreeFileRequestorPlain()
        {
            this.fname_prefix = "";
        }

        public TextReader openFileNamed(String fname)
        {
            return new StreamReader( fname_prefix + fname);
        }
    }
}
