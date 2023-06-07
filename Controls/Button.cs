using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame;

public class Button
{
    private MouseState currentState;
    private MouseState previousState;
    private SpriteFont spriteFont;
    private bool isHovering;
    private Texture2D texture;

    public event EventHandler Click;
    public Vector2 Position { get; set; }
    public Rectangle Bound
    {
        get
        {
            return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }
    }
    public string Text { get; set; }

    public Button(SpriteFont font, Texture2D texture2)
    {
        spriteFont = font;
        texture = texture2;
    }

    public void Update()
    {
        previousState = currentState;
        currentState = Mouse.GetState();

        var mouseBound = new Rectangle(currentState.X, currentState.Y, 1, 1);
        isHovering = false;

        if (mouseBound.Intersects(Bound))
        {
            isHovering = true;

            if (currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                Click.Invoke(this, new EventArgs());
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var color = Color.White;

        if (isHovering)
            color = Color.Gray;

        spriteBatch.Draw(texture, Bound, color);

        if (!string.IsNullOrEmpty(Text))
        {
            var x = Bound.X + (Bound.Width / 2) - (spriteFont.MeasureString(Text).X / 2);
            var y = Bound.Y + (Bound.Height / 2) - (spriteFont.MeasureString(Text).Y / 2);

            spriteBatch.DrawString(spriteFont, Text, new Vector2(x, y), Color.White);
        }
    }
}
