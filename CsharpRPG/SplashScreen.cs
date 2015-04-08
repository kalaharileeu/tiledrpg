using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CsharpRPG
{
    //Serialize this class, must be all public stuff use [XmlIgnore] ignore]
    //Make everything public for xml serialization!!!!
    public class SplashScreen : GameScreen
    {
        public Image Image;
        //Texture2D image;
        //need to use XmlElemt"variable name"] with a list
       // [XmlElement("Path")]//All part of XmlSerialization path. When ever you find "Path" store it in path!
       // public List<string> path;//so path list got pushes with waht was in xml file Path
        //if variable same name(case) as in xml file no need to call [XmlElement"variable name"]
        //Vector is a class so add x and y to xml  hierarchy
        public Vector2 Position;//Has to be public for serilize or use [XmlIgnore] and (private or public)

        public override void LoadContent()
        {
            base.LoadContent();
            //path = "SplashScreen/clouds2";//A content pipeline file
            //path = "clouds2.png";//Had to go into image properties and edit in VS
            //image = content.Load<Texture2D>(path[0]);//the first one from the list in the xml file
            //image = content.Load<Texture2D>(Image.Path);//Image no comes from the class Image
            Image.LoadContent();
            //Image.FadeEffect.Fadespeed = 0.5f;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            //when enter make a call to change screens
            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image, Vector2.Zero, Color.White);
            //spriteBatch.Draw(image, Position, Color.White);//Position comes from the Xml file
            Image.Draw(spriteBatch);
        }
    }
}
