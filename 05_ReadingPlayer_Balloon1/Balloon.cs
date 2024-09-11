using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Balloon
{
    class Balloon : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D balloon, background;
        Vector2 balloonPosition;
        Vector2 balloonOriginOffset;

        public Balloon()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            balloon = Content.Load<Texture2D>("spr_lives");
            background = Content.Load<Texture2D>("spr_background");
            balloonOriginOffset = new Vector2(balloon.Width/2, balloon.Height/2);
        }

        protected override void Update(GameTime gameTime)
        {
            checkIfQuit();
            MouseState currentMouseState = Mouse.GetState();
            /*
             * subtract the originOffset to draw balloon at the centre of mouse position
             * using the form of spriteBatch.Draw we've been using until now
             * */
            //balloonPosition = new Vector2(currentMouseState.X, currentMouseState.Y) - balloonOriginOffset;
            
            /*
             * but the book keeps the original position code and uses an overloaded
             * form of spriteBatch.Draw which takes account of the sprite origin
             */
            balloonPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(balloon, balloonPosition, null, Color.White, 0.0f, balloonOriginOffset, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        void checkIfQuit()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }
    }
}