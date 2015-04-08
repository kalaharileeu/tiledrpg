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
        public Vector2 Velocity;
        public float MoveSpeed;

        public GameObject()
        {
            Velocity = Vector2.Zero;
        }

        public virtual void LoadContent()
        {
            image.LoadContent();
        }

        public virtual void UnloadContent()
        {
            image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            image.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }

    }
}