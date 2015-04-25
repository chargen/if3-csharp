using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using InternetFilter.Log;

namespace InternetFilter.If2k.Kernel

{
    public class Kernel

    {
        private static InternetFilter.Log.Log log = new InternetFilter.Log.Log("InternetFilter.If2k.Kernel","Kernel:");
        private Scanner bad_url_scanner;
        private Scanner postbad_url_scanner;
        private Scanner bad_phrase_scanner;
        private Scanner good_url_scanner;
        private FileReadManager file_manager;

        public Kernel(FileReadManager file_requestor)

        {
            this.file_manager = file_requestor;
            this.bad_url_scanner = new ScannerPhrase(new TreeLoader());
            this.postbad_url_scanner = new ScannerPhrase(new TreeLoader());
            this.bad_phrase_scanner = new ScannerPhrase(new TreeLoader());
            this.good_url_scanner = new ScannerGoodUrl(new TreeLoader());
        }


        /**
         * Load a file into a scanner, make each entry have the specified flag
         *
         * @param scanner
         * @param fname
         * @param flag
         */
        private void loadScanner(Scanner scanner, String fname, TreeFlag flag)
        {
            if (file_manager != null)
            {
                try
                {
                    TextReader br = file_manager.openFileNamed(fname);
                    if (br != null)
                    {
                        scanner.Load(
                                     br,
                                     "",
                                     flag);
                    }
                } catch (FileNotFoundException e)
                {
                    log.Error( 0, "File not found for :{0}",fname);
                } catch (IOException e)
                {
                    log.Error( 0, "IO Exception for :{0}:{1}", fname, e);
                }
            }
        }


        /**
         * Initialize and load all the scanners from files
         *
         */
        public void init()
        {

            for (int cat = 0; cat < 15; ++cat)
            {
                loadScanner(bad_url_scanner, "" + cat + "badurl.txt", new TreeFlag(1, cat, 1, 0, 0));
                loadScanner(postbad_url_scanner, "" + cat + "postbadurl.txt", new TreeFlag(2, cat, 0, 0, 1));
                loadScanner(good_url_scanner, "" + cat + "goodurl.txt", new TreeFlag(3, cat, 0, 1, 0));
                loadScanner(bad_phrase_scanner, "" + cat + "badphr.txt", new TreeFlag(4, cat, 1, 0, 0));
            }
        }

        public void addBadUrl( String url, int cat )
        {
            bad_url_scanner.GetPatternExpander().add("", url, new TreeFlag(1,cat,1,0,0) );
            log.Information(3,"Kernel added " + url + " to category " + cat + " bad url" );
        }

        public void addPostBadUrl( String url, int cat )
        {
            postbad_url_scanner.GetPatternExpander().add("",url, new TreeFlag(2,cat,0,0,1) );
            log.Information(3,"Kernel added " + url + " to category " + cat + " postbad url" );
        }

        public void addGoodUrl( String url, int cat )
        {
            good_url_scanner.GetPatternExpander().add("",url, new TreeFlag(3,cat,0,1,0) );
            log.Information(3,"Kernel added " + url + " to category " + cat + " good url" );
        }

        public void removeBadUrl( String url, int cat )
        {
            bad_url_scanner.GetPatternExpander().remove("", url );
            log.Information(3,"Kernel added " + url + " to category " + cat + " bad url" );
        }

        public void removePostBadUrl( String url, int cat )
        {
            postbad_url_scanner.GetPatternExpander().remove("",url);
            log.Information(3,"Kernel added " + url + " to category " + cat + " postbad url" );
        }

        public void removeGoodUrl( String url, int cat )
        {
            good_url_scanner.GetPatternExpander().remove("",url);
            log.Information(3,"Kernel added " + url + " to category " + cat + " good url" );
        }

