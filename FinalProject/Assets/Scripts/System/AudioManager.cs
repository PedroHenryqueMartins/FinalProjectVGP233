using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton Instance
    public static AudioManager Instance;

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

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }
}
