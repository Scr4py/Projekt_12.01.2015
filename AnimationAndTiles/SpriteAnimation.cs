using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AnimationAndTiles
{
    class SpriteAnimation
    {
        public Vector2 Position {get; set;}
        private string name;
        public int FrameDelay;
        private Texture2D animationImage;
        private List<SpriteFrame> frames = new List<SpriteFrame>();
        private List<SpriteFrame> currentFrame = new List<SpriteFrame>();
        private int currentFrameCount;
        private string currentAnimationName;

        private float timer = 0;

        public SpriteAnimation(string name, string path,Texture2D animationImage)
        {
            this.name = name;
            this.animationImage = animationImage;
            //Lädt die bilder mit dem angegeben Pfad
            this.LoadFrames(path);
        }

        private void LoadFrames(string path)
        {
            // XML Reader wird erstellt
            XmlReader reader = XmlReader.Create(path);
            //Solange der Reader gelesen wird
            while (reader.Read())
            {
                //wenn das Startende Element ein <Sprite> ist
                if (reader.IsStartElement("sprite"))
                {
                    //Holt sich den namen der durch ein kleines n in der XMl datei vorhanden ist
                    string name = reader.GetAttribute("n");

                    //Wenn das n vorhanden ist
                    if (name.Contains(this.name))
                    {
                        SpriteFrame spriteframe = new SpriteFrame();
                        spriteframe.Name = name;
                        spriteframe.Bounds.X = Convert.ToInt32(reader.GetAttribute("x"));
                        spriteframe.Bounds.Y = Convert.ToInt32(reader.GetAttribute("y"));
                        spriteframe.Bounds.Width = Convert.ToInt32(reader.GetAttribute("w"));
                        spriteframe.Bounds.Height = Convert.ToInt32(reader.GetAttribute("h"));

                        //speichert alles in die Spriteframe liste
                        this.frames.Add(spriteframe);

                    }
                }
            }
        }

        private Rectangle CalculateSource()
        {
            SpriteFrame frame = this.currentFrame[currentFrameCount];
            return frame.Bounds;
        }

        private void CalculateFrame(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= this.FrameDelay)
            {
                this.timer = 0;

                if (this.currentFrameCount < this.currentFrame.Count - 1)
                {
                    this.currentFrameCount++;
                }
                else
                {
                    this.currentFrameCount = 0;
                }
            }
        }

        public void PlayAnimation(string name)
        {
            if (name != this.currentAnimationName)
            {
                this.currentFrame = this.frames.FindAll((SpriteFrame animation) => animation.Name.Contains(name));
                this.currentFrameCount = 0;
                this.currentAnimationName = name;

                if (currentFrame.Count == 0)
                {
                    throw new Exception(string.Format("No AnimationFrame found for {0}", name));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            this.CalculateFrame(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.currentFrame.Count != 0)
            {
                Rectangle source = this.CalculateSource();
                spriteBatch.Draw(this.animationImage, this.Position, source, Color.White);
            }
        }
   }
}
