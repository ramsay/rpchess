using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    interface View
    {

        void update( Log movelog );
    }

    class TextView : View
    {
        public TextView()
        {
        }

        public void update(Log movelog)
        {
        }
    }
}
