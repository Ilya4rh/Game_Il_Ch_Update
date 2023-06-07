using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyGame;

public class CoreGun : GameObject
{
    public bool IsAlive;
    public DateTime delay;
    private int maxPosition;
    private int speed = 2;

    public CoreGun(Vector2 position) : base(position)
    {
        Texture = Sprites.CoreGun;

        IsAlive = true;

        maxPosition = Bounds.Right + 100;

        delay = DateTime.Now;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsAlive)
            spriteBatch.Draw(Texture, Bounds, Color.White);
    }

    public override void Update(GameTime gameTime)
    {
        Move();
    }

    public void Move()
    {
        if (Bounds.Right < maxPosition)
            Position.X += speed;
        if (Bounds.Right == maxPosition)
            IsAlive = false;
    }
}
