using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Coin : GameObject
{
    public bool IsAlive;

    public Coin(Vector2 position) : base(position)
    {
        Texture = Sprites.Coin;

        IsAlive = true;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsAlive)
            spriteBatch.Draw(Texture, Bounds, Color.White);
        else 
            ManagerGameObjects.Coins.Remove(this);
    }

    public override void Update(GameTime gameTime) { }
}
