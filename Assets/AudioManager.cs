using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        public bool loop = false;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioSource> soundSources = new Dictionary<string, AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = s.clip;
                source.volume = s.volume;
                source.loop = s.loop;
                soundSources.Add(s.name, source);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(string name)
    {
        if (soundSources.ContainsKey(name))
            soundSources[name].Play();
        else
            Debug.LogWarning("Brak dŸwiêku o nazwie: " + name);
    }

    public void Stop(string name)
    {
        if (soundSources.ContainsKey(name))
            soundSources[name].Stop();
    }
}
