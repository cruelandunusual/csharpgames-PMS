using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Painter
{

    class Ball
    {
        Texture2D colorRed, colorGreen, colorBlue;
        Vector2 position, velocity, initialPosition;
        Color color;
        bool shooting;

        public Ball(ContentManager Content)
        {
            colorRed = Content.Load<Texture2D>("spr_ball_red");
            colorGreen = Content.Load<Texture2D>("spr_ball_green");
            colorBlue = Content.Load<Texture2D>("spr_ball_blue");
            initialPosition = new Vector2(85, 390);
            position = initialPosition;
            velocity = Vector2.Zero;
            shooting = false;
            color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonPressed() && !shooting)
            {
                shooting = true;
                //velocity = (inputHelper.MousePosition - position) * 1.2f;
                velocity = (inputHelper.MousePosition - position); // why not just have this, and deal with different velocity settings in Update?
                //Debug.WriteLine("mouse = " + inputHelper.MousePosition + " position = " + position + " velocity = " + velocity);               
            }
        }

        public void Update(GameTime gameTime)
        {
            if (shooting)
            {
                velocity.X *= 0.99f;
                velocity.Y += 6;
                position += velocity * ((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                color = Painter.GameWorld.Cannon.Color;
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
            position = Painter.GameWorld.Cannon.BallPosition - Center;
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

        public Color MyColor
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