using System;
using System.IO;
namespace InternetFilter.If2k.Kernel

{
    public class FileReadManagerForTest : FileReadManager

    {
        private String goodurl1;
        private String badurl1;
        private String postbadurl1;
        private String badphr1;

        public FileReadManagerForTest(
                                      String goodurl1,
                                      String badurl1,
                                      String postbadurl1,
                                      String badphr1
                                      )

        {
            this.goodurl1 = goodurl1;
            this.badurl1 = badurl1;
            this.postbadurl1 = postbadurl1;
            this.badphr1 = badphr1;
        }

        public FileReadManagerForTest()
        : this(
               "www.google.com\n",
               "www.badsite.com\n",
               "www.postbadsite.com\n",
               "[this is a ][,really][,fun][,stupid][bad phrase]\n")

        {
        }

        public TextReader openFileNamed(String fname)

        {
            BufferedStream br = null;
            FileStream f = null;
            if (fname == "1goodurl.txt")

            {
                f = new FileStream(goodurl1, System.IO.FileMode.Open);
            }
            else if (fname == "1badurl.txt")

            {
                f = new FileStream(badurl1, System.IO.FileMode.Open);
            }
            else if (fname == "1postbadurl.txt")

            {
                f = new FileStream(postbadurl1, System.IO.FileMode.Open);
            }
            else if (fname == "1badphr.txt")

            {
                f = new FileStream(badphr1, System.IO.FileMode.Open);
            }
            return new StreamReader(f);
        }
    }
}
