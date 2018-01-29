using System;
using Vikekh.CV.PhantomJS;

namespace Vikekh.CV.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var phantomJS = new PhantomJSWrapper();
            phantomJS.Execute();
        }
    }
}
