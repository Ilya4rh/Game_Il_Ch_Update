using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class Door : GameObject
{
    public bool IsOpen;

    public Door(Vector2 position) : base(position) 
    {
        Texture = Sprites.DoorClose;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
         spriteBatch.Draw(Texture, Bounds, Color.White);
    }

    public override void Update(GameTime gameTime)
    {
        if (ManagerGameObjects.Coins.Count == 0)
        {
            Texture = Sprites.DoorOpen;
            IsOpen = true;
        }
    }
}

