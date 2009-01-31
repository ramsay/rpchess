///<summary>
///The Main Game loop for RPChess
///</summary>
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

namespace RPChess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
		// Main Game members
        public enum MenuState
        {
            /// <summary>
            /// MainMenu = 0, This value means the game is at the main menu.
            /// </summary>
            MainMenu,
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
            /// Settings = 4, General game settings.
            /// </summary>
            Settings
        }
        private MenuState _menuState;
        // Chess members
        private Log _movelog;
        private View _view;

        private Model _model;
        private Controller _controller;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _menuState = MenuState.MainMenu;
            _movelog = new Log();

            _view = new TextView();
            _controller = new TextController();
            _model = new Model();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _movelog.Initialize();
            _view.Initialize();
            _controller.Initialize();
            _model.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
            switch (_menuState)
            {
                case MenuState.MainMenu:
                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        this.Exit();
                    break;
                case MenuState.Campaign:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        _menuState = MenuState.MainMenu;
                    break;
                case MenuState.PartyEditor:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        _menuState = MenuState.MainMenu;
                    break;
                case MenuState.Settings:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        _menuState = MenuState.MainMenu;
                    break;
                case MenuState.Versus:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        _menuState = MenuState.MainMenu;
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (_menuState)
            {
                case MenuState.MainMenu:
                    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    // 
                    break;
                case MenuState.Settings:
                    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.Campaign:
                    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.Versus:
                    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.PartyEditor:
                    graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
