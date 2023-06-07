using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Converters;

namespace MyGame
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteFont spriteFont;
        public int WidthWindow { get; private set; }
        public int HeightWindow { get; private set; }

        private State currentState;
        private State nextState;

        public void ChangeState(State state) => nextState = state;

        public Game1()
        {
            WidthWindow = 1200;
            HeightWindow = 800;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = HeightWindow; 
            graphics.PreferredBackBufferWidth = WidthWindow;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            IsFixedTimeStep = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteFont = Content.Load<SpriteFont>("spritefont");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }

            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            currentState.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}