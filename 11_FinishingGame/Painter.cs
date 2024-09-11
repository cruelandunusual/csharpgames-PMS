using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Painter
{
    class Painter : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected InputHelper inputHelper;
        protected static GameWorld gameWorld;
        protected static Random random;
        protected static Point screen;

        public Painter()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            inputHelper = new InputHelper();
            random = new Random();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            gameWorld = new GameWorld(Content);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Content.Load<Song>("snd_music"));
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update();
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

        public static Random Random
        {
            get { return random; }
        }

        public static Point Screen
        {
            get { return screen; }
        }
    }
}