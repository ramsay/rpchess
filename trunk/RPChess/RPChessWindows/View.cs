using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
	enum ViewType { Text, TwoD, ThreeD };
    interface View
    {
        ViewType Type
        {
            get;
        }
        long LastMove
        {
            get;
        }
        DateTime LastTime
        {
            get;
        }
		void Initialize();
        void update( Log movelog, BoardSpace[] boardState);
    }

    class TextView : View
    {
		long _lastMove;
		public long LastMove
		{
			get
			{
				return _lastMove;
			}
		}
		DateTime _lastTime;
		public DateTime LastTime
		{
			get
			{
				return _lastTime;
			}
		}
		public ViewType Type
		{
			get
			{
				return ViewType.Text;
			}
		}
        public TextView()
        {
        }
		
		public void Initialize()
		{
			_lastMove = 0;
            _lastTime = DateTime.MinValue;
			// set Buffer size (length, width)
			// set window size (length, width)
			// set Background color
			// set foreground color
			// set Window Title
		}
		
        public void update(Log movelog, BoardSpace[] boardState)
        {
			String board = _boardToString();
			Console.Clear();
			Console.Write(board);
			Console.Write("Next Move>");
			_lastMove = movelog.Count;
			_lastTime = DateTime.Now;
        }

        protected String _boardToString()
        {
            return "";
        }
    }
}
