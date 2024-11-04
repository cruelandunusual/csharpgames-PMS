using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Painter
{
    class Ball
    {
        Texture2D colorRed, colorGreen, colorBlue;
        Vector2 position, velocity;
        Color color;
        bool shooting;

        public Ball(ContentManager Content)
        {
            colorRed = Content.Load<Texture2D>("spr_ball_red");
            colorGreen = Content.Load<Texture2D>("spr_ball_green");
            colorBlue = Content.Load<Texture2D>("spr_ball_blue");
            position = new Vector2(65, 390);
            velocity = Vector2.Zero;
            shooting = false;
            color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonPressed() && !shooting)
            {
                shooting = true;
                velocity = (inputHelper.MousePosition - position) * 1.2f;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (shooting)
            {
                velocity.X *= 0.99f;
                velocity.Y += 6;
                position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Color = Painter.GameWorld.Cannon.Color;
                position = Painter.GameWorld.Cannon.BallPosition - Center;
            }
            if (Painter.GameWorld.IsOutsideWorld(position))
            {
                Reset();
            }
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
            position = new Vector2(65, 390);
            velocity = Vector2.Zero;
            shooting = false;
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