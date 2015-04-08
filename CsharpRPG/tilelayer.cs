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
    public class tilelayer
    {
        [XmlAttribute]
        public string name;
        [XmlElement("data")]
        public string data;
        [XmlIgnore]
        public Image Image;
        string state;
        List<Tile> underlayTiles;

        public string Data
        {
            get { return data; }
        }

        public tilelayer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
        }

        public void LoadContent(Vector2 tileDimensions, int tileheight, int tilewidth, int height, int width, int spacing, int margin)
        {
            Image.LoadContent();

            byte[] encodeddata = Convert.FromBase64String(data);
            string decodedString = Encoding.UTF8.GetString(encodeddata);

            List<int> tile_ids = new List<int>();
            for (int j = 0; j < (20 * 15); j++)
            {
                int i = j * 4;
                int c = decodedString[i];

                if (c < 0)
                {
                    int cp = 256 + c;
                    tile_ids.Add(cp);
                }
                else
                {
                    tile_ids.Add(c);
                }
            }

            Vector2 position = -tileDimensions;

            int numImageCol = (Image.width - margin)/(tilewidth + 2);//the (width + 2) is sell width + margin

            //The backfround is iange is 20 by 15: 
            int n = 0;
            for (int m = 0; m < height; m++)
            {
                for (int k = 0; k < width; k++)//the width is the amount of tile son the x axis
                {
                   if (tile_ids[n] != 0)
                    {
                    position.X = tileDimensions.X * k;
                    position.Y = tileDimensions.Y * m;

                    int v1 = tile_ids[n];

                    int col = (tile_ids[n] % numImageCol) - 1;//Y axes to start cropping
                    int row = (tile_ids[n] / numImageCol);//X axes col to start cropping

                    state = "Passive";//Passive state and solid state. solid state not walk throuhg
                    Tile tile = new Tile();//make a instance of a new tile
                    //send coordiantes to crop out the tile
                    //if (tile_ids[n] != 0)
                    //{
                        tile.LoadContent(position, new Rectangle(margin + col * ((int)tileDimensions.X + spacing), margin + row * ((int)tileDimensions.Y + spacing),
                            (int)tileDimensions.X, (int)tileDimensions.Y), state);//pass state to load content
                        underlayTiles.Add(tile);
                    }
                    n++;

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
            {
                tile.Update(gameTime, ref player);
                tile.scroll(1);
            }



            //foreach (Tile tile in overlayTiles)
            //    tile.Update(gameTime, ref player);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)//Optimse not to loop through everytile
        {
            List<Tile> tiles;//make a local list of tiles
            tiles = underlayTiles;
            //if (drawType == "Underlay")
            //    tiles = underlayTiles;
            //else
            //    tiles = overlayTiles;

            foreach (Tile tile in tiles)//serach through the local list of tiles
            {
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}
