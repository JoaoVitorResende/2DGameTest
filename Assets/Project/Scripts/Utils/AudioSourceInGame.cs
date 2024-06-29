using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class AudioSourceInGame : MonoBehaviour
    {
        [SerializeField] List<AudioClip> clips = new List<AudioClip>();
        public static AudioSourceInGame instance;

        private void Start()
        {
            instance = this;
        }

        public void PlayAudioClip(int id)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = clips[id];
            audioSource.Play();
        }
    }
}


