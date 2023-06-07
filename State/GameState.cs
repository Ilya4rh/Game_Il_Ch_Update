using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GameState : State
    {
        private readonly List<Button> buttons = new();

        public GameState(Game1 game1, GraphicsDevice graphics, ContentManager content, int level) : base(game1, graphics, content)
        {
            ManagerGameObjects.Clear();

            ManagerGameObjects.UploadMapObjects(game, level);

            var buttonTexture = content.Load<Texture2D>("Buttons/ButtonQuitGame");

            var quitGameWindow = new Button(game.spriteFont, buttonTexture)
            {
                Position = new Vector2(1200 - buttonTexture.Width, 0)
            };

            quitGameWindow.Click += QuitGameWindow;

            buttons.Add(quitGameWindow);
        }

        private void QuitGameWindow(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphicsDevice, content));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            ManagerGameObjects.Draw(game.spriteBatch);
            foreach (var button in buttons)
                button.Draw(spriteBatch);

            if (ManagerGameObjects.Player.IsWin)
                spriteBatch.Draw(content.Load<Texture2D>("MiniWindow/VictoryWindow"), new Vector2(400, 200), Color.White);
            if (!ManagerGameObjects.Player.IsAlive)
                spriteBatch.Draw(content.Load<Texture2D>("MiniWindow/LoseWindow"), new Vector2(400, 200), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            ManagerGameObjects.Update(gameTime);

            foreach (var button in buttons)
                button.Update();
        }
    }
}
