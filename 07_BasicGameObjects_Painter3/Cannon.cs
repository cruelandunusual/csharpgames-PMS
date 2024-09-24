using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{
    class Cannon : SpriteObject
    {
        Texture2D cannonBarrel, colorRed, colorGreen, colorBlue;
        Vector2 position, barrelOrigin, colorOrigin;
        Color color;
        float angle;

        public Cannon(ContentManager Content)
        {
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            colorRed = Content.Load<Texture2D>("spr_cannon_red");
            colorGreen = Content.Load<Texture2D>("spr_cannon_green");
            colorBlue = Content.Load<Texture2D>("spr_cannon_blue");
            color = Color.Blue;
            barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
            colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
            position = new Vector2(72, 405);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cannonBarrel, position, null, Color.White, angle, barrelOrigin, 1f, SpriteEffects.None, 0);
            if (color == Color.Red)
            {
                spriteBatch.Draw(colorRed, position, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
            }
            else if (color == Color.Green)
            {
                spriteBatch.Draw(colorGreen, position, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(colorBlue, position, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
            }
        }

        public void Reset()
        {
            color = Color.Blue;
            angle = 0.0f;
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
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