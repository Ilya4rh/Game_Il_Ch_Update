using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Timers;

namespace MyGame;

public class Gun : GameObject
{
    private DateTime delayFire;
    public bool CanFire 
    {   
        get
        {
            return DateTime.Now.Subtract(delayFire).TotalMilliseconds < 2500;
        }  
    }

    public bool directionIsRight;
    public CoreGun CoreGun;

    public Gun(Vector2 position) : base(position)
    {
        Texture = Sprites.Gun;

        directionIsRight = true;
    }

    public void InitializeCoreGun()
    {
        CoreGun = new CoreGun(new Vector2(Bounds.Right, Bounds.Y + 10));
        delayFire = CoreGun.delay;
    }

    public override void Update(GameTime gameTime) 
    {
        if (!CanFire)
            InitializeCoreGun();

        Shot(gameTime);
    }

    public void Shot(GameTime gameTime) => CoreGun.Update(gameTime);

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Bounds, Color.White);

        if (CoreGun != null && CoreGun.IsAlive)
            CoreGun.Draw(spriteBatch);
    }
}
