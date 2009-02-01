namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Net;
    using Microsoft.Xna.Framework.Storage;

    /// <summary>
    /// The viewType enum makes the distinction of how the game is going
    /// to be drawn.
    /// </summary>
	public enum ViewType { Text, TwoD, ThreeD };

    /// <summary>
    /// The Interface for the different types of views of the RPChess game class.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the viewing type of the View, whether it is Text, 2D or 3D.
        /// </summary>
        ViewType Type
        {
            get;
        }

        /// <summary>
        /// Gets the last move since the screen has been drawn.
        /// </summary>
        long LastMove
        {
            get;
        }

        /// <summary>
        /// Gets the last time since the screen has been drawn.
        /// </summary>
        TimeSpan LastTime
        {
            get;
        }

        /// <summary>
        /// Resets the stored values.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Draws the screen.
        /// </summary>
        void Draw(GameTime gameTime);
    }

    public class TextView : IView
    {
		long lastMove;
        TimeSpan lastTime;

        public TextView()
        {
        }

        public long LastMove
        {
            get
            {
                return lastMove;
            }
        }

        public TimeSpan LastTime
        {
            get
            {
                return lastTime;
            }
        }

        public ViewType Type
        {
            get
            {
                return ViewType.Text;
            }
        }
		
		public void Initialize()
		{
			lastMove = 0;
            lastTime = TimeSpan.MinValue;
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
        public void Draw(GameTime gameTime)
        {
            Console.Clear();
			Console.Write(Board.Instance);
			Console.Write("Next Move>");
			this.lastTime = gameTime.TotalGameTime;
        }
    }

    public class View2D : IView
    {
        /// <summary>
        /// Stores index the last move when drawing.
        /// </summary>
        private long lastMove;

        private TimeSpan lastTime;
        
        public View2D(
            ref GraphicsDeviceManager g,
            ref Model m,
            ref Log l)
        {
        }

        /// <summary>
        /// Gets the Type of the View, always returns ViewType.TwoD.
        /// </summary>
        public ViewType Type
        {
            get
            {
                return ViewType.TwoD;
            }
        }

        /// <summary>
        /// Gets the last move that was drawn to the screen.
        /// </summary>
        public long LastMove
        {
            get
            {
                return lastMove;
            }
        }

        /// <summary>
        /// Gets the last time that View.Draw was called.
        /// </summary>
        public TimeSpan LastTime
        {
            get
            {
                return LastTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            lastMove = 0;
            lastTime = TimeSpan.MinValue;
        }

        /// <summary>
        /// Draws a chess board and pieces on it using 2D graphics.
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            // Draw chess board.
            // Draw each piece.
            foreach (List<IBoardSpace> row in Board.Instance.BoardState)
            {
                foreach (IBoardSpace space in row)
                {
                    if (!space.IsEmpty)
                    {
                        // Draw physical object / piece.
                    }
                }
            }
            ////lastMove = Log.Instance.Count();
            lastTime = gameTime.TotalGameTime;
        }
    }
}
