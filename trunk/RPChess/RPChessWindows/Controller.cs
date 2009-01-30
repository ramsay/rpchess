///<summary>
/// Handles the controller interfaces.
///</summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    /// <summary>
    /// Interface to keep all future controllers interchangeable.
    /// </summary>
    interface Controller : IObject
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
        public TextController()
        {
        }
        public TextController(bool init)
        {
            if ( init )
            {
                Initialize();
            }
        }
        public void Initialize()
        {
			_count = 0;
        }
    }
}
