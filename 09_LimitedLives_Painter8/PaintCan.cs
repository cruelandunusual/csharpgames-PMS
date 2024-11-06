using System;
using System.Diagnostics;
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
            this.colorRed = Content.Load<Texture2D>("spr_can_red");
            this.colorGreen = Content.Load<Texture2D>("spr_can_green");
            this.colorBlue = Content.Load<Texture2D>("spr_can_blue");
            targetcolor = targetcol;
            minVelocity = 30f;
            color = Color.Blue;
            position = new Vector2(positionOffset, -colorRed.Height);
            velocity = Vector2.Zero;
        }

        // TODO: calculate cannon ball intersecting with paint can
        public void Update(GameTime gameTime)
        {
            if (velocity.Y == 0.0f && Painter.Random.NextDouble() < 0.01)
            {
                velocity = CalculateRandomVelocity();
                color = CalculateRandomColor();
            }
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //TODO: more investigations of how the position of the ball's centre is calculated
            Vector2 distanceVector = (Painter.GameWorld.Ball.Position + Painter.GameWorld.Ball.Center) - (position + Center);
            if (Math.Abs(distanceVector.X) < Center.X && Math.Abs(distanceVector.Y) < Center.Y)
            {
                Color = Painter.GameWorld.Ball.Color;
                Painter.GameWorld.Ball.Reset();
            }

            if (Painter.GameWorld.IsOutsideWorld(position))
            {
                if (color != targetcolor)
                {
                    Painter.GameWorld.Lives--;
                }
                Reset();
            }
            minVelocity += 0.001f;
            Debug.WriteLine(minVelocity);
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

        public void ResetMinVelocity()
        {
            minVelocity = 30f;
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

        // TODO: change the name of this property...
        // thoroughly investigate what happens when
        // the game uses the current texure colour 
        // instead of the Color property
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