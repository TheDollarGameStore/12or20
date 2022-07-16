using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource music;
    private AudioSource normal;
    private AudioSource pitched;

    private bool fadeIn;

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayerPrefs.DeleteAll();
        Invoke("StartFadingIn", 2f);
        AudioSource[] audioSources = GetComponents<AudioSource>();
        music = audioSources[0];
        normal = audioSources[1];
        pitched = audioSources[2];
    }

    void StartFadingIn()
    {
        music.Play();
        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            music.volume = Mathf.Lerp(music.volume, 0.4f, 4f * Time.deltaTime);
        }
    }

    public void PlayRandomized(AudioClip clip)
    {
        pitched.pitch = Random.Range(0.9f, 1.1f);
        pitched.PlayOneShot(clip);
    }

    public void PlayNormal(AudioClip clip)
    {
        normal.PlayOneShot(clip);
    }
}
