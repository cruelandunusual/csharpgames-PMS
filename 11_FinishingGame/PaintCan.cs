using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Painter
{
    class PaintCan : ThreeColorGameObject
    {
        protected Color targetColor;
        protected float minVelocity;
        protected float positionOffset;
        protected SoundEffect collectPoints;

        public PaintCan(ContentManager Content, float positionOffset, Color targetColor)
            : base(Content.Load<Texture2D>("spr_can_red"),
                   Content.Load<Texture2D>("spr_can_green"),
                   Content.Load<Texture2D>("spr_can_blue"))
        {
            this.positionOffset = positionOffset;
            collectPoints = Content.Load<SoundEffect>("snd_collect_points");
            this.targetColor = targetColor;
            minVelocity = 30;
            Reset();
        }

        public override void Update(GameTime gameTime)
        {
            if (velocity.Y == 0.0f && Painter.Random.NextDouble() < 0.01)
            {
                velocity = CalculateRandomVelocity();
                Color = CalculateRandomColor();
            }
            Vector2 distanceVector = ((Painter.GameWorld.Ball.Position + Painter.GameWorld.Ball.Center) - (position + Center));
            if (Math.Abs(distanceVector.X) < Center.X && Math.Abs(distanceVector.Y) < Center.Y)
            {
                Color = Painter.GameWorld.Ball.Color;
                Painter.GameWorld.Ball.Reset();
            }

            if (Painter.GameWorld.IsOutsideWorld(position))
            {
                if (color == targetColor)
                {
                    Painter.GameWorld.Score += 10;
                    collectPoints.Play();
                }
                else
                {
                    Painter.GameWorld.Lives--;
                }
                Reset();
            }
            minVelocity += 0.001f;
            rotation = (float)Math.Sin(position.Y / 50) * 0.5f;
            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
            position = new Vector2(positionOffset, -colorRed.Height);
            velocity = Vector2.Zero;
        }

        public void ResetMinVelocity()
        {
            minVelocity = 30;
        }

        public Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minVelocity);
        }

        public Color CalculateRandomColor()
        {
            int randomVal = Painter.Random.Next(3);
            if (randomVal == 0)
            {
                return Color.Red;
            }
            else if (randomVal == 1)
            {
                return Color.Green;
            }
            else
            {
                return Color.Blue;
            }
        }
    }

}