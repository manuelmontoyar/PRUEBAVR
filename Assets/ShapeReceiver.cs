using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeReceiver : MonoBehaviour
{
    public string correctTag;
    public PuzzleManager manager;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.CompareTag(correctTag))
        {
            activated = true;
            manager.AddCorrect();
            Debug.Log("Correct object!");
        }
    }
}