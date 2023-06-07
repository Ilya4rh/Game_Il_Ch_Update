using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyGame;

public class ManagerGameObjects 
{
    private static Game1 game;

    private static readonly int unitMap = 40;

    public static readonly List<Block> Blocks = new();
    public static readonly List<Coin> Coins = new();
    public static readonly List<Heart> Hearts = new();
    public static readonly List<FlyEnemy> FlyEnemies = new();
    public static readonly List<Heart> HeartsPlayer = new();
    public static readonly List<Gun> Guns = new();

    public static Door Door;

    public static Player Player;

    public static Sprites Sprites { get { return new Sprites(game); } }

    public static readonly Rectangle mapBounds = new Rectangle(0, 40, 1200, 800);

    public static void UploadMapObjects(Game1 game1, int numLevel)
    {
        game = game1;

        var map = GetMap(numLevel);

        InitializeMapObjects(map);
    }

    private static int[,] GetMap(int numLevel)
    {
        if (numLevel == 0)
            return Levels.First;
        else if (numLevel == 1)
            return Levels.Second;
        else
            return Levels.Third;
    }

    public static void InitializeMapObjects(int[,] map)
    {
        var x = 0;
        var y = 0;

        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 1)
                    Blocks.Add(new Block(new Vector2(x, y)));
                else if (map[i, j] == 2)
                {
                    if (y == 0)
                        HeartsPlayer.Add(new Heart(new Vector2(x, y)));
                    else
                        Hearts.Add(new Heart(new Vector2(x, y)));
                }
                else if (map[i, j] == 3)
                    Coins.Add(new Coin(new Vector2(x, y)));
                else if (map[i, j] == 4)
                    Door = new Door(new Vector2(x, y - (Sprites.DoorClose.Height - unitMap)));
                else if (map[i, j] == 5)
                    FlyEnemies.Add(new FlyEnemy(new Vector2(x, y)));
                else if (map[i, j] == 6)
                    Guns.Add(new Gun(new Vector2(x, y)));
                else if (map[i, j] == 7)
                    Player = new Player(new Vector2(x, y - (Sprites.PlayerStanding.Height - unitMap)));

                x += 40;
            }

            x = 0;
            y += 40;
        }
    }

    public static void Update(GameTime gameTime)
    {
        foreach (var flyEnemy in FlyEnemies)
            flyEnemy.Update(gameTime);
        foreach (var gun in Guns)
            gun.Update(gameTime);

        Door.Update(gameTime);
        Player.Update(gameTime);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.BackGroundGame, new Vector2(0, 40), Color.White);

        for (var i = 0; i < Blocks.Count; i++)
        {
            Blocks[i].Draw(spriteBatch);
            if (i < Coins.Count) Coins[i].Draw(spriteBatch);
            if (i < Hearts.Count) Hearts[i].Draw(spriteBatch);
            if (i < FlyEnemies.Count) FlyEnemies[i].Draw(spriteBatch);
            if (i < Guns.Count) Guns[i].Draw(spriteBatch);
            if (i < Player.CountHeart) HeartsPlayer[i].Draw(spriteBatch);
        }

        Door.Draw(spriteBatch);
        Player.Draw(spriteBatch);
    }

    public static void Clear()
    {
        Blocks.Clear();
        Hearts.Clear();
        FlyEnemies.Clear();
        Guns.Clear();
        Coins.Clear();
        HeartsPlayer.Clear();
    }
}