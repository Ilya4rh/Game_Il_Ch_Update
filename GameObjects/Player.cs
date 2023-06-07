using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Player : GameObject
{
    public int CountHeart;
    public int CountCoin;

    private Vector2 velocity;
    private Rectangle velocityBound;
    private readonly int walkSpeed = 10;
    private readonly int jump = 11;

    private bool isJump;
    private bool onBlock;

    private Directions direction;

    private readonly int period = 70;
    private int currentTime = 0;

    public bool IsWin = false;

    private readonly List<Block> blocks;
    private readonly List<Coin> coins;
    private readonly List<Heart> hearts;
    private readonly List<FlyEnemy> flyEnemies;
    private readonly List<Gun> guns;

    public bool IsAlive { get { return CountHeart != 0; } }

    public Player(Vector2 position) : base (position)
    {
        Texture = Sprites.PlayerStanding;

        CountHeart = ManagerGameObjects.HeartsPlayer.Count;
        CountCoin = 0;
        isJump = false;
        onBlock = false;

        blocks = ManagerGameObjects.Blocks;
        coins = ManagerGameObjects.Coins;
        hearts = ManagerGameObjects.Hearts;
        flyEnemies = ManagerGameObjects.FlyEnemies;
        guns = ManagerGameObjects.Guns;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsAlive)
        {
            MovePlayer(gameTime);

            currentTime += gameTime.ElapsedGameTime.Milliseconds;

            if (currentTime > period)
            {
                currentTime -= period;

                if (direction == Directions.Right)
                    Texture = Texture == Sprites.PlayerRunRight1 ? Sprites.PlayerRunRight2 : Sprites.PlayerRunRight1;
                else if (direction == Directions.Left)
                    Texture = Texture == Sprites.PlayerRunLeft1 ? Sprites.PlayerRunLeft2 : Sprites.PlayerRunLeft1;
                else if (direction == Directions.Top)
                    Texture = Sprites.PlayerJump1;
                else
                    Texture = Sprites.PlayerStanding;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsAlive)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }

    private void MovePlayer(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Right)) velocity.X = walkSpeed;
        else if (Keyboard.GetState().IsKeyDown(Keys.Left)) velocity.X = -walkSpeed;
        else velocity.X = 0;

        if (Keyboard.GetState().IsKeyDown(Keys.Space) && (!isJump || onBlock))
        {
            velocity.Y -= jump;
            isJump = true;
            onBlock = false;
        }

        velocityBound = Bounds;
        velocityBound.X += (int)velocity.X;
        velocityBound.Y += (int)velocity.Y;

        GetDirection();

        if (!ManagerGameObjects.mapBounds.Contains(velocityBound))
        {
            velocityBound.X -= (int)velocity.X;
            velocityBound.Y -= (int)velocity.Y;
            velocity = Vector2.Zero;
        }

        CollisionWithMapObjects();

        Position.X = velocityBound.X;
        Position.Y = velocityBound.Y;

        Gravitaion();
    }

    private void GetDirection()
    {
        if (velocity.X < 0)
            direction = Directions.Left;
        else if (velocity.X > 0)
            direction = Directions.Right;
        else if (velocity.Y < 0)
            direction = Directions.Top;
        else
            direction = Directions.Stand;
    }

    private void Gravitaion()
    {
        if (isJump || onBlock)
        {
            velocity.Y += 0.5f;

            if (Bounds.Bottom + velocity.Y == ManagerGameObjects.mapBounds.Height)
            {
                isJump = false;
                onBlock = false;
                Position.Y += velocity.Y;
                velocity.Y = 0;
            }
        }
    }

    private void CollisionWithMapObjects()
    {
        for (var i = 0; i < blocks.Count; i++)
        {
            CollisionWithBlock(blocks[i]);

            if (i < coins.Count)
                CollisionWithCoin(coins[i]);
            if (i < hearts.Count)
                CollisionWithHeart(hearts[i]);
            if (i < flyEnemies.Count)
                CollisionWithEnemy(flyEnemies[i]);
            if (i < guns.Count)
                CollisionWithCoreGun(guns[i]);
        }

        if (ManagerGameObjects.Door.IsOpen)
              CollisionWithDoor(ManagerGameObjects.Door);
            
    }

    private void CollisionWithBlock(Block block)
    {
        if (velocityBound.Intersects(block.Bounds))
        {
            velocityBound.X -= (int)velocity.X;
            velocityBound.Y -= (int)velocity.Y;
            velocity = Vector2.Zero;

            if (Bounds.Bottom == block.Bounds.Top)
            {
                onBlock = true;
                isJump = false;
            }
        }
    }

    private void CollisionWithCoin(Coin coin)
    {
        if (Bounds.Intersects(coin.Bounds) && coin.IsAlive)
        {
            coin.IsAlive = false;
            CountCoin++;
        }
    }
    
    private void CollisionWithEnemy(FlyEnemy flyEnemy)
    {
        if (Bounds.Intersects(flyEnemy.Bounds) && flyEnemy.IsAlive)
        {
            CountHeart--;
            flyEnemy.IsAlive = false;
        }
    }

    private void CollisionWithHeart(Heart heart)
    {
        if (CountHeart != 3 && Bounds.Intersects(heart.Bounds) && heart.IsAlive)
        {
            CountHeart++;
            heart.IsAlive = false;
        }
    }

    private void CollisionWithCoreGun(Gun gun)
    {
        if (Bounds.Intersects(gun.CoreGun.Bounds) && gun.CoreGun.IsAlive)
        {
            CountHeart--;
            gun.CoreGun.IsAlive = false;
        }
    }

    private void CollisionWithDoor(Door door)
    {
        if (Bounds.Intersects(door.Bounds))
            IsWin = true;
    }
}