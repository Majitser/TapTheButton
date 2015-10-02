using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioGround;
    public AudioSource audioSky;
    public AudioClip sound;

	void Start ()
    {
        audioGround.panStereo = 1;
        //audioGround.PlayOneShot(sound, 1);
        audioSky.panStereo = -1;
        //audioSky.PlayOneShot(sound, 1);*/
    }

	void Update ()
    {
	}
}
