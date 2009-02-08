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
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Net;
    using Microsoft.Xna.Framework.Storage;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Holds the value of the current menuState of the game.
        /// </summary>
        private MenuState menuState;

        /// <summary>
        /// The move log of the game.
        /// </summary>
        private Log moveLog;

        /// <summary>
        /// The View of the game.
        /// </summary>
        private IView view;

        /// <summary>
        /// The model of the game.
        /// </summary>
        private Model model;

        /// <summary>
        /// The controller of the game.
        /// </summary>
        private Controller controller;

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
            this.menuState = MenuState.MainMenu;
            this.moveLog = new Log();

            this.model = new Model();
            this.view = new View2D(ref this.graphics, ref this.model, ref 
                this.moveLog);
            this.controller = new TextController();
        }

        /// <summary>
        /// An enum of the different possible states of the game.
        /// </summary>
        public enum MenuState
        {
            /// <summary>
            /// Campaign = 1, This is the first choice of the main menu, it
            /// states the player is in the campaign mode.
            /// </summary>
            Campaign,

            /// <summary>
            /// Versus = 2, This is the local multiplayer mode.
            /// </summary>
            Versus,

            /// <summary>
            /// PartyEditor = 3, A customize mode that lets players create a team for campaign or versus.
            /// </summary>
            PartyEditor,

            /// <summary>
            /// Settings = 3, General game settings.
            /// </summary>
            Settings,

            /// <summary>
            /// MainMenu = 4, This value means the game is at the main menu.
            /// </summary>
            MainMenu
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
            this.moveLog.Initialize();
            this.view.Initialize();
            this.controller.Initialize();
            this.model.Initialize();
            base.Initialize();

            menuList = new List<string>(5);
            menuList.Add("Campaign");
            menuList.Add("Versus");
            menuList.Add("Settings");
            menuList.Add("Party Editor");
            menuList.Add("Exit");
        }

        SpriteFont ChessFont;
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
            ChessFont = Content.Load<SpriteFont>("ChessFont");
            chessboard = Content.Load<Texture2D>("chessboard2");

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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (this.menuState)
            {
                case MenuState.MainMenu:
                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        this.Exit();
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
                    {
                        if (menuSelection < 4)
                        {
                            menuSelection++;
                        }
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
                    {
                        if (menuSelection > 0)
                        {
                            menuSelection--;
                        }
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    {
                        if (menuSelection == menuList.Count - 1)
                        {
                            this.Exit();
                        }

                        menuState = (MenuState)menuSelection;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Back) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
                    {
                        this.Exit();
                    }

                    break;
                case MenuState.Campaign:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
                case MenuState.PartyEditor:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
                case MenuState.Settings:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
                case MenuState.Versus:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
            }

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
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred,
                        SaveStateMode.None, SpriteScale);
            switch (this.menuState)
            {
                case MenuState.MainMenu:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    string gamename = "RPChess";

                    // Find the center of the string
                    float increment = graphics.GraphicsDevice.Viewport.Height
                        / (menuList.Count + 2);
                    Vector2 FontPos =
                        new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                        increment);
                    Vector2 FontOrigin = ChessFont.MeasureString(gamename) / 2;
                    // Draw the string
                    spriteBatch.DrawString(ChessFont, gamename, FontPos, Color.LightGreen,
                        0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    for (int i = 0; i < menuList.Count; i++)
                    {
                        // Find the center of the string
                        FontOrigin = ChessFont.MeasureString(menuList[i]) / 2;
                        FontPos.Y += increment;
                        // Draw the string
                        if (i == menuSelection)
                        {
                            spriteBatch.DrawString(
                                ChessFont,
                                menuList[i],
                                FontPos,
                                Color.Red,
                                0,
                                FontOrigin,
                                0.7f,
                                SpriteEffects.None,
                                0.5f);
                        }
                        else
                        {
                            spriteBatch.DrawString(
                                ChessFont,
                                menuList[i],
                                FontPos,
                                Color.LightGreen,
                                0,
                                FontOrigin,
                                0.5f,
                                SpriteEffects.None,
                                0.5f);
                        }
                    }
                    break;
                case MenuState.Settings:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.Campaign:
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
                    IBoardSpace p;
                    int square = graphics.GraphicsDevice.Viewport.Height / 10;
                    for (int row = 0; row < 10; row++)
                    {
                        for (int col = 0; col < 10; col++)
                        {

                            p = model[row, col];
                            if (!p.IsEmpty)
                            {
                                p.ToString();
                                spriteBatch.DrawString(
                                    ChessFont, 
                                    ((Piece)p).Symbol, 
                                    new Vector2( row * square, col * square),
                                    Color.White);
                                // TODO: Determine color of piece
                            }
                        }
                    }
                    break;
                case MenuState.Versus:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.PartyEditor:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
