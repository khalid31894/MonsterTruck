using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip musicClip;
    public AudioSource[] EffectsSource;
    public AudioClip[] audioClips;
    public static SoundManager instance;
    static int coinIndex = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    public void PlayBetween(int i,int j)
    {
        PlayEffect_Instance(Random.Range(i, j));
    }
    public void CatchCoinSound()
    {
        if(coinIndex>7)
        {
            coinIndex = 0;
        }
        PlayEffect_Instance(coinIndex);
        coinIndex++;
    }
    
    public void PlayEffect_Instance(int index)
    {
        EffectsSource[index].PlayOneShot(audioClips[index]);
    }
    public void PlayEffect_Complete(int index)
    {
        if (!EffectsSource[index].isPlaying)
            EffectsSource[index].PlayOneShot(audioClips[index]);
    }
    public void PlayEffect_Loop(int index)
    {
        if (!EffectsSource[index].isPlaying)
        {
            EffectsSource[index].Play();
            EffectsSource[index].loop = true;
        }

    }
    
    public void StopEffect(int index)
    {
        if (EffectsSource[index])
        {
        EffectsSource[index].Stop();

        }
    }
    public void StopAllSounds()
    {
        for (int i = 0; i < EffectsSource.Length; i++)
        {
            if (EffectsSource[i].isPlaying)
                EffectsSource[i].Stop();
        }
    }
    public void PlayMusic()
    {
        MusicSource.clip = musicClip;
        MusicSource.Play();
    }

  

    public void CheckOnStart()
    {

        MusicSource.volume = PlayerPrefs.GetFloat("MusicSlider");
        //print(PlayerPrefs.GetFloat("MusicSlider"));
        if(PlayerPrefs.GetFloat("MusicSlider")>0)
        {
            PlayMusic();
        }
        for (int i = 0; i < EffectsSource.Length; i++)
        {
            EffectsSource[i].volume = PlayerPrefs.GetFloat("SoundSlider");
        }
    }
    public void MuteAll()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        AudioListener.volume = 0;
    }
    public void UnMute()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        AudioListener.volume = 1;
    }
}