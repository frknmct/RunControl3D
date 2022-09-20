using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAnimator : MonoBehaviour
{
    public Animator anim;
    public void MakeItPassive()
    {
        anim.SetBool("ok",false);
    }
}
