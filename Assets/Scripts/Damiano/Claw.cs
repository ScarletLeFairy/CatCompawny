using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("TRIGGER ENTER " + other.gameObject.name);
        //other.gameObject
        //Debug.Log("DESTROY " + other.gameObject.name);
        //Destroy(other.gameObject);

        Rigidbody rigid = FindRigidbody(other.transform);
        if (rigid != null)
        {
            rigid.AddForce(Vector3.up * 100, ForceMode.Impulse);
        }
    }

    Rigidbody FindRigidbody(Transform trans)
    {
        if (trans.GetComponent<Rigidbody>())
        {
            return trans.GetComponent<Rigidbody>();
        }

        if(trans.parent != null){
            return FindRigidbody(trans.parent);
        }
        

        return null;
    }
}
