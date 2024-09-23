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
        Texture2D background, cannonBarrel;
        Vector2 barrelPosition, barrelOrigin;
        float currentAngle, previousAngle;

        Texture2D colorRed, colorGreen, colorBlue;
        Texture2D currentColor;
        Vector2 colorOrigin;

        MouseState currentMouseState, previousMouseState;
        KeyboardState currentKeyboardState, previousKeyboardState;

        bool calculateAngle;

        public Painter()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            calculateAngle = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("spr_background");

            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            barrelPosition = new Vector2(72, 405);
            barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;

            colorRed = Content.Load<Texture2D>("spr_cannon_red");
            colorGreen = Content.Load<Texture2D>("spr_cannon_green");
            colorBlue = Content.Load<Texture2D>("spr_cannon_blue");
            currentColor = colorBlue;
            colorOrigin = new Vector2(currentColor.Width, currentColor.Height) / 2;
        }

        protected override void Update(GameTime gameTime)
        {
            QuitIfEscape();
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            previousAngle = currentAngle;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R)) // if R is pressed now but wasn't pressed last time through game loop
            {
                currentColor = colorRed;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G)) // if G is pressed now but wasn't pressed last time through game loop
            {
                currentColor = colorGreen;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.B) && previousKeyboardState.IsKeyUp(Keys.B)) // if B is pressed now but wasn't pressed last time through game loop
            {
                currentColor = colorBlue;
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) // if left button is pressed now but wasn't pressed last time through game loop
            {
                calculateAngle = !calculateAngle;
            }

            if (calculateAngle)
            {
                double opposite = currentMouseState.Y - barrelPosition.Y;
                double adjacent = currentMouseState.X - barrelPosition.X;
                currentAngle = (float)Math.Atan2(opposite, adjacent);
            }
            else
            {
                currentAngle = previousAngle; // leave cannon pointing at the last angle we calculated
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, currentAngle, barrelOrigin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(currentColor, barrelPosition, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
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