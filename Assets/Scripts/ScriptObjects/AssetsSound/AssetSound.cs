using System;
using System.Collections.Generic;
using Pool.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptObjects.AssetsSound
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Create/Assets/Sound", order = 0)]
    public class AssetSound : ScriptableObject
    {
        [Serializable]
        private class Sounds
        {
            public SoundType SoundType;
            public AudioClip[] AudioClips;
        }

        [SerializeField] private List<Sounds> _configSounds = new List<Sounds>();

        public  AudioClip GetAudioClip(SoundType type)
        {
            if (_configSounds.Count == 0) return null;
            switch (type)
            { 
                case SoundType.Atk:
                    return _configSounds[0].AudioClips[Random.Range(0, _configSounds[0].AudioClips.Length)];

                case SoundType.Hit:
                    return _configSounds[1].AudioClips[Random.Range(0, _configSounds[1].AudioClips.Length)];

                case SoundType.Block:
                    return _configSounds[2].AudioClips[Random.Range(0, _configSounds[2].AudioClips.Length)];

                case SoundType.Foot:
                    return _configSounds[3].AudioClips[Random.Range(0, _configSounds[3].AudioClips.Length)];

            }
            
            return null;
        }
    }
}