using System;
using System.Diagnostics;

namespace Vikekh.CV.PhantomJS
{
    public class PhantomJSWrapper
    {
        public PhantomJSWrapper()
        {
        }

        public void Execute()
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = @"C:\Users\Viktor\bin\phantomjs.exe";
                process.StartInfo.Arguments = @"rasterize.js https://vikekh.github.io/cv/sv cv.pdf";
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
