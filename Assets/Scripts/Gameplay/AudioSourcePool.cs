using UnityEngine;

namespace KHCore
{
    public class AudioSourcePool : PollingPool<AudioSource>
    {
        public AudioSourcePool(AudioSource prefab) : base(prefab)
        {

        }

        protected override bool IsActive(AudioSource component)
        {
            return component.isPlaying;
        }

        public void PlayAtPoint(AudioClip clip, Vector3 point = default, bool loop = false)
        {
            var source = Get();

            source.transform.position = point;
            source.clip = clip;
            source.loop = loop;
            source.Play();
        }
    }
}
