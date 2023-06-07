using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Block : GameObject
    {
        public Block(Vector2 position) : base(position)
        {
            Texture = Sprites.Block;
        }

        public override void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(Texture, Bounds, Color.White);

        public override void Update(GameTime gameTime) { }
    }
}
