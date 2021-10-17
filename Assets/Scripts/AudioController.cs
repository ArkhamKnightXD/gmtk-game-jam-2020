using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource GameSong;

    public AudioSource DamageTaken;

    public AudioSource ItemGet;

    public AudioSource OutOfControl;

    public AudioSource PlayerJump;

    public AudioSource Slide;

    public AudioSource GameOver;

    public AudioSource Win;

    public AudioSource Menu;

    public AudioSource LevelSelect;


    private void Awake()
    {
        Instance = this;
    }

    
    public enum SoundEffect
    {
        GameSong,
        DamageTaken,
        ItemGet,
        OutOfControl,
        PlayerJump,
        Slide,
        Win,
        Menu,
        LevelSelect,
        GameOver
    }
    

    public void PlaySoundEffect(SoundEffect type)
    {
        switch (type)
        {
        case SoundEffect.GameSong:
            GameSong.Play();
            break;

        case SoundEffect.DamageTaken:
            DamageTaken.Play();
            break;

        case SoundEffect.ItemGet:
            ItemGet.Play();
            break;

        case SoundEffect.OutOfControl:
            OutOfControl.Play();
            GameSong.Stop();
            break;

        case SoundEffect.PlayerJump:
            PlayerJump.Play();
            break;

        case SoundEffect.Slide:
            Slide.Play();
            break;

        case SoundEffect.Win:
            GameSong.Stop();
            Win.Play();
            break;

        case SoundEffect.GameOver:
            GameSong.Stop();
            GameOver.Play();
            break;

        case SoundEffect.Menu:
            Menu.Play();
            break;
        
        case SoundEffect.LevelSelect:
            LevelSelect.Play();
            break;

        default:
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
