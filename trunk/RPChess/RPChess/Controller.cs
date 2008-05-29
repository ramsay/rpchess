using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
   interface Controller
    {
    }

    class TextController : Controller
    {
        private uint _count;
        public int Count
        {
            get
            {
                return (int)_count;
            }
        }
        private Log _log;
        public TextController()
        {
        }
        public TextController(bool init)
        {
            if ( init )
            {
                initialize();
            }
        }
        public void initialize()
        {
            _log = new Log();
			_count = 0;
        }
    }
}
