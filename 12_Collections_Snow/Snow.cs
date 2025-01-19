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

        protected static Random random; // static Random variable accessible by other classes in namespace
        protected static Point screen; // static Point variable accessible by other classes in namespace

        private InputHelper inputHelper;

        Texture2D background, snowflake;
        IList<Snowflake> snowflakes; // declare instance variable of List of type Snowflake

        public Snow()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            snowflakes = new List<Snowflake>(); // initialise the list object
            random = new Random();
            inputHelper = new InputHelper();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            background = Content.Load<Texture2D>("spr_background");
            snowflake = Content.Load<Texture2D>("spr_snowflake"); // initialise the Texture2D snowflake variable
            Debug.WriteLine("snowflakes.Count = " + snowflakes.Count);
            while (snowflakes.Count < 500)
            {
                snowflakes.Add(new Snowflake(snowflake)); // populate the list, passing the Texture2D to the Snowflake constructor
            }
            Debug.WriteLine("snowflakes.Count = " + snowflakes.Count);
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
            //foreach (Snowflake obj in snowflakes)
            //{
            //    obj.Draw(gameTime, spriteBatch);
            //}
            int loopCount = 0;
            for (int i = snowflakes.Count-1; i >= 0; i--)
            {
                if (i % 2 == 0)
                {
                    snowflakes[i].Draw(gameTime, spriteBatch);
                    loopCount++;
                }
            }
            Debug.WriteLine("loopCount = " + loopCount);
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