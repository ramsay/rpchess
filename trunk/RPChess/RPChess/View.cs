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

    /// <summary>
    /// Implements a text interface for the RPChess game.
    /// </summary>
    public class TextView : IView
    {
		private long lastMove;
        private TimeSpan lastTime;

        /// <summary>
        /// Instantiates a new instance of the TextView class.
        /// </summary>
        public TextView()
        {
        }

        /// <summary>
        /// Gets the index of the last move that had been displayed to the
        /// user.
        /// </summary>
        public long LastMove
        {
            get
            {
                return lastMove;
            }
        }

        /// <summary>
        /// Gets the time that has passed since the board state has been 
        /// displayed to the user.
        /// </summary>
        public TimeSpan LastTime
        {
            get
            {
                return lastTime;
            }
        }

        /// <summary>
        /// Gets the type of UI this object implements.
        /// </summary>
        public ViewType Type
        {
            get
            {
                return ViewType.Text;
            }
        }
		
        /// <summary>
        /// Resets all private members.
        /// </summary>
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

    /// <summary>
    /// Implements a 2D UI of the RPChess.
    /// </summary>
    public class View2D : IView
    {
        /// <summary>
        /// Stores index the last move when drawing.
        /// </summary>
        private long lastMove;

        /// <summary>
        /// Stores the time since the last Draw.
        /// </summary>
        private TimeSpan lastTime;

        private GraphicsDeviceManager graphics;
        private Model model;
        private Log log;

        /// <summary>
        /// Instantiates a new instace of the View2D class.
        /// </summary>
        /// <param name="g">
        /// A reference to the Xna GraphicsDeviceManager for the current game.
        /// </param>
        /// <param name="m">
        /// A reference to the Model that should be displayed.
        /// </param>
        /// <param name="l">
        /// A reference to the Log for this game.
        /// </param>
        public View2D(
            ref GraphicsDeviceManager g,
            ref Model m,
            ref Log l)
        {
            graphics = g;
            model = m;
            log = l;
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
        /// Resets all private fields.
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
