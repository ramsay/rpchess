using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    class Log
    {
        private List<String> _moveList;
        public String[] MoveList
        {
            get
            {
                return _moveList.ToArray();
            }
        }
        public int Count
        {
            get
            {
                return _moveList.Count;
            }
        }

        public Log()
        {
            initialize();
        }
        
        protected void initialize()
        {
            _moveList = new List<String>();
        }

        public int add(String move)
        {
            _moveList.Add(move);
            return _moveList.Count;
        }

        public String peek()
        {
            return _moveList[_moveList.Count-1];
        }
    }
}
