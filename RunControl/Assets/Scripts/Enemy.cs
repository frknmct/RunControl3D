using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject targetToAttack;
    public NavMeshAgent _navMesh;
    private bool attackStarted;
    
    public void AnimationTrig()
    {
        GetComponent<Animator>().SetBool("Attack",true);
        attackStarted = true;
    }

    void LateUpdate()
    {
        if (attackStarted)
        {
            _navMesh.SetDestination(targetToAttack.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LowerCharacters"))
        {
            Vector3 newPos = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectShow(newPos,false,true);
            gameObject.SetActive(false);
        }
    }
}
