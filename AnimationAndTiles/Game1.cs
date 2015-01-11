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
            this.IsMouseVisible = true;
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
            MouseState MS = Mouse.GetState();

            Vector2 mousePosition = new Vector2(MS.X, MS.Y);
            Vector2 direction = mousePosition - this.player.Position * 33;

            float distance = calculateDistance(mousePosition, this.player.Position, mousePosition);
            this.player.velocity = Vector2.Zero;


            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            Tile nextTile = this.map.GetTile(this.player.Position + direction);

            if (MS.LeftButton == ButtonState.Pressed && nextTile != null)
            {
                if (nextTile.Type == Tile.Types.Path)
                {
                    if (distance < this.player.baseSpeed)
                    {
                        this.player.velocity += direction * distance / 20;
                    }
                    else
                    {
                        this.player.velocity += direction * this.player.baseSpeed / 20;
                    }
                }
            }

            this.player.Position += this.player.velocity;
        }

        private float calculateDistance(Vector2 A, Vector2 B, Vector2 direction)
        {
            A = new Vector2(Math.Abs(A.X), Math.Abs(A.Y));
            B = new Vector2(Math.Abs(B.X), Math.Abs(B.Y));

            float yDiff, xDiff, distance;
            yDiff = A.Y - B.Y;
            xDiff = A.X - B.X;

            AnimatePlayer(direction);

            distance = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
            return Math.Abs(distance);
        }
        private void AnimatePlayer(Vector2 direction)
        {
            float x = (this.player.Position.X * 33) - direction.X;
            float y = (this.player.Position.Y * 33) - direction.Y;
            Console.WriteLine(x);
            Console.WriteLine(y);
            if (x > y)
            {
                if (x < 0)
                {
                    this.player.state = Player.Walk;
                    this.player.direction = Player.Down;
                }
                else
                {
                    this.player.state = Player.Walk;
                    this.player.direction = Player.Left;
                }
            }
            else if (y > x)
            {
                if (y < 0)
                {
                    this.player.state = Player.Walk;
                    this.player.direction = Player.Right;
                }
                else
                {
                    this.player.state = Player.Walk;
                    this.player.direction = Player.Up;
                }
            }
            else
            {
                this.player.state = Player.Idle;
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
