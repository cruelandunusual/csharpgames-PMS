using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Painter
{

    class Ball : ThreeColorGameObject
    {
        protected bool shooting;

        public Ball(ContentManager Content) : base(Content.Load<Texture2D>("spr_ball_red"),
            Content.Load<Texture2D>("spr_ball_green"), Content.Load<Texture2D>("spr_ball_blue"))
        {
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonPressed() && !shooting)
            {
                shooting = true;
                velocity = (inputHelper.MousePosition - position) * 1.2f;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (shooting)
            {
                velocity.X *= 0.99f;
                velocity.Y += 6;
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
            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
            velocity = Vector2.Zero;
            position = new Vector2(65, 390);
            shooting = false;
        }
    }
}