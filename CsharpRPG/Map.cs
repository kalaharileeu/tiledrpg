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
    public class map
    {
        //Because it is a list you have to XMl element
        [XmlAttribute]
        public int width;//Amount of tiles to make up image
        [XmlAttribute]
        public int height;//Amount of tiles to make up image
        [XmlAttribute]
        public int tilewidth;//Pixel width
        [XmlAttribute]
        public int tileheight;//pixel width
        [XmlElement("tileset")]
        public tileset tilesetinfo;
        [XmlElement("tilelayer")]
        public List<tilelayer> Layer;
        [XmlElement("objectgroup")]
        public List<objectgroup> objectgroups;//In enemycreator.cs
        Vector2 TileDimensions;
        
        [XmlIgnore]
        List<AnimatedGraphics> graphicslist;
        [XmlIgnore]
        Dictionary<string, Loader.parameter> parameterdictionary;


        [XmlIgnore]
        CollisionManager collisionmanager;

        public map()
        {
            Layer = new List<tilelayer>();
            TileDimensions = Vector2.Zero;
            objectgroups = new List<objectgroup>();
            collisionmanager = new CollisionManager();
            graphicslist = new List<AnimatedGraphics>();
            parameterdictionary = new Dictionary<string, Loader.parameter>();//the data in Loader.parameter comes from xml
        }

        void LoadAnimated (Rectangle r, string animatedtype)
        {
            graphicslist.Add(new AnimatedGraphics(animatedtype, r, parameterdictionary[animatedtype]));
        }
        [XmlIgnore]
        public Dictionary<string, Loader.parameter> Parameterdictionary
        {
            get { return parameterdictionary; } 
        }
        
        public void LoadContent()
        {
            SoundManager.Instance.LoadContent();
            SoundManager.Instance.PlayMusic();
            TileDimensions.X = tilewidth;
            TileDimensions.Y = tileheight;
            foreach (tilelayer l in Layer)
            {
                l.Image = tilesetinfo.getimage;
                int spacing = tilesetinfo.spacing;
                int margin = tilesetinfo.margin;
                l.LoadContent(TileDimensions, tileheight, tilewidth, height, width, spacing, margin);
            }

            foreach (objectgroup o in objectgroups)
            {
                foreach (Enemy e in o.Enemies)
                    e.LoadContent();
            }
        }

        public void UnloadContent()
        {
            foreach (tilelayer l in Layer)
                l.UnloadContent();

            foreach (AnimatedGraphics a in graphicslist)
            {
                a.UnloadContent();
            }

            foreach (objectgroup o in objectgroups)
            {
                foreach (Enemy e in o.Enemies)
                    e.UnloadContent();
            }
        }

        public void Update(GameTime gameTime, Player player)
        {

            foreach (tilelayer l in Layer)
                l.Update(gameTime, player);

            if (player.Dying && (!player.isDead))
            {
                LoadAnimated(player.GetCurrentRect(), player.Animatedtype);
                player.Dead(true);
            }

            foreach (objectgroup o in objectgroups)
            {
                collisionmanager.checkEnemyPlayerBulletCollision(o.enemies);

                bool update = false;//do not update the list of enenmies
                int valuetoremove = 1000;//which one of the list of enemies to remove randomw high value
                int i = 0;
                foreach (Enemy e in o.Enemies)
                {
                    if (!e.Dying)
                    {
                        e.Update(gameTime);
                    }
                    else
                    {
                        LoadAnimated(e.GetCurrentRect(), e.Animatedtype);//Load the explosion if enemy dead
                        update = true;
                        valuetoremove = i;//it is important to remove
                    }
                    i++;
                }

                if(update)//Yes go for the update
                {
                    o.removeenemy(valuetoremove);//remove dying enemy from list
                    update = false;
                }

                graphicslist.RemoveAll(AnimatedGraphics => AnimatedGraphics.Dead == true);
                foreach (AnimatedGraphics a in graphicslist)
                {
                    if(!a.Dead)
                        a.Update(gameTime);
                }

                BulletHandler.Instance.update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            foreach (objectgroup o in objectgroups)
            {
                foreach (Enemy e in o.Enemies)
                    e.Draw(spriteBatch);
            }

            foreach (AnimatedGraphics a in graphicslist)
                a.Draw(spriteBatch);

            foreach (tilelayer l in Layer)
                l.Draw(spriteBatch, drawType);
        }
    }
}
