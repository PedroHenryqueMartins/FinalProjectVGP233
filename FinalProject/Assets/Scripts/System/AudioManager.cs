using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton Instance
    public static AudioManager Instance = null;

    public AudioSource audioSource;

    [SerializeField]
    public List<AudioClip> audioClips;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Play sound depending on Index
    public void PlaySound(int index)
    {
        // Check that clip does exist
        if(audioClips.Count >= index)
        {
            audioSource.PlayOneShot(audioClips[index]);
        }
    }

    // Play Sound for clicking buttons or UI elements
    public void PlayClickSound(int index)
    {
        // Check that clip does exist
        if(audioClips.Count >= 2)
        {
            audioSource.PlayOneShot(audioClips[1]);
        }
    }
}
