using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace Snow
{
    class Snow : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        protected static Random random;
        protected static Point screen;

        private InputHelper inputHelper;

        Texture2D background, snowflake;
        List<Snowflake> snowflakes;

        public Snow()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            snowflakes = new List<Snowflake>();
            random = new Random();
            inputHelper = new InputHelper();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            background = Content.Load<Texture2D>("spr_background");
            snowflake = Content.Load<Texture2D>("spr_snowflake");
            while (snowflakes.Count < 500)
            {
                snowflakes.Add(new Snowflake(snowflake));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(); //update input before anything else
            QuitIfEscape(inputHelper.KeyPressed(Keys.Escape));
            foreach (Snowflake obj in snowflakes)
            {
                obj.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            foreach (Snowflake obj in snowflakes)
            {
                obj.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public static Random Random
        {
            get { return random; }
        }

        public static Point Screen
        {
            get { return screen; }
        }

        public void QuitIfEscape(bool isEscapePressed)
        {
            if (isEscapePressed)
            {
                this.Exit();
            }
        }
    }
}