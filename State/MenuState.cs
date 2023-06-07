using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MyGame;

public class MenuState : State
{
    private List<Button> buttons;

    public MenuState(Game1 game1, GraphicsDevice graphics, ContentManager content) : base(game1, graphics, content)
    {
        var buttonTexture = content.Load<Texture2D>("Buttons/ButtonMenu");
        var spriteFont = content.Load<SpriteFont>("spritefont");

        var firstLevelButton = new Button(spriteFont, buttonTexture)
        {
            Position = new Vector2(50, 100),
            Text = "First Level"
        };

        firstLevelButton.Click += FirstLevelButtonClick;

        var secondLevelButton = new Button(spriteFont, buttonTexture)
        {
            Position = new Vector2(50, 240),
            Text = "Second Level"
        };

        secondLevelButton.Click += SecondLevelButton;

        var thirdLevelButton = new Button(spriteFont, buttonTexture)
        {
            Position = new Vector2(50, 380),
            Text = "Third Level"
        };

        thirdLevelButton.Click += ThirdLevelButton;

        var quitButton = new Button(spriteFont, buttonTexture)
        {
            Position = new Vector2(50, 580),
            Text = "Quit"
        };

        quitButton.Click += QuitButton;

        buttons = new List<Button>()
        {
            firstLevelButton, secondLevelButton, thirdLevelButton, quitButton
        };
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var button in buttons)
            button.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        graphicsDevice.Clear(Color.Green);
        foreach(var button in buttons)
            button.Draw(spriteBatch);

        spriteBatch.Draw(content.Load<Texture2D>("MiniWindow/WindowInformation"), new Vector2(600, 100), Color.White);

        spriteBatch.End();
    }

    private void FirstLevelButtonClick(object sender, EventArgs e)
    {
        var state = new GameState(game, graphicsDevice, content, 0);
        game.ChangeState(state);
    }

    private void SecondLevelButton(object sender, EventArgs e)
   {
        var gameState = new GameState(game, graphicsDevice, content, 1);
        game.ChangeState(gameState);
    }

    private void ThirdLevelButton(object sender, EventArgs e)
    {
        var state = new GameState(game, graphicsDevice, content, 2);
        game.ChangeState(state);
    }

    private void QuitButton(object sender, EventArgs e)
    {
        game.Exit();
    }
}
