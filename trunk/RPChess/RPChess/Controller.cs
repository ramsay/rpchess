using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
   interface Controller
    {
       Log updateLog();
    }

    class TextController : Controller
    {
        private Log _moveLog;
        public Log Log
        {
            get
            {
                return _moveLog;
            }
        }

        public TextController()
        {
            _moveLog = new Log();
        }

        public Log updateLog()
        {
            return _moveLog;
        }
    }
}
