using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource)), SelectionBase]
public class Destructible : MonoBehaviour {
    

    public int health = 100;
    public int points = 5;
    public int claw_damage = 10;
    public float stability = 2f;

    public bool destructable = false;

    public GameObject UI_Damage;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (destructable && health <= 0){
            Destroy(gameObject);
        }
	}

    public void SufferDamage()
    {
        SufferDamage(claw_damage);
    }

    protected void SufferDamage(int damage)
    {
        GameObject dmg = Instantiate(UI_Damage, transform.position, Quaternion.identity);
        dmg.GetComponent<UI_Damage>().damage = damage;
        health -= damage;
    }
    
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > stability)
        {
            SufferDamage(Mathf.RoundToInt(collision.relativeVelocity.magnitude) * points);
            audioSource.Play();
        }
    }
}
