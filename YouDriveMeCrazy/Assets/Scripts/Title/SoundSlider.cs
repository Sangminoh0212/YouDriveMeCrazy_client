using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundSlider : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;
    // Start is called before the first frame update
    public void AudioControl(){
        float sound = audioSlider.value;
        //슬라이더 미니멈 값을 -40으로 설정해놔서(그 이하로 내려가면 깨진대)슬라이더로 -40가면 완전 뮤트로 만들라고
        if(sound == -40f) masterMixer.SetFloat("BGM",-80);
        else masterMixer.SetFloat("BGM", sound);
    }   
    public void ToggleAudioVolume(){
        AudioListener.volume = AudioListener.volume == 0? 1:0;
    }
}
