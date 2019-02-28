using System;
using System.Collections.Generic;
using System.Text;

namespace HosingConsole.Config
{
    public class CompilerOptions
    {
        public bool noImplicitAny { get; set; }
        public bool noEmitOnError { get; set; }
        public bool removeComments { get; set; }
        public bool sourceMap { get; set; }
        public string target { get; set; }
    }
}
