using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource Asource;

    public AudioClip[] Musics;

    public int currentIndex;

    private float nextDateForChange;

    void Start()
    {
        nextDateForChange = 0;
        currentIndex = Random.Range(0, Musics.Length);
    }

    void Update()
    {
        if (nextDateForChange <= Time.time)
        {
            int rdm = currentIndex;
            while (rdm == currentIndex)
            {
                rdm = Random.Range(0, Musics.Length);
            }
            currentIndex = rdm;

            nextDateForChange = Time.time + Musics[currentIndex].length + 2;

            Asource.PlayOneShot(Musics[currentIndex]);
        }
    }
}
