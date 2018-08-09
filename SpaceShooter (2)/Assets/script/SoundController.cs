using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundEffect
{
    exp_astroid,exp_enemy,exp_player,wea_enemy,wea_player
}
public class SoundController : MonoBehaviour {
    [SerializeField]
    private AudioSource BGM, Effect;
    [SerializeField]
    private AudioClip[] EffectClip;
    [SerializeField]
    private AudioClip BGMClip;
	// Use this for initialization
	void Start () {
        BGM.clip = BGMClip;
        BGM.Play();
	}
    public void PlayEffectSound(eSoundEffect index)
    {
        Effect.PlayOneShot(EffectClip[(int)index]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
