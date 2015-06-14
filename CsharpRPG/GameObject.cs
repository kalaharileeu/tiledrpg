using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    public class GameObject
    {
        public Image image;
        protected int movespeed;
        protected Vector2 velocity;
        protected string imagesource;
        protected string effects;
        protected int numframesX;
        protected int numframesY;
        protected bool dead;
        protected bool dying;
        protected int hits;
        protected int strength;

        public GameObject()
        {
           // image = new Image();
        }

        public virtual void LoadContent()
        {
            //image.LoadContent();
        }

        public virtual void UnloadContent()
        {
            //image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            //image.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
           // image.Draw(spriteBatch);
        }
    }
}