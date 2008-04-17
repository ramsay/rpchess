using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
	enum ViewType { Text, TwoD, ThreeD };
    interface View
    {
		ViewType Type;
		long LastMove;
		double LastTime;
		void initialize;
        void update( Log movelog, BoardSpace[] boardState);
    }

    class TextView : View
    {
		long _lastMove;
		public LastMove
		{
			get
			{
				return _lastMove;
			}
		}
		double _lastTime;
		public LastTime
		{
			get
			{
				return _lastTime;
			}
		}
		ViewType Type
		{
			get
			{
				return ViewType.Text;
			}
		}
        public TextView()
        {
        }
		
		public void initialize()
		{
			_lastMove = 0;
			_lastTime = 0;
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
			_lastMove = movelog.size();
			_lastTime = DateTime.Now();
        }
    }
}
