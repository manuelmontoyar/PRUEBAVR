using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTIVASONIDO : MonoBehaviour
{
    public AudioSource audioSource;
    private bool played = false; // ← Evita que se repita

    void OnTriggerEnter(Collider other)
    {
        if (!played && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            audioSource.Play();
            played = true;
        }
    }
}
// ASFASF