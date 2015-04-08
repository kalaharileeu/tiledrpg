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
    public class Layer
    {
        //class with a class, why, do not want anything else to acces it
        public class TileMap//class in a class
        {
            //Because it is a list you have to XMl element
            [XmlElement("Row")]
            public List<string> Row;
            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tile;
        public Image Image;
        public string SolidTiles, OverlayTiles;//Solidtiles for collision
        //privat list of tiles
        List<Tile> underlayTiles, overlayTiles;
        string state;

        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles = String.Empty;
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();
            Vector2 position = -tileDimensions;

            foreach(string row in Tile.Row)//This Tile is a tile map
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;
                foreach(string s in split)
                {
                    if(s != String.Empty)
                    {
                        position.X += tileDimensions.X;
                        if (!s.Contains("x"))
                        {
                            state = "Passive";//Passive state and solid state. solid state not walk throuhg
                            Tile tile = new Tile();//make a instance of a new tile

                            string str = s.Replace("[", String.Empty);
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                state = "Solid";

                            //send coordiantes to crop out the tile
                            tile.LoadContent(position, new Rectangle(
                                value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y,
                                (int)tileDimensions.X, (int)tileDimensions.Y), state);//pass state to load content

                            if (OverlayTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                overlayTiles.Add(tile);
                            else
                                underlayTiles.Add(tile);
                        }

                    }
                }
            }
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player)//ptimise
        {
            //Image.Update(gameTime);//this is not in his code
            foreach (Tile tile in underlayTiles)
                tile.Update(gameTime, ref player);

            foreach (Tile tile in overlayTiles)
                tile.Update(gameTime, ref player);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)//Optimse not to loop through everytile
        {
            List<Tile> tiles;//make a local list of tiles 
            if (drawType == "Underlay")
                tiles = underlayTiles;
            else
                tiles = overlayTiles;

            foreach(Tile tile in tiles)//serach through the local list of tiles
            {
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }

    }
}
