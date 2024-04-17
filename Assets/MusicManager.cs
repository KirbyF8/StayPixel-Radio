using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private AudioClip[] songs;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;

    private int cancionActual = 0;

    [SerializeField] private GameObject looping;
    [SerializeField] private TMPro.TMP_Text listeningTo;

    [SerializeField] private Image image;
    [SerializeField] private Sprite[] images;

    [SerializeField] private GameObject Maracas;

    private int rotationZ;

    void Start()
    {
        looping.SetActive(false);
        LoadAllSongs();

        YouAreListeningTo();


        audioSource.clip = songs[cancionActual];
        slider.maxValue = audioSource.clip.length;


        audioSource.loop = false;

        audioSource.SetScheduledEndTime(AudioSettings.dspTime + audioSource.clip.length);
        audioSource.PlayScheduled(AudioSettings.dspTime);
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + audioSource.clip.length);

        LoadAllSongs();
      
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            slider.value = audioSource.time;

            rotationZ = Random.Range(-25, 26);
            Maracas.transform.Rotate(new Vector3(0, 0, rotationZ), Space.Self);
        }

        
    }

   
    private void LoadAllSongs()
    {
        songs = Resources.LoadAll<AudioClip>("Songs");
        // images = Resources.LoadAll<Sprite>("Sprites");
    }


public void Shuffle()
    {
        cancionActual = Random.Range(0, songs.Length);
        audioSource.clip = songs[cancionActual];
        audioSource.Play();
        YouAreListeningTo();
    }

    public void Resume()
    {
        audioSource.Play();
    }

    public void ResetSong()
    {
        audioSource.Stop();
        audioSource.time = 0f;
        audioSource.Play();
    }

    public void Next()
    {
        cancionActual++;
        audioSource.clip = songs[cancionActual];
        YouAreListeningTo();
        Resume();
    }

    public void Previous()
    {
        cancionActual--;

        if (cancionActual <= 0)
        {
            cancionActual = 0;
        }

        audioSource.clip = songs[cancionActual];
        YouAreListeningTo();
        Resume();
    }
    public void Loop()
    {
        if (audioSource.loop == false)
        {
            audioSource.loop = true;
            looping.SetActive(true);
        }
        else
        {
            audioSource.loop = false;
            looping.SetActive(false);
        }
      
    }

    public void YouAreListeningTo()
    {


        listeningTo.text = songs[cancionActual].name;
       // image.sprite = images[cancionActual];
    }

    
}
