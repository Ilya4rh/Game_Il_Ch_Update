using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame;

public class Heart : GameObject
{
    public bool IsAlive;

    public Heart(Vector2 position) : base(position)
    {
        Texture = Sprites.Heart;

        IsAlive = true;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsAlive)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }

    public override void Update(GameTime gameTime) { }
}
