using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPhysicsFix : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        // Escucha cuando se suelta el objeto
        grab.selectExited.AddListener(OnSoltar);
    }

    void OnSoltar(SelectExitEventArgs args)
    {
        rb.velocity = Vector3.zero;        // ← en vez de linearVelocity
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
    }
}