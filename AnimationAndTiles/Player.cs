using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AnimationAndTiles
{
    class Player
    {
        public const string Down = "Down";
        public const string Up = "Up";
        public const string Left = "Left";
        public const string Right = "Right";

        public const string Idle = "Idle";
        public const string Walk = "Walk";

        public SpriteAnimation SpriteAnimation { get; set;}

        public Vector2 Position = new Vector2(1,1);
        public string state = Walk;
        public string direction = Up;

        public int baseSpeed = 1;
        public Vector2 velocity;
        
        
        public void Update(GameTime gameTime)
        {
            this.PlayAnimation();
            this.SpriteAnimation.Position = this.Position;
            this.SpriteAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            this.SpriteAnimation.Draw(spritebatch);
        }

        private void PlayAnimation()
        {
            this.SpriteAnimation.PlayAnimation(string.Format("{0}_{1}", this.state, this.direction));
        }
        public void Move(Vector2 moveDirection)
        {
            this.Position += moveDirection;
        }
    }
}
