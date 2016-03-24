using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;

    public List<AudioBundle> maleSFXLibrary;
    public List<AudioBundle> femaleSFXLibrary;
    public List<AudioBundle> SFXLibrary;
    public List<AudioBundle> IntroLibrary;
    public List<AudioBundle> AnnouncerLibrary;
    private AudioClip currentSong;
    private AudioSource ownAudioSource;

    public float SFXVolume;
    public float OSTVolume;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        ownAudioSource = GetComponent<AudioSource>();
        if (ownAudioSource.clip == null)
        {
            // ChangeBackgroundMusic("Main Menu");
        }
    }

    void Update()
    {
        if (MainMenu.Instance != null) {
            SFXVolume               = float.Parse(MainMenu.Instance.SfxVolumeString) / 10;
            ownAudioSource.volume   = float.Parse(MainMenu.Instance.musicVolumeString) / 10;
        }
    }

    public void CallMaleSound(string targetTrack)
    {
        for (int i = 0; i < maleSFXLibrary.Count; i++)
        {
            if (maleSFXLibrary[i].nameOfTrack == targetTrack)
            {
                AudioSource.PlayClipAtPoint(maleSFXLibrary[i].track, Vector3.zero, SFXVolume);
            }
        }
    }

    public void CallFemaleSound(string targetTrack)
    {
        for (int i = 0; i < femaleSFXLibrary.Count; i++)
        {
            if (femaleSFXLibrary[i].nameOfTrack == targetTrack)
            {
                AudioSource.PlayClipAtPoint(femaleSFXLibrary[i].track, Vector3.zero, SFXVolume);
            }
        }
    }

    public void CallAnnouncerSound(string targetTrack)
    {
        for (int i = 0; i < AnnouncerLibrary.Count; i++)
        {
            if (AnnouncerLibrary[i].nameOfTrack == targetTrack)
            {
                AudioSource.PlayClipAtPoint(AnnouncerLibrary[i].track, Vector3.zero, SFXVolume);
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

    public void CallSFXSound(string targetTrack)
    {
        for (int i = 0; i < SFXLibrary.Count; i++)
        {
            if (SFXLibrary[i].nameOfTrack == targetTrack)
            {
                ownAudioSource.clip = SFXLibrary[i].track;
                ownAudioSource.Play();
            }
        }
    }

    public void PlaySound(Gender targetGender, SoundEffect targetEffect)
    {
        if (targetGender == Gender.FEMALE)
        {
            switch (targetEffect)
            {
                case SoundEffect.ATTACK:
                    CallFemaleSound("Attack " + Random.Range(0, 8));
                    break;
                case SoundEffect.BLOCK:
                    CallFemaleSound("Block " + Random.Range(0, 8));

                    break;
                case SoundEffect.DEATH:
                    CallFemaleSound("Death " + Random.Range(0, 6));

                    break;
                case SoundEffect.HIT:
                    CallFemaleSound("Hit " + Random.Range(0, 7));

                    break;
            }
        }
        else if (targetGender == Gender.MALE)
        {
            switch (targetEffect)
            {
                case SoundEffect.ATTACK:
                    CallMaleSound("Attack " + Random.Range(0, 9));

                    break;
                case SoundEffect.BLOCK:
                    CallMaleSound("Block " + Random.Range(0, 9));

                    break;
                case SoundEffect.DEATH:
                    CallMaleSound("Death " + Random.Range(0, 6));

                    break;
                case SoundEffect.HIT:
                    CallMaleSound("Hit " + Random.Range(0, 8));

                    break;
            }
        }
    }

    public void PlaySound(AnnouncerSounds targetSound)
    {
        switch (targetSound)
        {
            case AnnouncerSounds.CENA:
                CallAnnouncerSound("Cena");
                break;
            case AnnouncerSounds.JOHN:
                CallAnnouncerSound("John");
                break;
            case AnnouncerSounds.CHOOSEYOURCHARACTER:
                CallAnnouncerSound("Choose Your Character");
                break;
            case AnnouncerSounds.FIGHT:
                CallAnnouncerSound("Fight");
                break;
            case AnnouncerSounds.LOSE:
                CallAnnouncerSound("Lose");
                break;
            case AnnouncerSounds.ROUND1:
                CallAnnouncerSound("Round 1");
                break;
            case AnnouncerSounds.ROUND2:
                CallAnnouncerSound("Round 2");
                break;
            case AnnouncerSounds.ROUND3:
                CallAnnouncerSound("Round 3");
                break;
            case AnnouncerSounds.TIMEUP:
                CallAnnouncerSound("Time up");
                break;
            case AnnouncerSounds.WIN:
                CallAnnouncerSound("Win");
                break;
            case AnnouncerSounds.WINNER:
                CallAnnouncerSound("Winner");
                break;
            case AnnouncerSounds.TEN:
                CallAnnouncerSound("10");
                break;
            case AnnouncerSounds.NINE:
                CallAnnouncerSound("9");
                break;
            case AnnouncerSounds.EIGHT:
                CallAnnouncerSound("8");
                break;
            case AnnouncerSounds.SEVEN:
                CallAnnouncerSound("7");
                break;
            case AnnouncerSounds.SIX:
                CallAnnouncerSound("6");
                break;
            case AnnouncerSounds.FIVE:
                CallAnnouncerSound("5");
                break;
            case AnnouncerSounds.FOUR:
                CallAnnouncerSound("4");
                break;
            case AnnouncerSounds.THREE:
                CallAnnouncerSound("3");
                break;
            case AnnouncerSounds.TWO:
                CallAnnouncerSound("2");
                break;
            case AnnouncerSounds.ONE:
                CallAnnouncerSound("1");
                break;
        }
    }

    public void PlaySound(SFXSounds targetSound)
    {
        switch (targetSound)
        {
            case SFXSounds.BLOODSPLATTER:
                CallSFXSound("Blood Splatter " + Random.Range(0, 3));
                break;
            case SFXSounds.BUTTON:
                CallSFXSound("Button");
                break;
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

public enum SoundType
{
    ANNOUNCEMENT,
    SFX,
    CHARACTERSFX
}

public enum SoundEffect
{
    BLOCK,
    DEATH,
    HIT,
    ATTACK
}

public enum AnnouncerSounds
{
    CENA,
    JOHN,
    CHOOSEYOURCHARACTER,
    FIGHT,
    LOSE,
    ROUND1,
    ROUND2,
    ROUND3,
    TIMEUP,
    WIN,
    WINNER,
    TEN,
    NINE,
    EIGHT,
    SEVEN,
    SIX,
    FIVE,
    FOUR,
    THREE,
    TWO,
    ONE
}

public enum SFXSounds
{
    BLOODSPLATTER,
    BUTTON
}