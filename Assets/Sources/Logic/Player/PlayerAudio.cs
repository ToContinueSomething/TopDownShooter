using UnityEngine;

namespace Sources.Logic.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _shoot;
        [SerializeField] private AudioClip _pickItem;
        [SerializeField] private AudioClip _dontShoot;

        public void PlayShoot()
        {
            AddRandomPitch();
            _audioSource.PlayOneShot(_shoot);
        }

        public void PlayPickItem()
        {
            SetDefaultPitch();
            _audioSource.PlayOneShot(_pickItem);
        }

        public void PlayDontShoot()
        {
            AddRandomPitch();
            _audioSource.PlayOneShot(_dontShoot);
        }

        private void AddRandomPitch()
        {
            _audioSource.pitch = Random.Range(0.8f, 1.3f);
        }

        private void SetDefaultPitch()
        {
            _audioSource.pitch = 1f;
        }
    }
}