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
        private const string Down = "Down";
        private const string Up = "Up";
        private const string Left = "Left";
        private const string Right = "Right";

        private const string Idle = "Idle";
        private const string Walk = "Walk";

        public SpriteAnimation SpriteAnimation { get; set;}

        private Vector2 position = Vector2.Zero;
        private string state = Idle;
        private string direction = Down;
        
        public void Update(GameTime gameTime)
        {
            this.PlayAnimation();
            this.ProcessInput();
            this.SpriteAnimation.Position = this.position;
            this.SpriteAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            this.SpriteAnimation.Draw(spritebatch);
        }

        private void ProcessInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.direction = Down;
                this.state = Walk;

                this.position.Y++;
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                this.direction = Up;
                this.state = Walk;

                this.position.Y--;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                this.direction = Left;
                this.state = Walk;

                this.position.X--;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                this.direction = Right;
                this.state = Walk;

                this.position.X++;
            }
            else
            {
                this.state = Idle;
            }
        }

        private void PlayAnimation()
        {
            this.SpriteAnimation.PlayAnimation(string.Format("{0}_{1}", this.state, this.direction));
        }
    }
}
