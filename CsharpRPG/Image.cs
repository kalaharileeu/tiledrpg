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
    public class Image
    {
        [XmlAttribute]
        public string source;
        [XmlAttribute]
        public int width;//width in pixel
        [XmlAttribute]
        public int height;//height in pixel
        //public string Path;
        public float Alpha;
        public string Text, FontName;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public bool IsActive;
        [XmlIgnore]
        public Texture2D Texture;
        Vector2 origin;
        ContentManager content;
        RenderTarget2D renderTarget;//Something clever?
        SpriteFont font;
        //This created with ImageEffect class in mind
        Dictionary<string, ImageEffect> effectList;
        //This created with ImageEffect class in mind
        public string Effects;
        [XmlIgnore]
        public Vector2 amountofframes;


        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;

        void SetEffect<T>(ref T effect)//refernce to the effect so that we can modify it
        {

            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            if(!effectList.ContainsKey((effect.GetType().ToString().Replace("CsharpRPG.", ""))))
                effectList.Add(effect.GetType().ToString().Replace("CsharpRPG.", ""), (effect as ImageEffect));

        }

        public void ActivateEffect(string effect)
        {
            if(effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
                if ( effect == "SpriteSheetEffect")
                    effectList[effect].SetAmountFrames(this.amountofframes);

            }
        }

        public void DeactivateEffect(string effect)
        {
            if(effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()//Store the effects of the class in the effects string
        {
            Effects = String.Empty;
            foreach (var effect in effectList)
            {
                if(effect.Value.IsActive)
                    Effects += effect.Key +  ":";
            }
            if(Effects != String.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach(var effect in effectList)
                DeactivateEffect(effect.Key);

            string[] split = Effects.Split(':');
            foreach (string s in split)
                ActivateEffect(s); 
        }

        public Image()
        {
            source = Text = Effects  = String.Empty;//source used to be path
            FontName = "Fonts/Kootenay";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
            amountofframes = new Vector2(5, 1);//This is the default value for player
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (source != String.Empty)
                Texture = content.Load<Texture2D>(source);

            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if(Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y = font.MeasureString(Text).Y;

             if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            //The screen is its own render target
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.Spritebatch.Begin();
            if(Texture != null)
                ScreenManager.Instance.Spritebatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.Spritebatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.Spritebatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);

            if(Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);

        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);//this if mainly for a zoom affect
            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha,
                0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
