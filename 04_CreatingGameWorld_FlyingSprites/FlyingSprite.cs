using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace FlyingSprites
{
    class FlyingSprite : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D balloon, background;
        Vector2 balloonPosition;
        float spriteX;
        float spriteY;

        public FlyingSprite()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            spriteX = 0f;
            spriteY = 0f;
        }

        protected override void LoadContent()
        {
            /*
             * pass GraphicsDevice to spriteBatch constructor
             * as a handle of the current device to draw to
            */
            spriteBatch = new SpriteBatch(GraphicsDevice);
            balloon = Content.Load<Texture2D>("spr_lives");
            background = Content.Load<Texture2D>("spr_background");
        }

        protected override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                spriteX -= 20;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                spriteX += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                spriteY -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spriteY += 2;
            }

            float xPosition = (graphics.PreferredBackBufferHeight / 2) + (gameTime.TotalGameTime.Milliseconds / 2) + spriteX;
            float yPosition = (graphics.PreferredBackBufferHeight / 2) + spriteY;
            balloonPosition = new Vector2(xPosition, yPosition);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(balloon, balloonPosition, Color.White);
            spriteBatch.End();
        }
    }
}
