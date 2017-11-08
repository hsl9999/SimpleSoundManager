﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightGive
{
	[System.Serializable]
	public class BackGroundMusicPlayer : MonoBehaviour
	{
		public AudioSource audioSource;

		private IEnumerator fadeInMethod;
		private IEnumerator fadeOutMethod;
		private float volume;
		private float fadeVolume;
		private float loopStartTime;
		private float loopEndTime;
		private bool isFade;
		private bool isPlaying;
		private bool isCheckLoopPoint;


		public bool IsPlaying { get { return isPlaying; } }

		public BackGroundMusicPlayer()
		{
			loopStartTime = 0.0f;
			loopEndTime = 0.0f;

			isPlaying = false;
			isFade = false;
			isCheckLoopPoint = false;
		}

		void Awake()
		{
			fadeVolume = 1.0f;
			audioSource = this.gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.loop = true;
			audioSource.spatialBlend = 0.0f;
			audioSource.outputAudioMixerGroup = SimpleSoundManager.Instance.bgmAudioMixerGroup;
			audioSource.volume = SimpleSoundManager.Instance.BGMVolume;
		}

		public void Play(AudioClip _clip, bool _isLoop, float _volume, float _loopStartTime, float _loopEndTime)
		{
			this.gameObject.SetActive(true);
			isPlaying = true;
			fadeVolume = 1.0f;
			volume = _volume;

			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.time = 0.0f;
			audioSource.volume = _volume;
			audioSource.clip = _clip;
			audioSource.Play();

			
			//ループ再生で、かつループ開始・終了位置の指定があった時
			if (_isLoop && (_loopStartTime != 0.0f || _loopEndTime != 1.0f))
			{
				isCheckLoopPoint = true;
			}
			else
			{
				isCheckLoopPoint = false;
			}

		}

		public void PlayerUpdate()
		{
			if (isCheckLoopPoint) {
				if (audioSource.time >= loopEndTime)
				{
					audioSource.time = loopStartTime;
				}
			}
		}

		public void FadeIn(float _fadeTime,float _waitTime)
		{
			fadeInMethod = _FadeIn(_fadeTime, _waitTime);
			StartCoroutine(fadeInMethod);
		}
		public void FadeOut(float _fadeTime)
		{
			fadeOutMethod = _FadeOut(_fadeTime);
			StartCoroutine(fadeOutMethod);
		}

		public void Stop()
		{
			audioSource.Stop();
			isPlaying = false;
		}

		private IEnumerator _FadeIn(float _fadeTime, float _waitTime)
		{
			var timeCnt = 0.0f;
			while (timeCnt < _fadeTime)
			{
				timeCnt += Time.deltaTime;
				fadeVolume = Mathf.Clamp01(timeCnt / _fadeTime);
				ChangeVolume();
				yield return new WaitForEndOfFrame();
			}
		}

		private IEnumerator _FadeOut(float _fadeTime)
		{
			var timeCnt = 0.0f;
			while (timeCnt < _fadeTime)
			{
				timeCnt += Time.deltaTime;
				fadeVolume = 1.0f - Mathf.Clamp01(timeCnt / _fadeTime);
				ChangeVolume();
				yield return new WaitForEndOfFrame();
			}
			Stop();
		}

		public void ChangeVolume()
		{
			var v = volume *
				fadeVolume *
				SimpleSoundManager.Instance.BGMVolume *
				SimpleSoundManager.Instance.TotalVolume;
			Debug.Log("VolumeChange" + v);
			audioSource.volume = v;		
		}
	}
}