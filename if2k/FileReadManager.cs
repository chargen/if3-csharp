using System;
using System.IO;
namespace InternetFilter.If2k.Kernel

{
    public interface FileReadManager

    {
        TextReader openFileNamed(String fname);
    }
}
