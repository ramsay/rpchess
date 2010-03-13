//-----------------------------------------------------------------------
// <copyright file="Controller.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace RPChess
{
    class Menu : DrawableGameComponent
    {

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
        /// The model of the game.
        /// </summary>
        private Model model;

        /// <summary>
        /// Holds the value of the current menuState of the game.
        /// </summary>
        private MenuState menuState;

        public Menu(Game game) : base(game)
        {
            menuList = new List<string>(5);
            menuList.Add("Campaign");
            menuList.Add("Versus");
            menuList.Add("Settings");
            menuList.Add("Party Editor");
            menuList.Add("Exit");
            this.menuState = MenuState.MainMenu;
            this.model = new Model();
        }

        GamePadState previousState;
        KeyboardState previousKeyboardState;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            KeyboardState currentKeyboardState = Keyboard.GetState();
            switch (this.menuState)
            {
                case MenuState.MainMenu:
                    // Allows the game to exit
                    if (currentState.Buttons.Back == ButtonState.Pressed &&
                        previousState.Buttons.Back == ButtonState.Released)
                    {
                        //this.game.Exit();
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.Down) &&
                        previousKeyboardState.IsKeyUp(Keys.Down) ||
                        currentState.DPad.Down == ButtonState.Pressed &&
                        previousState.DPad.Down == ButtonState.Released)
                    {
                        if (menuSelection < 4)
                        {
                            menuSelection++;
                        }
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.Up) &&
                        previousKeyboardState.IsKeyUp(Keys.Up) ||
                        currentState.DPad.Up == ButtonState.Pressed &&
                        previousState.DPad.Up == ButtonState.Released)
                    {
                        if (menuSelection > 0)
                        {
                            menuSelection--;
                        }
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.Enter) &&
                        previousKeyboardState.IsKeyUp(Keys.Enter) ||
                        currentState.Buttons.A == ButtonState.Pressed &&
                        previousState.Buttons.A == ButtonState.Released)
                    {
                        if (menuSelection == menuList.Count - 1)
                        {
                            //this.Exit();
                        }

                        menuState = (MenuState)menuSelection;
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.Back) &&
                        previousKeyboardState.IsKeyUp(Keys.Back) ||
                        currentState.Buttons.Back == ButtonState.Pressed &&
                        previousState.Buttons.Back == ButtonState.Released)
                    {
                        //this.Exit();
                    }

                    break;
                case MenuState.Campaign:
                    if (currentKeyboardState.IsKeyDown(Keys.Back) &&
                        previousKeyboardState.IsKeyUp(Keys.Back) ||
                        currentState.Buttons.Back == ButtonState.Pressed &&
                        previousState.Buttons.Back == ButtonState.Released)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
                case MenuState.PartyEditor:
                    if (currentKeyboardState.IsKeyDown(Keys.Back) &&
                        previousKeyboardState.IsKeyUp(Keys.Back) ||
                        currentState.Buttons.Back == ButtonState.Pressed &&
                        previousState.Buttons.Back == ButtonState.Released)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
                case MenuState.Settings:
                    if (currentKeyboardState.IsKeyDown(Keys.Back) &&
                        previousKeyboardState.IsKeyUp(Keys.Back) ||
                        currentState.Buttons.Back == ButtonState.Pressed &&
                        previousState.Buttons.Back == ButtonState.Released)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
                case MenuState.Versus:
                    if (currentKeyboardState.IsKeyDown(Keys.Back) &&
                        previousKeyboardState.IsKeyUp(Keys.Back) ||
                        currentState.Buttons.Back == ButtonState.Pressed &&
                        previousState.Buttons.Back == ButtonState.Released)
                    {
                        this.menuState = MenuState.MainMenu;
                    }

                    break;
            }

            previousState = currentState;
            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime);
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SpriteFont ChessFont;
        Texture2D chessboard;
        Matrix SpriteScale;
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;
            this.graphics = new GraphicsDeviceManager(Game);
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            ChessFont = Content.Load<SpriteFont>("ChessFont");
            chessboard = Content.Load<Texture2D>("chessboard");

            // Default resolution is 800x600; scale sprites up or down based on
            // current viewport
            float screenscale = (float)graphics.GraphicsDevice.Viewport.Width / 800f;
            // Create the scale transform for Draw. 
            // Do not scale the sprite depth (Z=1).
            SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
        }

        int menuSelection = 0;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
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