using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    class ScrollingBackground : Enemy
    {
        Image image2;
        int numframes;

        public ScrollingBackground(Enemy e)
        {

        }

        public void LoadContent()
        {
            base.LoadContent();

            image2.Position.X = 0;
            image2.Position.Y = 0;
            image2.height = image.height;
            image2.width = image.width;
            image2.source = image.source;
            image2.LoadContent();
        }

        public void UnloadContent()
        {
            base.UnloadContent();

           // image2.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            //scroll();
            base.Update(gameTime);
            //image.IsActive = true;
            //image.Position.X += 1;
            //image.Position.Y += 1;
           // image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //image2.Draw(spriteBatch);
        }
    }

//    public class ScrollingBackgroundCreator
//    {
//        Enemy createGameObject()
//        {
//            return new ScrollingBackground();
//        }
//    }
}
