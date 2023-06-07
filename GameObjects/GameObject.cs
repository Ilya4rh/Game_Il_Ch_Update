using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace MyGame
{
    public abstract class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;

        public Rectangle Bounds { get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); } }

        protected readonly Sprites Sprites;

        public GameObject(Vector2 position) 
        {
            Position = position;

            Sprites = ManagerGameObjects.Sprites;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
