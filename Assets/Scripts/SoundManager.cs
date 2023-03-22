using System;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private Sound[] musicSounds;

    [SerializeField]
    private Sound[] vfxSounds;

    [SerializeField]
    private AudioMixer musicAudioMixer;

    [SerializeField]
    private AudioMixer vfxAudioMixer;

    [SerializeField]
    private AudioMixerGroup musicGroup;

    [SerializeField]
    private AudioMixerGroup vfxGroup;

    private const string BACKGROUND_SOUND               = "Background";
    private const string WIN_SOUND                      = "Win";
    private const string LOSE_SOUND                     = "Lose";
    private const string COLLECT_SOUND                  = "Collect";
    private const string SELECT_SOUND                   = "Select";
    private const string TOGGLE_SOUND                   = "Toggle";
    private const string COINS_SOUND                    = "Coins";
    private const string ROCKET_SOUND                   = "Rocket";
    private const string DANGER_SOUND                   = "Danger";
    private const string MUSIC_EXPOSED_PARAMETER        = "MusicVolume";
    private const string VFX_EXPOSED_PARAMETER          = "VFXVolume";
    private const float ROCKET_SOUND_OFFSET             = 1f;
    private const float COIN_SOUND_OFFSET               = 0.5f;
    private const float DANGER_SOUND_OFFSET             = 4f;

    private bool isDangerSoundPlayed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        SetAudioSourceGroups();
        isDangerSoundPlayed = false;

        PlayBackgroundSound();

        Collectable.OnEggCollected += PlayCollectedSound;
        Collectable.OnMeatCollected += PlayCollectedSound;
        Collectable.OnMilkCollected += PlayCollectedSound;
        SettingsManager.OnButtonPressedSoundNeeded += PlayToggleSound;
        CollectableBasket.OnGoodiesSentToEarth += PlayCoinSound;
        LevelManager.OnLevelCompleted += PlayWinSounds;
        LevelMenuUI.OnLevelFailedLoaded += PlayLoseSound;
        CollectableBasket.OnGoodiesSentToEarth += InvokeRocketSound;
        AnimalMovement.OnDangerSoundNeeded += PlayDangerSound;
        AnimalMovement.OnDangerSoundStopNeeded += StopDangerSound;
        LevelMenuUI.OnLoadLevelMapSelected += PlayBackgroundSound;
        LevelMenuUI.OnRepeatLevelSelected += PlayBackgroundSound;
        LevelMenuUI.OnLoadNextLevelSelected += PlayBackgroundSound;
    }

    private void OnDestroy()
    {
        Collectable.OnEggCollected -= PlayCollectedSound;
        Collectable.OnMeatCollected -= PlayCollectedSound;
        Collectable.OnMilkCollected -= PlayCollectedSound;
        SettingsManager.OnButtonPressedSoundNeeded -= PlayToggleSound;
        LevelManager.OnLevelCompleted -= PlayWinSounds;
        LevelMenuUI.OnLevelFailedLoaded -= PlayLoseSound;
        CollectableBasket.OnGoodiesSentToEarth -= InvokeRocketSound;
        AnimalMovement.OnDangerSoundNeeded -= PlayDangerSound;
        AnimalMovement.OnDangerSoundStopNeeded -= StopDangerSound;
        LevelMenuUI.OnLoadLevelMapSelected -= PlayBackgroundSound;
        LevelMenuUI.OnRepeatLevelSelected -= PlayBackgroundSound;
        LevelMenuUI.OnLoadNextLevelSelected -= PlayBackgroundSound;
    }

    private void SetAudioSourceGroups()
    {
        foreach (Sound s in vfxSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = vfxGroup;
            s.source.clip = s.clip;
        }

        foreach (Sound s in musicSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = musicGroup;
            s.source.clip = s.clip;
            s.source.loop = true;
        }
    }

    public void SetMusicLevel(float sliderValue)
    {
        musicAudioMixer.SetFloat(MUSIC_EXPOSED_PARAMETER, Mathf.Log10(sliderValue) * 20);
    }

    public void SetVFXLevel(float sliderValue)
    {
        vfxAudioMixer.SetFloat(VFX_EXPOSED_PARAMETER, Mathf.Log10(sliderValue) * 20);
    }

    private void PlayDangerSound()
    {
        if(!isDangerSoundPlayed)
        {
            isDangerSoundPlayed = true;
            Play(DANGER_SOUND);
            Invoke(nameof(StopDangerSound), 2f);
            Invoke(nameof(SetIsDangerSoundPlayedToFalse), DANGER_SOUND_OFFSET);
        }
    }

    public void PlayBackgroundSound()
    {
        PlayMusic(BACKGROUND_SOUND);
    }

    private void StopDangerSound()
    {
        Stop(DANGER_SOUND);
        SetIsDangerSoundPlayedToFalse();
    }

    private void SetIsDangerSoundPlayedToFalse()
    {
        isDangerSoundPlayed = false;
    }

    private void PlayCoinSound()
    {
        Play(COINS_SOUND);
        Invoke(nameof(CoinSound), COIN_SOUND_OFFSET);
    }

    private void CoinSound()
    {
        Play(COINS_SOUND);
    }

    private void PlayWinSounds()
    {
        StopMusic(BACKGROUND_SOUND);
        Play(WIN_SOUND);
    }

    private void PlayLoseSound()
    {
        StopMusic(BACKGROUND_SOUND);
        Play(LOSE_SOUND);
    }

    public void PlayToggleSound()
    {
        Play(TOGGLE_SOUND);
    }

    private void PlayCollectedSound()
    {
        Play(COLLECT_SOUND);
    }

    public void PlaySelectSound()
    {
        Play(SELECT_SOUND);
    }

    private void InvokeRocketSound()
    {
        Invoke(nameof(PlayRocketSound), ROCKET_SOUND_OFFSET);
    }

    private void PlayRocketSound()
    {
        Play(ROCKET_SOUND);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(vfxSounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(vfxSounds, sound => sound.name == name);
        s.source.Stop();
    }
}
