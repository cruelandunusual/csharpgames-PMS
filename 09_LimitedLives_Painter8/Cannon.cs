﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Painter
{
    class Cannon
    {
        Texture2D cannonBarrel, colorRed, colorGreen, colorBlue;
        Vector2 position, barrelOrigin, colorOrigin;
        Color color;
        float angle;

        public Cannon(ContentManager Content)
        {
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            colorRed = Content.Load<Texture2D>("spr_cannon_red");
            colorGreen = Content.Load<Texture2D>("spr_cannon_green");
            colorBlue = Content.Load<Texture2D>("spr_cannon_blue");
            color = Color.Blue;
            barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
            colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
            position = new Vector2(72, 405);
        }

        public void HandleInput(InputHelper inputHelper)
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
            angle = (float)Math.Atan2(opposite, adjacent);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cannonBarrel, position, null, Color.White, angle, new Vector2(34, 34), 1.0f, SpriteEffects.None, 0);
            if (color == Color.Red)
            {
                spriteBatch.Draw(colorRed, position, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
            }
            else if (color == Color.Green)
            {
                spriteBatch.Draw(colorGreen, position, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(colorBlue, position, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
            }
        }

        public void Reset()
        {
            color = Color.Blue;
            angle = 0.0f;
        }

        public Vector2 BallPosition
        {
            get
            {
                float opposite = (float)Math.Sin(angle) * cannonBarrel.Width * 0.5f;
                float adjacent = (float)Math.Cos(angle) * cannonBarrel.Width * 0.5f;
                return position + new Vector2(adjacent, opposite);
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                if (value != Color.Red && value != Color.Green && value != Color.Blue)
                {
                    return;
                }
                color = value;
            }
        }
    }
}