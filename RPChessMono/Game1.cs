//-----------------------------------------------------------------------
// <copyright file="Game1.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    //using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    //using Microsoft.Xna.Framework.Net;
    using Microsoft.Xna.Framework.Storage;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        /// <summary>
        /// The model of the game.
        /// </summary>
        private chesswar.Model chess;

        
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initializes a new instance of the Game1 class that actually runs 
        /// the game loop.
        /// </summary>
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.chess = new chesswar.Model();
        }

        List<string> menuList;
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        Texture2D chessboard;
        Matrix SpriteScale;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            chessboard = Content.Load<Texture2D>("chessboard");

            // Default resolution is 800x600; scale sprites up or down based on
            // current viewport
            float screenscale = (float)graphics.GraphicsDevice.Viewport.Width / 800f;
            // Create the scale transform for Draw. 
            // Do not scale the sprite depth (Z=1).
            SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        GamePadState previousState;
        KeyboardState previousKeyboardState;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            KeyboardState currentKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Returns a Rectangle that is safe to draw dependent on platform.
        /// </summary>
        /// <param name="percent">How much of the screen to use.</param>
        /// <returns>A rectangle that is safe for drawing.</returns>
        protected Rectangle GetTitleSafeArea(float percent)
        {
            Rectangle retval = new Rectangle(graphics.GraphicsDevice.Viewport.X,
                graphics.GraphicsDevice.Viewport.Y,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height);
#if XBOX
        // Find Title Safe area of Xbox 360.
        float border = (1 - percent) / 2;
        retval.X = (int)(border * retval.Width);
        retval.Y = (int)(border * retval.Height);
        retval.Width = (int)(percent * retval.Width);
        retval.Height = (int)(percent * retval.Height);
        return retval;            
#else
            return retval;
#endif
        }

        int menuSelection = 0;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred,
            //            SaveStateMode.None, SpriteScale);
            this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            ////view.Draw(gameTime);
            // Draw chess board.
            spriteBatch.Draw(
                chessboard,
                Vector2.Zero,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                (float)graphics.GraphicsDevice.Viewport.Height /
                (float)chessboard.Height,
                SpriteEffects.None,
                0.0f);

            // Draw each piece.
            chesswar.IBoardSpace p;
            int square = graphics.GraphicsDevice.Viewport.Height / 10;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {

                    p = chess[row, col];
                    if (p != null)
                    {
                        p.ToString();
                        //spriteBatch.DrawString(
                        //    ChessFont, 
                        //    ((Piece)p).Symbol, 
                        //    new Vector2( row * square, col * square),
                        //    Color.White);
                        // TODO: Determine color of piece
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
