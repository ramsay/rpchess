//-----------------------------------------------------------------------
// <copyright file="Game1.cs" company="BENTwerx">
//     LGPL Copyright 2008 Robert Ramsay
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
        private View view;

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

            this.view = new TextView();
            this.controller = new TextController();
            this.model = new Model();
        }

        /// <summary>
        /// An enum of the different possible states of the game.
        /// </summary>
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

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
            switch (this.menuState)
            {
                case MenuState.MainMenu:                    
                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
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
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (this.menuState)
            {
                case MenuState.MainMenu:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.Settings:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.Campaign:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.Versus:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case MenuState.PartyEditor:
                    this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
