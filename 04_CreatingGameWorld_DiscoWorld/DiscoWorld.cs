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
            int red = gameTime.TotalGameTime.Milliseconds;
            Console.WriteLine(red);
            background = new Color(red, 0, 0);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);
        }
    }
}