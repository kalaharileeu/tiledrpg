﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace CsharpRPG
{
    public class Player
    {
        public Image image;
        public Vector2 Velocity;
        public float MoveSpeed;

        bool dying = false;
        bool dead = false;
        string animatedtype;
        Rectangle deathvector;

        public Player()
        {
            Velocity = Vector2.Zero;
            animatedtype = "largeexplosion";
        }

        public void SetDying(Rectangle dv)
        {
            deathvector = new Rectangle();
            deathvector = dv;
            dying = true;
            //image.IsActive = false;
        }

        public bool isDead
        { 
            get{ return dead; }
        }

        public void Dead(bool v)
        {
            dead = v;
        }

        public bool Dying
        {
            get { return dying; }
        }

        public Rectangle GetCurrentRect()
        {
            Rectangle collisionRect = new Rectangle((int)image.Position.X, (int)image.Position.Y, image.width, image.height);
            return collisionRect;
        }

        public void LoadContent()
        {
            image.LoadContent();
            image.Position.Y = 80;
        }

        public void UnloadContent()
        {
            image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            image.IsActive = true;
            if (Velocity.X == 0)//If you do not want to do diagonal movement
            {
                if (InputManager.Instance.KeyDown(Keys.Down))
                {
                    Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else if (InputManager.Instance.KeyDown(Keys.Up))
                {
                    Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else
                {
                    Velocity.Y = 0;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
            }

            if (Velocity.Y == 0)//If you do not want to do diagonal movement
            {
                if (InputManager.Instance.KeyDown(Keys.Right))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else if (InputManager.Instance.KeyDown(Keys.Left))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;//Y coordinate for down is 0, left Y = 1, right Y= 2, up y = 3
                }
                else
                {
                    Velocity.X = 0;
                    image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
            }

            if (InputManager.Instance.KeyPressed(Keys.Space))
            {
                //image height and width is in pixels given by xml file
                BulletHandler.Instance.addPlayerBullet((int)(image.Position.X + image.width), (int)(image.Position.Y + (image.height / 2)));
                SoundManager.Instance.PlayPhaser();
            }


            if (Velocity.X == 0 && Velocity.Y == 0)//if the player is not moving
                image.IsActive = false;

            //BulletHandler.Instance.update(gameTime);
            image.Update(gameTime);
            image.Position += Velocity;
            //image.Position.X = 620;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            BulletHandler.Instance.draw(spriteBatch);
            image.Draw(spriteBatch);
        }


        public string Animatedtype 
        {
            get { return animatedtype; }
        }
    }
}
