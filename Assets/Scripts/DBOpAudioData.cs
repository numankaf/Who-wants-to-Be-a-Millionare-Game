using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DBOpAudioData : ScriptableObject
{
    public AudioClip[] SuccessClips;
    public AudioClip[] pickedClips;
    public AudioClip[] wrongClips;

    public AudioClip retryButton;
    public AudioClip mainMenuButton;
}
