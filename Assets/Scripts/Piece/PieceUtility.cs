﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PieceUtility
{
	public static List<Address> CalcForeverMoveRange(Board board, Address from, IReadOnlyList<Address> defineRanges)
	{
		var ranges = new List<Address>();

		var tmpAddress = from;
		int defineRangeIndex = 0;
		while (true)
		{
			tmpAddress += defineRanges[defineRangeIndex];
			var square = board.GetSquare(tmpAddress);
			bool canAddAddress = tmpAddress.IsValid() && !square.IsSelf();
			if (canAddAddress)
			{
				ranges.Add(tmpAddress);
			}

			if (!canAddAddress || square.IsEnemy())
			{
				defineRangeIndex++;
				if (defineRangeIndex == defineRanges.Count)
				{
					break;
				}
				tmpAddress = from;
				continue;
			}
		}
		return ranges;
	}

	public static void RemoveSelfSquare(Board board, ref List<Address> addresses)
	{
		addresses.Where(address => !board.GetSquare(address).IsSelf()).ToList();
	}

	public static IPiece CreatePiece(PieceInfo pieceInfo)
	{
		switch (pieceInfo)
		{
			case PieceInfo.King: return new King();
			case PieceInfo.Rook: return new Rook();
			case PieceInfo.Bishop: return new Bishop();
			case PieceInfo.Gold: return new Gold();
			case PieceInfo.Silver: return new Silver();
			case PieceInfo.Knight: return new Knight();
			case PieceInfo.Lance: return new Lance();
			case PieceInfo.Pawn: return new Pawn();
			case PieceInfo.Pro_Rook: return new PromotedRoook();
			case PieceInfo.Pro_Bishop: return new PromotedBishop();
			case PieceInfo.Pro_Silver: return new PromotedSilver();
			case PieceInfo.Pro_Knight: return new PromotedKnight();
			case PieceInfo.Pro_Lance: return new PromotedLance();
			case PieceInfo.Pro_Pawn: return new PromotedPawn();
			default:
				return null;
		}
	}

	public static PieceInfo ReverseInfoToSelf(PieceInfo pieceInfo)
	{
		return pieceInfo -= PieceInfo.Enemy;
	}

	public static List<Address> VerticalRanges(int x)
	{
		var ranges = new List<Address>();
		for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
		{
			ranges.Add(new Address(x, y));
		}

		return ranges;
	}

	public static bool CanPromote(PieceInfo pieceInfo)
	{
		// 王将と金将は成らない
		if (pieceInfo == PieceInfo.King ||
			pieceInfo == PieceInfo.Gold)
		{
			return false;
		}

		return PieceInfo.Promoted > pieceInfo && pieceInfo > PieceInfo.Empty;
	} 
}
