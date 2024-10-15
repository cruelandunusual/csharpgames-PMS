using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Painter
{

    class GameWorld
    {
        Texture2D background;
        Ball ball;
        Cannon cannon;

        public GameWorld(ContentManager Content)
        {
            background = Content.Load<Texture2D>("spr_background");

            cannon = new Cannon(Content);
            ball = new Ball(Content);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        }

        public void Update(GameTime gameTime)
        {
            ball.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            ball.Draw(gameTime, spriteBatch);//we draw the ball before the cannon so it's hidden when not being fired
            cannon.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public Ball Ball
        {
            get { return ball; }
        }

        public Cannon Cannon
        {
            get { return cannon; }
        }

        public bool IsOutsideWorld(Vector2 position)
        {
            return position.X < 0 || position.X > Painter.Screen.X || position.Y > Painter.Screen.Y;
        }
        public void QuitIfEscape(InputHelper inputHelper, Painter painterGame)
        {
            if (inputHelper.KeyPressed(Keys.Escape))
            {
                painterGame.Exit();
            }
        }
    }
}