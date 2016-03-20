using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
    
    public static AudioController Instance;

    public List<AudioBundle> SFXLibrary;
    public List<AudioBundle> IntroLibrary;
    public List<AudioBundle> AnnouncerLibrary;
    private AudioClip currentSong;
    private AudioSource ownAudioSource;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        ownAudioSource = GetComponent<AudioSource>();
        if (ownAudioSource.clip == null)
        {
            ChangeBackgroundMusic("Main Menu");
        }
    }

    public void CallSound(string targetTrack)
    {
        for(int i = 0; i < SFXLibrary.Count;i++)
        {
            if (SFXLibrary[i].nameOfTrack == targetTrack)
            {
                AudioSource.PlayClipAtPoint(SFXLibrary[i].track,Vector3.zero);
            }
        }
    }
    public void CallAnnouncerSound(string targetTrack)
    {
        for (int i = 0; i < SFXLibrary.Count; i++)
        {
            if (SFXLibrary[i].nameOfTrack == targetTrack)
            {
                AudioSource.PlayClipAtPoint(SFXLibrary[i].track, Vector3.zero);
            }
        }
    }
    public void ChangeBackgroundMusic(string targetTrack)
    {
        for (int i = 0; i < IntroLibrary.Count; i++)
        {
            if (IntroLibrary[i].nameOfTrack == targetTrack)
            {
                ownAudioSource.clip = IntroLibrary[i].track;
                ownAudioSource.Play();
            }
        }
    }
}

[System.Serializable]
public class AudioBundle
{
    public string nameOfTrack;
    public AudioClip track;

    public AudioBundle(string NameOfTrack, AudioClip Track)
    {
        this.nameOfTrack    = NameOfTrack;
        this.track          = Track;
    }
}

