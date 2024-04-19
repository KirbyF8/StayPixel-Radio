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
   private Sprite[] sprites;

    [SerializeField] private GameObject Maracas;
    [SerializeField] private GameObject random;

    private int rotationZ;

    private bool shuffled;
    private int songBeforeShuffle;
    void Start()
    {
        looping.SetActive(false);
        LoadAllSongs();
        random.SetActive(false);
        YouAreListeningTo();

        shuffled = false;
        audioSource.clip = songs[cancionActual];
        slider.maxValue = audioSource.clip.length;


        audioSource.loop = false;

      

        LoadAllSongs();
      
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            slider.value = audioSource.time;

            if (slider.value >= slider.maxValue )
            {
                Next();
            }

            rotationZ = Random.Range(-5, 6);
            Maracas.transform.Rotate(new Vector3(0, 0, rotationZ), Space.Self);
        }

        
    }

   
    private void LoadAllSongs()
    {
        songs = Resources.LoadAll<AudioClip>("Songs");
        sprites = Resources.LoadAll<Sprite>("Images");

        
    }


public void Shuffle(bool isFromButton)
    {
        if (isFromButton) { shuffled = !shuffled; }
       
        songBeforeShuffle = cancionActual;

        if (!shuffled) 
        { 
            random.SetActive(false);
            shuffled = false; 
        }
        else 
        {
            random.SetActive(true);
            shuffled = true;
            cancionActual = Random.Range(0, songs.Length);
            while (cancionActual == songBeforeShuffle) { cancionActual = Random.Range(0, songs.Length); }
            audioSource.clip = songs[cancionActual];
            audioSource.Play();
            YouAreListeningTo();

        }

        
       
       
    }

    public void Resume()
    {
        audioSource.Play();
        slider.maxValue = audioSource.clip.length;
    }

    public void ResetSong()
    {
        audioSource.Stop();
        audioSource.time = 0f;
        audioSource.Play();
    }

    public void Next()
    {
        if (shuffled) { Shuffle(false); }
        
        cancionActual++;
        if (cancionActual > songs.Length)
        {
            cancionActual = 0;
        }
        audioSource.clip = songs[cancionActual];
        YouAreListeningTo();
        Resume();
    }

    public void Previous()
    {
        cancionActual--;
        if (shuffled)
        {
            cancionActual = songBeforeShuffle;
            audioSource.clip = songs[cancionActual];
            songBeforeShuffle = Random.Range(0, songs.Length);
            while (cancionActual == songBeforeShuffle) { cancionActual = Random.Range(0, songs.Length); }
        }
        else
        {
            if (cancionActual <= 0)
            {
                cancionActual = songs.Length - 1;
            }
            audioSource.clip = songs[cancionActual];
        }

        

        
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

            image.sprite = sprites[cancionActual];
        
        
    }

   


}
