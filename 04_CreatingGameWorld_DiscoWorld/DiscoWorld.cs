using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DiscoWorld
{
    class DiscoWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color background;
        
        public DiscoWorld()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            int backgroundComponent_RED = gameTime.TotalGameTime.Minutes * 200;
            int backgroundComponent_GREEN = gameTime.TotalGameTime.Seconds * 30;
            int backgroundComponent_BLUE = gameTime.TotalGameTime.Milliseconds;

            background = new Color(
                backgroundComponent_RED,
                backgroundComponent_GREEN,
                backgroundComponent_BLUE);

        }

        protected override void Draw(GameTime gameTime)
        {
            //Console.Write(gameTime.ToString());
            GraphicsDevice.Clear(background);
        }
    }
}