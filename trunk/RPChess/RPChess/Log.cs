using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    /// <summary>
    /// A lazy mans message storage / callback.
    /// The Log class maintains a list of all moves made
    /// throughout the game. 
    /// </summary>
    public class Log
    {
        private List<String> _moveList;
        /// <summary>
        /// An immutable array of Strings that allows
        /// all other classes in the RPChess
        /// </summary>
        public String[] MoveList
        {
            get
            {
                return _moveList.ToArray();
            }
        }
        /// <summary>
        /// The number of moves in the Log.
        /// </summary>
        public int Count
        {
            get
            {
                return _moveList.Count;
            }
        }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Log()
        {
        }
        /// <summary>
        /// Constructor that also initializes.
        /// </summary>
        /// <param name="init">Initializes if true</param>
        public Log(bool init)
        {
            if (init)
            {
                initialize();
            }
        }
        /// <summary>
        /// Initializes the _moveList.  Emptys it if there were entrys.
        /// </summary>
        public void initialize()
        {
            if (_moveList.Count != 0)
            {
                _moveList.Clear();
            }
            _moveList = new List<String>();
        }
        /// <summary>
        /// Adds a move to the Log.
        /// </summary>
        /// <param name="move">A string containing the piece moved, and the move it made.</param>
        /// <returns>The new Log.Count</returns>
        public int add(String move)
        {
            _moveList.Add(move);
            return _moveList.Count;
        }
        /// <summary>
        /// An easy method to get the last move on the log without
        /// instantiating a new Array.
        /// </summary>
        /// <returns>The latest string log entry.</returns>
        public String peek()
        {
            return _moveList[_moveList.Count-1];
        }
    }
}
