using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Painter
{

    class Painter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, cannonBarrel, balloon;
        Vector2 barrelPosition, barrelOrigin, balloonPosition, balloonOrigin;
        ButtonState left;
        Color backgroundColor;
        float angle;
        const float ZERO = 0f;

        public Painter()
        {
            this.Content.RootDirectory = "Content"; //Load the game assets from here
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("spr_background");
            balloon = Content.Load<Texture2D>("spr_lives");
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");

            barrelPosition = new Vector2(72, 405);
            barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
            balloonOrigin = new Vector2(balloon.Width, balloon.Height) / 2;
            backgroundColor = Color.White;
        }

        protected override void Update(GameTime gameTime)
        {
            QuitIfEscape();
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                double opposite = mouse.Y - barrelPosition.Y;
                double adjacent = mouse.X - barrelPosition.X;
                angle = (float)Math.Atan2(opposite, adjacent);
            }
            else
            {
                angle = ZERO;
            }
            balloonPosition = new Vector2(mouse.X, mouse.Y);
            if(mouse.RightButton == ButtonState.Pressed)
            {
                backgroundColor = Color.CornflowerBlue;
            }
            else
            {
                backgroundColor = Color.White;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, backgroundColor);
            spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(balloon, balloonPosition, null, Color.White, 0.0f, balloonOrigin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        private void QuitIfEscape()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }
    }
}