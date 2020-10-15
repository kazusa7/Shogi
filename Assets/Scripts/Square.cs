﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 盤面の1マスを表すクラス
/// </summary>
public class Square : MonoBehaviour
{
	[SerializeField]
	private Image m_PieceImage;

	private Address m_Address;
	private PieceInfo m_PieceInfo;
	private Action<Address> m_OnPressed;

	public void Setup(Address address, Action<Address> onPressed)
	{
		m_Address = address;
		m_PieceInfo = PieceInfo.OutOfBoard;
		m_PieceImage.enabled = false;
		m_OnPressed = onPressed;
	}

	public void SetPieceInfo(PieceInfo info)
	{
		m_PieceInfo = info;
		if (info > PieceInfo.Empty)
		{
			var sprite = Resources.Load<Sprite>($"Textures/japanese-chess/koma/60x64/{info.ToString()}");
			m_PieceImage.sprite = sprite;
			m_PieceImage.enabled = true;
		}
		else
		{
			m_PieceImage.sprite = null;
			m_PieceImage.enabled = false;
		}
	}

	public bool IsSelf()
	{
		return (PieceInfo.King <= m_PieceInfo && m_PieceInfo <= PieceInfo.Pro_Pawn);
	}

	public bool IsEnemy()
	{
		return (m_PieceInfo & PieceInfo.Enemy) == PieceInfo.Enemy;
	}

	public bool IsPieceEmpty => m_PieceInfo <= PieceInfo.Empty;

	public void OnPressed()
	{
		m_OnPressed.Invoke(m_Address);
	} 
}