using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;


public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;


    public void PlaySFXFootsteps()
    {
        _mixer.SetFloat("Footsteps", -7);
    }
}
