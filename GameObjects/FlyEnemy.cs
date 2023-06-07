using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame;

public class FlyEnemy : GameObject
{
    public bool IsAlive;

    private readonly int maxPosition;

    private readonly int minPosition;

    private readonly int speed = 1;

    private Directions moveDirection;

    public FlyEnemy(Vector2 position) : base(position)
    {
        Texture = Sprites.EnemyFlyRight;

        IsAlive = true;

        minPosition = (int)Position.X;
        maxPosition = Bounds.Right + 100;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsAlive)
        {
            if (moveDirection == Directions.Right)
                spriteBatch.Draw(Texture, Bounds, Color.White);
            else
                spriteBatch.Draw(Texture, Bounds, Color.White);
        }
        else
            ManagerGameObjects.FlyEnemies.Remove(this);
    }

    public override void Update(GameTime gameTime)
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (Bounds.Right < maxPosition && moveDirection == Directions.Right)
            Position.X += speed;
        if (Bounds.Right == maxPosition)
            moveDirection = Directions.Left;
        if (Bounds.Left > minPosition && moveDirection == Directions.Left)
            Position.X -= speed;
        if (Bounds.Left == minPosition)
            moveDirection = Directions.Right;

        Texture = moveDirection == Directions.Right ? Texture = Sprites.EnemyFlyRight : Sprites.EnemyFlyLeft;
    }
}