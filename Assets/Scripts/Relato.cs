using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relato : MonoBehaviour
{
    public List<string> RelatoList = new List<string>();
    public List<AudioClip> Clips = new List<AudioClip>();
    public List<Sprite> Images = new List<Sprite>();
    public AudioSource audioSource ;
    public Text texto;
    public Image image;
    private int index = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Play();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Play()
    {
        audioSource.clip = Clips[index];
        texto.text = RelatoList[index];
        image.sprite = Images[index];
        audioSource.Play();
        StartCoroutine(WaitForAudioToEnd());
    }

    private System.Collections.IEnumerator WaitForAudioToEnd()
    {
        // Espera mientras el audio está reproduciéndose
        while (audioSource.isPlaying)
        {
            yield return null; // Espera hasta el siguiente frame
        }

        // Acción a realizar cuando el audio termine
        Debug.Log("El audio ha terminado.");
        OnAudioEnd();
    }

    private void OnAudioEnd()
    {
        // Aquí puedes colocar el código que se ejecutará al finalizar el audio
        index++;
        Play();
    }
}
