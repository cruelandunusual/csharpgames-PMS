using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Painter
{
    class PaintCan
    {
        Texture2D colorRed, colorGreen, colorBlue;
        Vector2 position, velocity;
        Color color, targetcolor;
        float minVelocity;

        public PaintCan(ContentManager Content, float positionOffset, Color targetcol)
        {
            colorRed = Content.Load<Texture2D>("spr_can_red");
            colorGreen = Content.Load<Texture2D>("spr_can_green");
            colorBlue = Content.Load<Texture2D>("spr_can_blue");
            targetcolor = targetcol;
            minVelocity = 30;
            color = Color.Blue;
            position = new Vector2(positionOffset, -colorRed.Height);
            velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            if (velocity.Y == 0.0f && Painter.Random.NextDouble() < 0.01)
            {
                velocity = CalculateRandomVelocity();
                color = CalculateRandomColor();
            }
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Painter.GameWorld.IsOutsideWorld(position))
            {
                Reset();
            }
            minVelocity += 0.001f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (color == Color.Red)
            {
                spriteBatch.Draw(colorRed, position, Color.White);
            }
            else if (color == Color.Green)
            {
                spriteBatch.Draw(colorGreen, position, Color.White);
            }
            else
            {
                spriteBatch.Draw(colorBlue, position, Color.White);
            }
        }

        public void Reset()
        {
            color = Color.Blue;
            position.Y = -colorRed.Height;
            velocity = Vector2.Zero;
        }

        public Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minVelocity);
        }

        public Color CalculateRandomColor()
        {
            int randomval = Painter.Random.Next(3);
            if (randomval == 0)
                return Color.Red;
            else if (randomval == 1)
                return Color.Green;
            else
                return Color.Blue;
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

        public Vector2 Center
        {
            get { return new Vector2(colorRed.Width, colorRed.Height) / 2; }
        }
    }

}