using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LowerCharacter : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent NavMesh;
    public GameManager _gameManager;
    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
    }


    private void LateUpdate()
    {
        NavMesh.SetDestination(Target.transform.position);
    }

    Vector3 GivePosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox"))
        {
            _gameManager.DestroyEffectShow(GivePosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Saw"))
        {
            _gameManager.DestroyEffectShow(GivePosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("FanPin"))
        {
            _gameManager.DestroyEffectShow(GivePosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Sledge"))
        {
            _gameManager.DestroyEffectShow(GivePosition(),true);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            _gameManager.DestroyEffectShow(GivePosition(),false,false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("EmptyCharacter"))
        {
            _gameManager.Characters.Add(other.gameObject);
        }
    }
}
