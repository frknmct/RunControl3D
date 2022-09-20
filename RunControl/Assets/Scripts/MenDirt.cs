using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenDirt : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(MakeItFalse());
    }

    IEnumerator MakeItFalse()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
    
}
