#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace AnimationAndTiles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player = new Player();
        Map map = new Map();
        SpriteAnimation spriteAnimation;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.map.TileSheet = Content.Load<Texture2D>("Tiles");
            this.map.LoadMapFromImage(Content.Load<Texture2D>("Map_2"));
            //this.spriteAnimation = new SpriteAnimation("Brawler_Evo_2", Content.RootDirectory + "/Brawler_Evo_2.xml", Content.Load<Texture2D>("Brawler_Evo_2"));
            this.spriteAnimation = new SpriteAnimation("link", Content.RootDirectory + "/link.xml", Content.Load<Texture2D>("link"));
            this.spriteAnimation.FrameDelay = 200;
            this.player.SpriteAnimation = spriteAnimation;
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.player.Update(gameTime);
            ProcessInput();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            this.map.RenderMap(spriteBatch);
            this.player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void ProcessInput()
        {
            //KeyboardState keyboardState = Keyboard.GetState();

            //if (keyboardState.IsKeyDown(Keys.S))
            //{
            //    this.player.direction = Player.Down;
            //    this.player.state = Player.Walk;

            //    this.player.Position.Y++;
            //}
            //else if (keyboardState.IsKeyDown(Keys.W))
            //{
            //    this.player.direction = Player.Up;
            //    this.player.state = Player.Walk;

            //    this.player.Position.Y--;
            //}
            //else if (keyboardState.IsKeyDown(Keys.A))
            //{
            //    this.player.direction = Player.Left;
            //    this.player.state = Player.Walk;

            //    this.player.Position.X--;
            //}
            //else if (keyboardState.IsKeyDown(Keys.D))
            //{
            //    this.player.direction = Player.Right;
            //    this.player.state = Player.Walk;

            //    this.player.Position.X++;
            //}
            //else
            //{
            //    this.player.state = Player.Idle;
            //}

            // Mouse
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 target = this.ConvertScreenToWorldPoint(mouseState.X, mouseState.Y);
                Vector2 direction = target - this.player.Position;
                this.MovePlayer(direction);
                Console.WriteLine(string.Format("X: {0} Y: {1} MousestateX: {2} MousestateY: {3} ",target.X,target.Y,mouseState.X,mouseState.Y));

                if ()
                {
                    this.player.direction = Player.Down;
                    this.player.state = Player.Walk;
                }
                else if ()
                {
                    this.player.direction = Player.Up;
                    this.player.state = Player.Walk;
                }
                else if ()
                {
                    this.player.direction = Player.Right;
                    this.player.state = Player.Walk;
                }
                else if ()
                {
                    this.player.direction = Player.Left;
                    this.player.state = Player.Walk;
                }
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    this.player.state = Player.Idle;
                }
            }
        }
            private void MovePlayer(Vector2 moveDirection)
            {
                Tile nextTile = this.map.GetTile(this.player.Position + moveDirection);
                if (nextTile != null)
                {
                    if (nextTile.Type == Tile.Types.Path)
                    {
                    this.player.Move(moveDirection);
                    }
                }
            }
            
        private Vector2 ConvertScreenToWorldPoint(int x, int y)
        {
            int tileX = x / Map.TileWidth;
            int tileY = y / Map.TileHeight;

            return new Vector2(tileX, tileY);
        }
    }
}
