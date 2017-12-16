using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject UI_Damage;

    public int health = 100;
    public int points = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void SufferDamage(int damage)
    {
        GameObject dmg = Instantiate(UI_Damage, transform.position, Quaternion.identity);
        dmg.GetComponent<UI_Damage>().damage = damage;
    }
}
