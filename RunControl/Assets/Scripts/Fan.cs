using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public Animator anim;
    public float waitTime;
    public BoxCollider _wind;

    public void AnimationSituation(string situation)
    {
        if (situation == "true")
        {
            anim.SetBool("Start",true);
            _wind.enabled = true;
        }
        else
        {
            anim.SetBool("Start",false);
            StartCoroutine(AnimationTrig());
            _wind.enabled = false;
        }

        
    }

    IEnumerator AnimationTrig()
    {

        yield return new WaitForSeconds(waitTime);
        AnimationSituation("true");
    }
}
