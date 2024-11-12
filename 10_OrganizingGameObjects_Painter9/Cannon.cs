using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Painter
{
    class Cannon : ThreeColorGameObject
    {
        protected Texture2D cannonBarrel;
        protected Vector2 barrelOrigin;
        protected float barrelRotation;

        public Cannon(ContentManager Content) :
            base(Content.Load<Texture2D>("spr_cannon_red"),
                Content.Load<Texture2D>("spr_cannon_green"),
                Content.Load<Texture2D>("spr_cannon_blue"))
        {
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            position = new Vector2(72, 405);
            origin = new Vector2(colorRed.Width / 2, colorRed.Height / 2);
            barrelOrigin = new Vector2(cannonBarrel.Height / 2, cannonBarrel.Height / 2);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.R))
            {
                color = Color.Red;
            }
            else if (inputHelper.KeyPressed(Keys.G))
            {
                color = Color.Green;
            }
            else if (inputHelper.KeyPressed(Keys.B))
            {
                color = Color.Blue;
            }

            double opposite = inputHelper.MousePosition.Y - position.Y;
            double adjacent = inputHelper.MousePosition.X - position.X;
            barrelRotation = (float)Math.Atan2(opposite, adjacent);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cannonBarrel, position, null, Color.White, barrelRotation, barrelOrigin, 1.0f, SpriteEffects.None, 0);
            base.Draw(gameTime, spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
            barrelRotation = 0f;
        }

        public Vector2 BallPosition
        {
            get
            {
                float opposite = (float)Math.Sin(barrelRotation) * cannonBarrel.Width * 0.6f;
                float adjacent = (float)Math.Cos(barrelRotation) * cannonBarrel.Width * 0.6f;
                return position + new Vector2(adjacent, opposite);
            }
        }
    }
}