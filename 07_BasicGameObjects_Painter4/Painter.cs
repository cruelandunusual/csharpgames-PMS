using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Painter
{
    class Painter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHelper inputHelper;
        static GameWorld gameWorld;

        public Painter()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            inputHelper = new InputHelper();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameWorld = new GameWorld(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update();
            gameWorld.QuitIfEscape(inputHelper, this);
            gameWorld.HandleInput(inputHelper);
            gameWorld.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            gameWorld.Draw(gameTime, spriteBatch);
        }

        public static GameWorld GameWorld
        {
            get { return gameWorld; }
        }

    }
}