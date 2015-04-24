using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HuntTheWumpus.SharedCode.GameControl;

namespace HuntTheWumpus.Platform.Desktop.Trivia
{
    class Trivia
    {
        private List<String> _hints = new List<String>();

        public List<String> Hints
        {
            get { return _hints; }
        }
        
        public Trivia()
        {
            Log.Info("Creating Trivia...");
            _hints.AddRange(new string[] {"hint1", "hint2", "hint3"});

        }
    }
}
