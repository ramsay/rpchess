using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
	enum ViewType { Text, TwoD, ThreeD };
    interface View
    {
        /// <summary>
        /// 
        /// </summary>
        ViewType Type
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        long LastMove
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        DateTime LastTime
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
		void Initialize();
        /// <summary>
        /// 
        /// </summary>
        void Update();
    }

    class TextView : View
    {
        ////private IBoardSpace[][] _BoardState;
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
		/// <summary>
		/// Updates the view. Consider merging with Game1.cs or bringing other
        /// game elements in (Menu, Settings, etc.).
		/// </summary>
        public void Update()
        {
            Console.Clear();
			Console.Write(Board.Instance);
			Console.Write("Next Move>");
			this._lastTime = DateTime.Now;
        }
    }
}
