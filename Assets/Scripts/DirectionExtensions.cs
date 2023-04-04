using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionExtensions
{
	public static Vector2Int ToVector2Int(this Direction direction)
	{
		return direction switch
		{
			Direction.North => Vector2Int.up,
			Direction.East => Vector2Int.right,
			Direction.South => Vector2Int.down,
			Direction.West => Vector2Int.left,
			_ => Vector2Int.zero,
		};
	}

	public static Direction NextClockwise(this Direction direction)
	{
        return direction switch
		{
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => direction,
        };
	}

	public static Direction NextCounterClockwise(this Direction direction)
	{
		return direction switch
		{
            Direction.North => Direction.West,
			Direction.West => Direction.South,
			Direction.South => Direction.East,
			Direction.East => Direction.North,
			_ => direction,
		};
	}

	public static Direction Opposite(this Direction direction)
	{
		return (Direction)(((int)direction + 2) % 4);
	}

	public static float ToRotation(this Direction direction)
	{
		return (int)(direction) * 90;
	}
}
