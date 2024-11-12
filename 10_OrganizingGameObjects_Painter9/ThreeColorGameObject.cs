using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{
    class ThreeColorGameObject
    {
        protected Texture2D colorRed, colorGreen, colorBlue, currentColor;
        protected Vector2 position, origin, velocity;
        protected float rotation;
        protected Color color;

        public ThreeColorGameObject(Texture2D colorRed, Texture2D colorGreen, Texture2D colorBlue)
        {
            this.colorRed = colorRed;
            this.colorGreen = colorGreen;
            this.colorBlue = colorBlue;
            this.currentColor = colorBlue;
            color = Color.Blue;
            position = Vector2.Zero;
            velocity = Vector2.Zero;
        }

        public virtual void HandleInput(InputHelper inputHelper)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (color == Color.Red)
            {
                spriteBatch.Draw(colorRed, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
            }
            else if (color == Color.Green)
            {
                spriteBatch.Draw(colorGreen, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(colorBlue, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
            }
            
        }

        public virtual void Reset()
        {
            color = Color.Blue;
        }

        public Vector2 Center
        {
            get { return new Vector2(colorRed.Width, colorRed.Height) / 2; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                if (value != Color.Red && value != Color.Green && value != Color.Blue)
                {
                    return;
                }
                color = value;
            }
        }

    }
}