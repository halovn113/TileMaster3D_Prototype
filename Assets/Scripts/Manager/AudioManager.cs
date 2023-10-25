using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KHCore
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource musicSource;
        public AudioSource effectPrefab;
        public AudioSource singleEffectSource;

        public float LowPitchRange = .95f;
        public float HighPitchRange = 1.05f;

        //public AudioPrefab audioEffect;
        //public AudioPrefab audioMusic;

        public AudioContainer audioEffect;
        public AudioContainer audioMusic;

        public Dictionary<string, AudioClip> listEffect = new Dictionary<string, AudioClip>();
        public Dictionary<string, AudioClip> listMusic = new Dictionary<string, AudioClip>();

        private AudioSourcePool audioSourcePool = null;

        private void Awake()
        {
            
        }

        public void Start()
        {
            Init();
        }

        public void InitAudioPool()
        {
            audioSourcePool = new AudioSourcePool(effectPrefab);
        }

        private void OnNewLevelLoad(Scene scene, LoadSceneMode mode)
        {
            InitAudioPool();
        }

        protected void Init()
        {
            MasterInstance.AudioManager = this;
            if (musicSource == null)
            {
                gameObject.AddComponent<AudioSource>();
                musicSource = transform.GetComponent<AudioSource>();
            }
            InitData(audioEffect, listEffect);
            InitData(audioMusic, listMusic);
            InitAudioPool();
            SceneManager.sceneLoaded += OnNewLevelLoad;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Old version
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="list"></param>
        private void InitData(AudioPrefab prefab, Dictionary<string, AudioClip> list)
        {
            for (int i = 0; i < prefab.audioInfos.Length; i++)
            {
                var name = prefab.audioInfos[i].audioName == string.Empty ? prefab.audioInfos[i].audio.name : prefab.audioInfos[i].audioName;
                list.Add(name, prefab.audioInfos[i].audio);
            }
        }

        private void InitData(AudioContainer container, Dictionary<string, AudioClip> list)
        {
            for (int i = 0; i < container.audioInfos.Length; i++)
            {
                var name = container.audioInfos[i].audioName == string.Empty ? container.audioInfos[i].audio.name : container.audioInfos[i].audioName;
                list.Add(name, container.audioInfos[i].audio);
            }
        }

        public void PlayEffect(string name, bool loop = false)
        {
            if (listEffect.ContainsKey(name))
            {
                var audio = listEffect[name];
                if (audio != null)
                {
                    audioSourcePool.PlayAtPoint(audio, default, loop);
                }
            }
        }

        public void PlayMusic(string name, bool loop = true)
        {
            if (listMusic.ContainsKey(name))
            {
                var audio = listMusic[name];
                if (audio != null)
                {
                    musicSource.clip = audio;
                    musicSource.loop = loop;
                    musicSource.Play();
                }
            }
        }

        public void StopMusic(float delay = 0)
        {
            StopAllCoroutines();
            StartCoroutine(_stopMusic(delay));
        }

        IEnumerator _stopMusic(float delay)
        {
            yield return new WaitForSeconds(delay);
            musicSource.Stop();
        }


        public void PlayEffectSingle(string name, bool loop = false)
        {
            if (listEffect.ContainsKey(name))
            {
                var audio = listEffect[name];
                if (audio != null)
                {
                    singleEffectSource.clip = audio;
                    singleEffectSource.loop = loop;
                    singleEffectSource.Play();
                }
            }
        }

        public void StopEffectSingle(float delay = 0)
        {
            StopAllCoroutines();
            StartCoroutine(_stopEffectSingle(delay));
        }

        IEnumerator _stopEffectSingle(float delay)
        {
            yield return new WaitForSeconds(delay);
            singleEffectSource.Stop();
        }

        public void SetMusicVolume(float value)
        {
            musicSource.volume = value;
        }


    }

}
