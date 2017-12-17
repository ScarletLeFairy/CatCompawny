using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour {

    

    void OnTriggerEnter(Collider other)
    {
        Vector3 closest = other.ClosestPoint(transform.position);
        Vector3 dir = ((closest - transform.position ).normalized  + Vector3.up * 0.1f).normalized;

        Debug.DrawLine(transform.position, transform.position + dir, Color.magenta, 4);

        Rigidbody rigid = FindRigidbody(other.transform);
        if (rigid != null)
        {
            rigid.AddForce(dir * 50, ForceMode.Impulse);
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
