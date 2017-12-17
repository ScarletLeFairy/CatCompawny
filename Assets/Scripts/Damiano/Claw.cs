using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour {

    

    void OnTriggerEnter(Collider other)
    {
        //Vector3 closest = other.ClosestPoint(transform.position);
        Vector3 dir = (other.transform.position - transform.position).normalized; //((closest - transform.position ).normalized  + Vector3.up * 0.1f).normalized;

        Debug.DrawLine(transform.position, transform.position + dir, Color.magenta, 4);

        Rigidbody rigid = FindRigidbody(other.transform);
        if (rigid != null)
        {
            rigid.AddForce(dir * 3, ForceMode.Impulse);
            rigid.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);

           
        }

        Destructible dest = rigid != null ? rigid.gameObject.GetComponent<Destructible>() : FindDestructible(other.transform);
        if (dest != null)
        {
            dest.SufferDamage();
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

    Destructible FindDestructible(Transform trans)
    {
        if (trans.GetComponent<Destructible>())
        {
            return trans.GetComponent<Destructible>();
        }

        if (trans.parent != null)
        {
            return FindDestructible(trans.parent);
        }


        return null;
    }
}
