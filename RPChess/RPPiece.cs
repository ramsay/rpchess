using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace RPChess
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class RPPiece : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int _x;
        private int _y;
        private chesswar.Piece _piece;

        public RPPiece(Game game, int X, int Y, chesswar.Piece p)
            : base(game)
        {
            // TODO: Construct any child components here
            // TODO: Figure color of piece.
            _x = X;
            _y = Y;
            this._piece = p;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        
        private SpriteBatch spriteBatch;
        private SpriteFont ChessFont;
        private Matrix SpriteScale;
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            ChessFont = Game.Content.Load<SpriteFont>("ChessFont");
            // Default resolution is 800x600; scale sprites up or down based on
            // current viewport
            float screenscale = (float)Game.GraphicsDevice.Viewport.Width / 800f;
            // Create the scale transform for Draw. 
            // Do not scale the sprite depth (Z=1).
            SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred,
                        SaveStateMode.None, SpriteScale);
            int square = Game.GraphicsDevice.Viewport.Height / 8;
            spriteBatch.DrawString(ChessFont,
                    this._piece.Symbol,
                    new Vector2(_x * square, _y * square),
                    Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}