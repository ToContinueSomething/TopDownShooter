using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Logic.Enemy
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _dieClip;
        
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Die()
        {
            _audioSource.pitch = Random.Range(0.8f, 1.3f);
            _audioSource.PlayOneShot(_dieClip);
        }
    }
}