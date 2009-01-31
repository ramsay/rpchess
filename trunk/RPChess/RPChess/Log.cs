using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    /// <summary>
    /// A lazy man's message storage / callback.
    /// The Log class maintains a list of all moves made
    /// throughout the game. The list is static making 
	/// this a monostate.
    /// </summary>
    public class Log
    {
        private static List<string> _moveList;
        private static uint _references;
        private static bool _initialized;
        /// <summary>
        /// The number of moves in the Log.
        /// </summary>
        public int Count
        {
            ///<summary>
            ///Gets the number of moves.
            ///</summary>
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
            _references++;
        }
        /// <summary>
        /// Constructor that also initializes.
        /// </summary>
        /// <param name="init">Initializes if true</param>
        public Log(bool init)
        {
            _references++;
            if (init)
            {
                Initialize();
            }
        }
        /// <summary>
        /// Default destructor. Removes a reference. If 
        /// reference is less than 1 then clear _moveList.
        /// </summary>
        ~Log()
        {
            _references--;
            if (_moveList != null)
            {
                if (_references < 1)
                {
                    _moveList.Clear();
                }
            }
        }
        /// <summary>
        /// Initializes the _moveList.  Emptys it if there were entrys.
        /// </summary>
        public void Initialize()
        {
            if ( _references < 2 || !_initialized)
            {
                if (_moveList.Count != 0)
                {
                    _moveList.Clear();
                }
                _moveList = new List<string>();
                _initialized = true;
            }
        }
        /// <summary>
        /// Adds a move to the Log.
        /// </summary>
        /// <param name="move">A string containing the piece moved, and the move it made.</param>
        /// <returns>The new Log.Count</returns>
        public int add(string move)
        {
            _moveList.Add(move);
            return _moveList.Count;
        }
        /// <summary>
        /// An easy method to get the last move on the log without
        /// instantiating a new Array.
        /// </summary>
        /// <returns>The latest string log entry.</returns>
        public string peek()
        {
            return _moveList[_moveList.Count-1];
        }
        /// <summary>
        /// Clears the log.
        /// </summary>
        /// <returns>Returns the number of entries cleared.</returns>
        public int Clear()
        {
            if ( _references < 2 )
            {
                int count = _moveList.Count;
                _moveList.Clear();
                return count;
            }
            return 0;
        }
		/// <summary>
		/// Index access to all elements in the log.
		/// </summary>
		/// <returns>A string located at the index.</returns>
		public string at ( int index )
		{
			if ( index < 0 || index >= Count || Count == 0)
			{
				return "";
			}
			return _moveList[index];
		}
    }
}
