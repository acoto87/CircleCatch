using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sources;
    public AudioClip[] clips;

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);

        sources = GetComponents<AudioSource>();
        sources[0].loop = true;
    }

    void Start()
    {
        Play(0, 0);
    }

    public void Play(int sourceIndex, int clipIndex)
    {
        if (sources[sourceIndex].isPlaying)
        {
            sources[sourceIndex].Stop();
        }

        sources[sourceIndex].clip = clips[clipIndex];
        sources[sourceIndex].Play();
    }
}
