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
        public List<objectgroup> objectgroups;

        Vector2 TileDimensions;
        CollisionManager collisionmanager;

        public map()
        {
            Layer = new List<tilelayer>();
            TileDimensions = Vector2.Zero;
            objectgroups = new List<objectgroup>();
            collisionmanager = new CollisionManager();
        }

        public void LoadContent()
        {
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

            foreach (objectgroup o in objectgroups)
            {
                foreach (Enemy e in o.Enemies)
                    e.UnloadContent();
            }
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (tilelayer l in Layer)
                l.Update(gameTime, ref player);

            foreach (objectgroup o in objectgroups)
            {
                collisionmanager.checkEnemyPlayerBulletCollision(o.enemies);

                foreach (Enemy e in o.Enemies)
                    e.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            foreach (objectgroup o in objectgroups)
            {
                foreach (Enemy e in o.Enemies)
                    if(!e.Dying)
                        e.Draw(spriteBatch);
            }

            foreach (tilelayer l in Layer)
                l.Draw(spriteBatch, drawType);
        }
    }
}
