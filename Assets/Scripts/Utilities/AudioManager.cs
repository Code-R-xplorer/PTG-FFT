using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        public List<Audio> audios;

        private Dictionary<string, AudioSource> _audioItems;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _audioItems = new Dictionary<string, AudioSource>();
            foreach (var audio1 in audios)
            {
                var gObject = new GameObject($"{audio1.audioName} Object");
                gObject.transform.SetParent(transform);
                var audioSource = gObject.AddComponent<AudioSource>();
                audioSource.clip = audio1.audioClip;
                audioSource.volume = audio1.audioVolume;
                audioSource.playOnAwake = false;
                _audioItems.Add(audio1.audioName, audioSource);
            }
        }

        public void Play(string clipName)
        {
            if (_audioItems.TryGetValue(clipName, out var source))
            {
                source.PlayOneShot(source.clip, source.volume);
            }
        }
    }

    [Serializable]
    public class Audio
    {
        public string audioName;
        public AudioClip audioClip;
        public float audioVolume = 1;
    }
}