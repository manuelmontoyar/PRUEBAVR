using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int totalRequired = 3;
    private int currentCorrect = 0;

    public GameObject door;
    public GameObject audio;

    public void AddCorrect()
    {
        currentCorrect++;

        if (currentCorrect >= totalRequired)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Debug.Log("Door Opened!");

        

        door.SetActive(false);
        audio.SetActive(true);
        audio.GetComponent<AudioSource>().Play(); // ← Forzar reproducción
    }
}