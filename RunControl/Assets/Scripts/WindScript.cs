using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LowerCharacters"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-10,0,0),ForceMode.Impulse);
        }
    }
}
