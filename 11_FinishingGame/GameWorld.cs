using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Painter
{
    class GameWorld
    {
        protected int score;
        protected Texture2D background, gameOver;
        protected Texture2D scoreBar;
        protected SpriteFont gameFont;

        protected Ball ball;
        protected PaintCan can1, can2, can3;
        protected Cannon cannon;
        protected int lives;
        protected Texture2D livesSprite;

        public GameWorld(ContentManager Content)
        {
            gameFont = Content.Load<SpriteFont>("PainterFont");

            gameOver = Content.Load<Texture2D>("spr_gameover");
            livesSprite = Content.Load<Texture2D>("spr_lives");

            background = Content.Load<Texture2D>("spr_background");
            scoreBar = Content.Load<Texture2D>("spr_scorebar");
            cannon = new Cannon(Content);
            ball = new Ball(Content);
            can1 = new PaintCan(Content, 450.0f, Color.Red);
            can2 = new PaintCan(Content, 575.0f, Color.Green);
            can3 = new PaintCan(Content, 700.0f, Color.Blue);

            lives = 5;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (lives > 0)
            {
                cannon.HandleInput(inputHelper);
                ball.HandleInput(inputHelper);
            }
            else if (inputHelper.KeyPressed(Keys.Space))
            {
                Reset();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (lives <= 0)
            {
                return;
            }
            ball.Update(gameTime);
            can1.Update(gameTime);
            can2.Update(gameTime);
            can3.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(scoreBar, new Vector2(10, 10), Color.White);
            ball.Draw(gameTime, spriteBatch);
            cannon.Draw(gameTime, spriteBatch);
            can1.Draw(gameTime, spriteBatch);
            can2.Draw(gameTime, spriteBatch);
            can3.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(gameFont, "Score: " + score, new Vector2(20, 18), Color.White);
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(livesSprite, new Vector2(i * livesSprite.Width + 15, 60), Color.White);
            }
            if (lives <= 0)
            {
                spriteBatch.Draw(gameOver, new Vector2(Painter.Screen.X - gameOver.Width, Painter.Screen.Y - gameOver.Height) / 2, Color.White);
            }
            spriteBatch.End();
        }

        public void Reset()
        {
            score = 0;
            lives = 5;
            cannon.Reset();
            ball.Reset();
            can1.Reset();
            can2.Reset();
            can3.Reset();
            can1.ResetMinVelocity();
            can2.ResetMinVelocity();
            can3.ResetMinVelocity();
        }

        public Ball Ball
        {
            get { return ball; }
        }

        public Cannon Cannon
        {
            get { return cannon; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public bool IsOutsideWorld(Vector2 position)
        {
            return position.X < 0 || position.X > Painter.Screen.X || position.Y > Painter.Screen.Y;
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }
    }

}