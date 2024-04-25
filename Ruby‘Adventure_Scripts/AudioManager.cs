using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @Author: wangzixuan31 2916492020@qq.com

/// <summary>
/// 播放音乐音效
/// </summary>


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private AudioSource audioS;//获取组件

    // Start is called before the first frame update
    void Start()
    {
        instance = this;//instance设置为当前AudioManager的实例，这样其他脚本可以通过AudioManager.instance来访问,方便联系其他脚本
        audioS = GetComponent<AudioSource>();
    }


    /// <summary>
    /// 播放指定音效
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);//使用AudioSource的PlayOneShot方法来播放这个音效
    }


   
}
