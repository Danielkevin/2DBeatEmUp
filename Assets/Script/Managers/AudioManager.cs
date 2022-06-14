using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<Sound> sounds;

    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.playOnAwake = s.PlayOnAwake;
        }
        Play("BGM");
    }

    public void Play(string name)
    {
        Sound s = sounds.Find(x => x.Name == name);
        if (s != null)
            s.Source.Play();
    }

    public void Stop(string name)
    {
        Sound s = sounds.Find(x => x.Name == name);
        if (s != null)
            s.Source.Stop();
    }

    public bool GetIsPlay(string name)
    {
        //Debug.Log(name);
        Sound s = sounds.Find(x => x.Name == name);
        bool isPlay = s.Source.isPlaying;
        return isPlay;
    }
}

[System.Serializable]
public class Sound
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    [Range(0f,1f)]
    [SerializeField] private float volume;
    [Range(.1f, 3f)]
    [SerializeField] private float pitch;
    private AudioSource source;
    [SerializeField] private bool loop;
    [SerializeField] private bool playOnAwake;

    public AudioClip Clip { get => clip; set => clip = value; }
    public float Volume { get => volume; set => volume = value; }
    public float Pitch { get => pitch; set => pitch = value; }
    public AudioSource Source { get => source; set => source = value; }
    public string Name { get => name; set => name = value; }
    public bool Loop { get => loop; set => loop = value; }
    public bool PlayOnAwake { get => playOnAwake; set => playOnAwake = value; }
}
