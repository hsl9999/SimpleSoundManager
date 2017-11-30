﻿using UnityEngine;

/// <summary>
/// オーディオクリップの情報用のクラス
/// </summary>
[System.Serializable]
public class AudioClipInfo
{
	/// <summary>
	/// この音ファイルを使うかどうか
	/// </summary>
	[SerializeField]
	public bool isUse = true;
	/// <summary>
	/// オーディオの番号
	/// </summary>
	[SerializeField]
	public int audioNo;
	/// <summary>
	/// オーディオクリップ
	/// </summary>
	[SerializeField]
	public AudioClip clip;
	[SerializeField]
	private float volume;
	[SerializeField]
	private float pitch;

	public AudioClip AudioCilp	{ get { return clip; } }
	public float Volume		{ get { return volume; } }
	public float Picth		{ get { return pitch; } }


	/// <summary>
	/// オーディオの名前
	/// </summary>
	public string audioName
	{
		get
		{
			return clip.name;
		}
	}

	/// <summary>
	/// コンストラクタ
	/// 番号とオーディオクリップを設定
	/// </summary>
	/// <param name="_audioNo">番号</param>
	/// <param name="_clip">オーディオクリップ</param>
	public AudioClipInfo(int _audioNo, AudioClip _clip)
	{
		audioNo = _audioNo;
		clip = _clip;
	}
}