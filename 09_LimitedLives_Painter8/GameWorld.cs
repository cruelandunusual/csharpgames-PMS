using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Painter
{

    class GameWorld
    {
        Texture2D background, gameover;
        Ball ball;
        PaintCan can1, can2, can3;
        Cannon cannon;
        int lives;
        Texture2D livesSprite;

        public GameWorld(ContentManager Content)
        {
            gameover = Content.Load<Texture2D>("spr_gameover");
            livesSprite = Content.Load<Texture2D>("spr_lives");
            background = Content.Load<Texture2D>("spr_background");
            cannon = new Cannon(Content);
            ball = new Ball(Content);
            can1 = new PaintCan(Content, 450.0f, Color.Red); //left lane
            can2 = new PaintCan(Content, 575.0f, Color.Green); //middle lane
            can3 = new PaintCan(Content, 700.0f, Color.Blue); //right lane
            lives = 2;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (lives > 0)
            {
                cannon.HandleInput(inputHelper);
                ball.HandleInput(inputHelper);
            }
            else if(inputHelper.KeyPressed(Keys.Space)) 
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
            ball.Draw(gameTime, spriteBatch);
            cannon.Draw(gameTime, spriteBatch);
            can1.Draw(gameTime, spriteBatch);
            can2.Draw(gameTime, spriteBatch);
            can3.Draw(gameTime, spriteBatch);
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(livesSprite, new Vector2(i * livesSprite.Width + 15, 20), Color.White);
            }
            if (lives <= 0)
            {
                spriteBatch.Draw(gameover, new Vector2(Painter.Screen.X - gameover.Width, Painter.Screen.Y - gameover.Height) / 2, Color.White);
            }
            spriteBatch.End();
        }

        public void Reset()
        {
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

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
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