using System;
using Manager;
using ScriptObjects.AssetsSound;
using UnityEngine;

namespace Pool.Sound
{
    public enum SoundType
    {
        Atk,
        Hit,
        Block,
        Foot
    }
    public class PoolItemSound : PoolItemBase
    {
        private AudioSource _audioSource;
        [SerializeField] private SoundType _type;
        [SerializeField] private AssetSound _soundAssets;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
        }

        public override void Spawn()
        {
            PlaySound();
        }

        private void PlaySound()
        {
            _audioSource.clip = _soundAssets.GetAudioClip(_type);
            _audioSource.Play();
            StartRecycle();
        }

        private void StartRecycle()
        {
            TimeManager.MainInstance.TryGetOneTimer(0.3f , DisableSelf);
        }

        private void DisableSelf()
        {
            _audioSource.Stop();
            this.gameObject.SetActive(false);
        }
    }
}