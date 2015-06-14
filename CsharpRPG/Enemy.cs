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
    //**********************class Enemy************************//
    public class Enemy
    {
        //***********************class Propeties********************//
        public class Properties
        {
            //**********************class Property********************//
            public class Property
            {
                [XmlAttribute]
                public string name;
                [XmlAttribute]
                public string value;
            }
            [XmlElement("property")]
            public List<Property> listOfProperties;

            public Properties()
            {
                listOfProperties = new List<Property>();
            }

            public List<Property> ListOfProperties
            {
                get { return listOfProperties; }
            }
        }

        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string type;
        [XmlAttribute]
        public int x;
        [XmlAttribute]
        public int y;
        [XmlAttribute]
        public int width;
        [XmlAttribute]
        public int height;
        [XmlElement("properties")]
        public Properties properties;//Height, Width, numFrames, source
        int numFrames;
        protected Image image;
        int scrollSpeed;
        [XmlIgnore]
        Image image2;//Scrolling background
        protected bool dying = false;
        int updatedelay = 0;
        Rectangle deathvector;
        string animatedtype;
        float firing_seconds;

        public Enemy()
        {
            firing_seconds = 0;
            properties = new Properties();
            image = new Image();

            animatedtype = "smallexplosion";
            numFrames = 1;
            scrollSpeed = GameplayScreen.scrollspeed;
        }

        public void Dead(Rectangle dv)
        {
            deathvector = new Rectangle();
            deathvector = dv;
            dying = true;
        }

        public bool Dying
        {
            get { return dying; }
        }

        public string Animatedtype
        {
            get { return animatedtype; }
        }

        public string Type
        {
            get { return type; }
        }

        public Rectangle GetCurrentRect()
        {
            Rectangle collisionRect = new Rectangle((int)image.Position.X, (int)image.Position.Y, width, height);
            return collisionRect;
        }

        public virtual void LoadContent()
        {
            //Properties.Property v = new Properties.Property();

            foreach (Properties.Property v in properties.ListOfProperties)
            {
                image.Position.X = x;
                image.Position.Y = y;
                if (v.name == "Height")
                    image.height = Convert.ToInt32(v.value);
                else if (v.name == "Width")
                    image.width = Convert.ToInt32(v.value);
                else if (v.name == "numFrames")
                    numFrames = Convert.ToInt32(v.value);
                else if (v.name == "source")
                    image.source = v.value;
            }

            if (type == "ScrollingBackground")
            {
                image2 = new Image();
                foreach (Properties.Property v in properties.ListOfProperties)
                {
                    image2.Position.X = 640;
                    image2.Position.Y = y;
                    if (v.name == "Height")
                        image2.height = Convert.ToInt32(v.value);
                    else if (v.name == "Width")
                        image2.width = Convert.ToInt32(v.value);
                    else if (v.name == "numFrames")
                        numFrames = Convert.ToInt32(v.value);
                    else if (v.name == "source")
                        image2.source = v.value;
                }
                image2.LoadContent();
            }

            image.LoadContent();
        }

        public virtual void UnloadContent()
        {
            image.UnloadContent();
            if (type == "ScrollingBackground")
            {
                image2.UnloadContent();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (type == "ScrollingBackground")
            {
                updatedelay++;
                if (updatedelay > 2)
                {
                    scroll();
                    image2.IsActive = true;
                        if (image2.Position.X <= 1)
                        {
                            image2.Position.X = 640;
                            image.Position.X = 0;
                        }
                    image2.Update(gameTime);
                    image.Update(gameTime);

                    updatedelay = 0;
                }
            }
            else 
            {
                if (image.Position.X > 640)
                    scroll();
                else
                    image.Position.X -= 3;
                //if the position is smaller that 1 then it can die
                if (image.Position.X < -20)
                    dying = true;
                image.IsActive = true;
                image.Update(gameTime);
            }

            firing_seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (firing_seconds > 5.0)
            {
                BulletHandler.Instance.addEnemyBullet((int)image.Position.X, (int)image.Position.Y);
                firing_seconds = 0;
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (type == "ScrollingBackground")
            {
                image2.Draw(spriteBatch);
                image.Draw(spriteBatch);
            }
            else
                image.Draw(spriteBatch);
        }

        public void scroll()
        {
            if (this.Type != "player")
            {
                if (type == "ScrollingBackground")
                {
                    image2.Position.X -= scrollSpeed;
                    image.Position.X -= scrollSpeed;
                }
                else
                    image.Position.X -= scrollSpeed;
                
            }
        }
    }
}
