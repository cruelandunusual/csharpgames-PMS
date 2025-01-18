using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snow
{
    class Snowflake
    {
        protected Texture2D sprite;
        protected Vector2 position, velocity;
        //protected double velocityOffset;
        protected float amplitude;
        protected double freq;
        protected float scale;

        public Snowflake(Texture2D spr)
        {
            sprite = spr;
            SetRandomValues();
        }

        public virtual void Update(GameTime gameTime)
        {
            float velocityOffset = amplitude * (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds * freq);
            position += (velocity + new Vector2(velocityOffset, 0)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (position.Y > Snow.Screen.Y)
            {
                SetRandomValues();
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public virtual void SetRandomValues()
        {
            position = new Vector2(Snow.Random.Next(800), -Snow.Random.Next(500));
            velocity = new Vector2(0, (float)(0.2 + Snow.Random.NextDouble() * 30));
            amplitude = (float)Snow.Random.NextDouble() * 5;
            freq = Snow.Random.NextDouble() / 700;
            scale = (float)(Snow.Random.NextDouble() * 0.9 + 0.1);
        }

        public Vector2 Position
        {
            get { return position; }
        }
    }

}