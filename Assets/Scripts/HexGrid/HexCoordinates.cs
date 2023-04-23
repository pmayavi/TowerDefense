using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, y;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public int Z
    {
        get
        {
            return -X - Y;
        }
    }

    public HexCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int y)
    {
        return new HexCoordinates(x - y / 2, y);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + ", " + Y.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString() + "\n" + Y.ToString();
    }
}
