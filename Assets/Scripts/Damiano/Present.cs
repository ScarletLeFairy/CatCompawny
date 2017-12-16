using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), SelectionBase]
public class Present : Destructible {

    public AudioClip clip;
    AudioSource audioSource;

    // Use this for initialization
    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }
	
	// Update is called once per frame
	void Update () {
        //SufferDamage(1234);
	}

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2) {
            SufferDamage(Mathf.RoundToInt(collision.relativeVelocity.magnitude) * points);
            audioSource.Play();
        }
    }
}
