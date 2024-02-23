using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    private float bgmFadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_HIGH;

    //Background music name, sound effect name
    private string nextBGMName;
    private string nextSEName;

    //Is the BGM fading out?
    private bool isFadeOut = false;

    //Audio sources for BGM and SE
    public AudioSource AttachBGMSource;
    public AudioSource AttachSESource;

    //Keep all audio
    private Dictionary<string, AudioClip> bgmDic, seDic;

    protected override void Awake()
    {
        base.Awake();

        //Load all SE & BGM files from resource folder
        bgmDic = new Dictionary<string, AudioClip>();
        seDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            bgmDic[bgm.name] = bgm;
        }

        foreach (AudioClip se in seList)
        {
            seDic[se.name] = se;
        }
    }

    private void Start()
    {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);
        AttachSESource.volume = PlayerPrefs.GetFloat(CONST.SE_VOLUME_KEY, CONST.SE_VOLUME_DEFAULT);
    }

    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!seDic.ContainsKey(seName))
        {
            Debug.LogError(seName + " There is no SE named");
            return;
        }

        nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }

    private void DelayPlaySE()
    {
        AttachSESource.PlayOneShot(seDic[nextSEName] as AudioClip);
    }

    public void PlayBGM(string bgmName, float fadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_HIGH)
    {
        if (!bgmDic.ContainsKey(bgmName))
        {
            Debug.LogError(bgmName + " There is no BGM named");
            return;
        }

        //BGM is not currently playing
        if (!AttachBGMSource.isPlaying)
        {
            nextBGMName = "";
            AttachBGMSource.clip = bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //BGM is playing
        else if(AttachBGMSource.clip.name != bgmName)
        {
            nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }

    public void FadeOutBGM(float fadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_LOW)
    {
        bgmFadeSpeedRate = fadeSpeedRate;
        isFadeOut = true;
    }

    private void Update()
    {
        if(!isFadeOut)
        {
            return;
        }

        //Gradually lower the volume, when the volume reaches 0 play the next BGM
        AttachBGMSource.volume -= Time.deltaTime * bgmFadeSpeedRate;
        if(AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);
            isFadeOut = false;
            if (!string.IsNullOrEmpty(nextBGMName))
            {
                PlayBGM(nextBGMName);
            }
        }
    }

    public void ChangeBGMVolume(float BGMVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        PlayerPrefs.SetFloat(CONST.BGM_VOLUME_KEY, BGMVolume);
    }

    public void ChangeSEVolume(float SEVolume)
    {
        AttachSESource.volume = SEVolume;
        PlayerPrefs.SetFloat(CONST.SE_VOLUME_KEY, SEVolume);
    }
}
