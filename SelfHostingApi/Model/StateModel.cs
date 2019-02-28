using System;
using System.Collections.Generic;
using System.Text;

namespace HosingConsole.Model
{
    public class StateModel
    {
        public string LastState { get; set; }

        public int Counter { get; set; }

        public DateTimeOffset LastTry { get; set; }

    }
}
