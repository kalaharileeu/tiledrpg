using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CsharpRPG
{
    //AnimatedGraphics is wher explosions and effects are created
    public class AnimatedGraphics : GameObject
    {
        //string type;
        Vector2 position;
        bool dead = false;
        int animatedlife;

        public AnimatedGraphics(string type, Rectangle r, Loader.parameter parameter)
        {
            type = parameter.type;
            imagesource = parameter.imagesource;
            effects = parameter.effects;
            movespeed = parameter.movespeed;
            numframesX = parameter.numframesX;
            numframesY = parameter.numframesY;
            animatedlife = parameter.life;
            position.X = r.X;
            position.Y = r.Y;
            LoadContent();
        }

        public bool Dead
        {
            get { return dead; }
        }

        public override void LoadContent()
        {
            //base.LoadContent();
            image = new Image();
            image.source = imagesource;
            image.Effects = effects;
            image.amountofframes.X = numframesX;
            image.amountofframes.Y = numframesY;
            image.Position.X = position.X;
            image.Position.Y = position.Y;

            image.LoadContent();//call this last after all ingae settings

        }

        public override void UnloadContent()
        {
            //base.UnloadContent();
            image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            image.Update(gameTime);
            animatedlife -= 1;
            image.IsActive = true;
            if (animatedlife == 0)
                dead = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            image.Draw(spriteBatch);
        }

    }
}
