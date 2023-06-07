using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace MyGame
{
    public class Sprites
    {
        public static Game1 game;

        public Sprites(Game1 game1)
        {
            game = game1;
        }

        public static Texture2D PlayerStanding { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerStanding"); } }

        public static Texture2D PlayerRunLeft1 { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerRunLeft1"); } }

        public static Texture2D PlayerRunLeft2 { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerRunLeft2"); } }

        public static Texture2D PlayerRunRight1 { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerRunRight1"); } }

        public static Texture2D PlayerRunRight2 { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerRunRight2"); } }

        public static Texture2D PlayerJump1 { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerJump1"); } }

        public static Texture2D PlayerJump2 { get { return game.Content.Load<Texture2D>("ImageMapObjects/playerJump2"); } }

        public static Texture2D DoorClose { get { return game.Content.Load<Texture2D>("ImageMapObjects/closeDoor"); } }

        public static Texture2D DoorOpen { get { return game.Content.Load<Texture2D>("ImageMapObjects/openDoor"); } }

        public static Texture2D Heart { get { return game.Content.Load<Texture2D>("ImageMapObjects/heart"); } }

        public static Texture2D Coin { get { return game.Content.Load<Texture2D>("ImageMapObjects/coin"); } }

        public static Texture2D EnemyFlyLeft { get { return game.Content.Load<Texture2D>("ImageMapObjects/flyingEnemyLeft"); } }

        public static Texture2D EnemyFlyRight { get { return game.Content.Load<Texture2D>("ImageMapObjects/flyingEnemyRight"); } }

        public static Texture2D Gun { get { return game.Content.Load<Texture2D>("ImageMapObjects/gunRight"); } }

        public static Texture2D BackGroundGame { get { return game.Content.Load<Texture2D>("backGround"); } }

        public static Texture2D CoreGun { get { return game.Content.Load<Texture2D>("ImageMapObjects/coreGun"); } }

        public static Texture2D Block { get { return game.Content.Load<Texture2D>("ImageMapObjects/block"); } }



    }
}