        public int quantifyResultsForUrlData(KernelResults text_results, KernelResults link_results)
        {

            int result = 0;
            bool is_good = false;
            bool is_bad = false;
            bool is_unknown = false;

            if (link_results.getTotalBadness() > 0)
            {
                is_bad = true;
                is_good = false;
                is_unknown = false;
            }

            if (link_results.getTotalPostbad() > 0 || link_results.getTotalPostbad() > 0)
            {
                is_bad = true;
                is_good = false;
                is_unknown = false;
            }

            if (text_results.getTotalBadness() > 0)
            {
                is_bad = true;
                is_good = false;
                is_unknown = false;
            }

            if (text_results.getTotalPostbad() > 0)
            {
                is_bad = true;
                is_good = false;
                is_unknown = false;
            }


            if (is_bad)
            {
                result = -1;
            }
            if (is_good)
            {
                result = 1;
            }

            return result;

        }

        class MyTarget : ScannerTarget
        {

            LinkedList<TreeNode> matches;

            MyTarget()
            {
                matches = new LinkedList<TreeNode>();
            }

            LinkedList<TreeNode> getMatches()
            {
                return matches;
            }

            public void matchFound(Scanner s, TreeNode match_item)
            {
                matches.AddLast(match_item);
            }
        }

        public KernelResults searchAll(String buf)
        {
            KernelResults target = new KernelResults();

            bad_url_scanner.ScanBuffer(buf, target);
            good_url_scanner.ScanBuffer(buf, target);
            postbad_url_scanner.ScanBuffer(buf, target);
            bad_phrase_scanner.ScanBuffer(buf, target);

            return target;
        }

        public KernelResults searchHostNameOfUrl(String url_string)
        {
            KernelResults target = new KernelResults();

            Uri url = null;
            String host = null;

            url = new Uri(url_string);

            host = url.Host;

            bad_url_scanner.ScanBuffer(host, target);
            //good_url_scanner.ScanBuffer(host, target);
            postbad_url_scanner.ScanBuffer(host, target);
            bad_phrase_scanner.ScanBuffer(host, target);

            return target;
        }

        public int quantifyResultsForUrl(String url_to_test)
        {
            KernelResults url_results = searchAll(url_to_test);

            KernelResults hostname_results = searchHostNameOfUrl(url_to_test);

            return quantifyResultsForUrl(url_to_test,hostname_results, url_results);
        }

        public int quantifyResultsForUrl(String url, KernelResults hostname_results,
                                         KernelResults url_results)
        {
            int result = 0;
            bool is_good = false;
            bool is_bad = false;
            bool is_unknown = true;

            // if the hostname is known bad then we are bad
            if (hostname_results.getTotalBadness() > 0)
            {
                is_bad = true;
                is_good = false;
                is_unknown = false;
            }

            // if the hostname is known good then we are good
            if (hostname_results.getTotalGoodness() > 0)
            {
                is_good = true;
                is_bad = false;
                is_unknown = false;
            }

            // if the hostname or the full url is known postbad then we are bad
            if (hostname_results.getTotalPostbad() > 0 || url_results.getTotalPostbad() > 0)
            {
                is_bad = true;
                is_good = false;
                is_unknown = false;
            }

            // if the site is not yet known then check the rest of the url
            if (is_unknown)
            {
                // if the rest of the url is known bad then we are bad
                if (url_results.getTotalBadness() > 0)
                {
                    is_bad = true;
                    is_good = false;
                    is_unknown = false;
                }

                // if the rest of the url is known postbad then we are bad
                if (url_results.getTotalPostbad() > 0)
                {
                    is_bad = true;
                    is_good = false;
                    is_unknown = false;
                }

                if (url_results.getTotalGoodness() > 0)
                {
                    foreach ( TreeNode i in url_results.getMatches() )
                    {
                        TreeFlag f = i.getFlags();

                        if( f.getGoodness()>0 )

                        {
                            String extracted = i.extractWord();

                            if (    url == extracted ||
                                    url == "http://" + extracted ||
                                    url == "http://www." + extracted )
                            {
                                is_bad = false;
                                is_good = true;
                                is_unknown = false;
                                break;
}
                        }
                    }
                }
            }

            if (is_bad)
            {
                result = -1;
            }
            if (is_good)
            {
                result = 1;
            }

            return result;
        }
    }
}
