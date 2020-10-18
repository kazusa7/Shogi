﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 金将
/// </summary>
public class Gold : IPiece
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, 1),
		new Address(1, 1),
		new Address(1, 0),
		new Address(0, -1),
		new Address(-1, 0),
		new Address(-1, 1),
	};

	public bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public List<Address> MoveRanges(Board board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X, from.Y + 1));
		ranges.Add(new Address(from.X + 1, from.Y + 1));
		ranges.Add(new Address(from.X + 1, from.Y));
		ranges.Add(new Address(from.X, from.Y - 1));
		ranges.Add(new Address(from.X - 1, from.Y));
		ranges.Add(new Address(from.X - 1, from.Y + 1));

		ranges.Where(address => address.IsValid());

		ranges = PieceUtility.RemoveSelfSquare(board, ranges);
		return ranges;
	}
}
