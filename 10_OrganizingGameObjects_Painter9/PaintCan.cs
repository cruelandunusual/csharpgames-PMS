using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{
    class PaintCan : ThreeColorGameObject
    {
        Color targetcolor;
        float minVelocity;
        float positionOffset;

        public PaintCan(ContentManager Content, float positionOffset, Color targetcol)
            : base(Content.Load<Texture2D>("spr_can_red"),
                   Content.Load<Texture2D>("spr_can_green"),
                   Content.Load<Texture2D>("spr_can_blue"))
        {
            this.positionOffset = positionOffset;
            targetcolor = targetcol;
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
                if (color != targetcolor)
                {
                    Painter.GameWorld.Lives--;
                }
                Reset();
            }
            minVelocity += 0.001f;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (color == Color.Red)
            {
                spriteBatch.Draw(colorRed, position, null, Color.White, (float)Math.Sin(position.Y / 50.0) * 0.05f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
            else if (color == Color.Green)
            {
                spriteBatch.Draw(colorGreen, position, null, Color.White, (float)Math.Sin(position.Y / 50.0) * 0.05f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(colorBlue, position, null, Color.White, (float)Math.Sin(position.Y / 50.0) * 0.05f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
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