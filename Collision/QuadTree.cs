using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyGame;

public class QuadTree<T>
{
    private Rectangle bounds;
    private readonly int maxObjects = 100;
    private readonly int maxLevels = 10;
    private readonly int levelNode;

    private List<GameObject> objects = new List<GameObject>();
    private QuadTree<T>[] nodes = new QuadTree<T>[4];

    public QuadTree(Rectangle bounds)
    {
        this.bounds = bounds;
    }

    public QuadTree(int levelNode, Rectangle bounds)
    {
        this.levelNode = levelNode;
        this.bounds = bounds;
    }

    private void Split()
    {
        var x = bounds.X;
        var y = bounds.Y;
        var halfWidth = bounds.Width / 2;
        var halfHeight = bounds.Height / 2;

        nodes[0] = new QuadTree<T>(levelNode + 1, new Rectangle(x, y, halfWidth, halfHeight));
        nodes[1] = new QuadTree<T>(levelNode + 1, new Rectangle(x+halfWidth, y, halfWidth, halfHeight));
        nodes[2] = new QuadTree<T>(levelNode + 1, new Rectangle(x, y+halfHeight, halfWidth, halfHeight));
        nodes[3] = new QuadTree<T>(levelNode + 1, new Rectangle(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
    }

    public void Insert(GameObject mapObject)
    {
        if (objects.Count == maxObjects && levelNode < maxLevels)
        {
            Split();

            for (var i = 0; i < objects.Count; i++)
            {
                var index = GetIndex(objects[i].Bounds);

                if (index != -1)
                {
                    nodes[index].Insert(objects[i]);
                    objects.RemoveAt(i);
                    i--;
                }
            }
        }

        objects.Add(mapObject);

        if (nodes[0] != null)
        {
            var index = GetIndex(mapObject.Bounds);

            if (index != -1)
            {
                nodes[index].Insert(mapObject);
                objects.Remove(mapObject);
            }
        }
    }

    public List<GameObject> Retrieve(Rectangle targetRect)
    {
        var res = new List<GameObject>();

        if (bounds.Intersects(targetRect))
        {
            res.AddRange(objects);

            if (nodes[0] != null)
            {
                for (var i = 0; i < nodes.Length; i++)
                    res.AddRange(nodes[i].Retrieve(targetRect));
            }
        }

        return res;
    }

    public int GetIndex(Rectangle value)
    {
        var index = -1;

        var verticalMidPoint = bounds.X + bounds.Width / 2;
        var horizonaMidPoint = bounds.Y + bounds.Height / 2;

        if (value.X < verticalMidPoint && value.Right < verticalMidPoint)
        {
            if (value.Y < horizonaMidPoint && value.Bottom < horizonaMidPoint)
                index = 1;
            else if (value.Y > horizonaMidPoint)
                index = 2;
        }
        else if (value.X > verticalMidPoint)
        {
            if (value.Y < horizonaMidPoint && value.Bottom < horizonaMidPoint)
                index = 0;
            else if (value.Y > horizonaMidPoint)
                index = 3;
        }

        return index;
    }

    public void Remove(GameObject mapObject)
    {
        if (objects.Contains(mapObject))
            objects.Remove(mapObject);

        if (nodes[0] != null)
        {
            for (var i = 0; i < nodes.Length; i++)
                nodes[i].Remove(mapObject);
        }
    }

    public void Clear()
    {
        objects.Clear();

        for (var i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] != null)
            {
                nodes[i].Clear();
                nodes[i] = null;
            }
        }
    }
}
