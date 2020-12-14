using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton Instance
    public static AudioManager Instance = null;

    public AudioSource audioSource;

    // Button that will be pressed
    public List<Button> buttons;
    public AudioClip levelTheme;

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

        // Make sure to add a listener
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    // Play sound depending on Index
    public void PlaySound(int index)
    {
        // Check that clip does exist
        if (audioClips.Count >= index && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClips[index]);
        }
    }

    // Play Sound for clicking buttons or UI elements
    public void PlayClickSound()
    {
        PlaySound(0);
    }

    public void SetLevelAudio()
    {
        gameObject.GetComponent<AudioSource>().clip = levelTheme;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
