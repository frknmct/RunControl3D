using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EmptyCharacter : MonoBehaviour
{
    public SkinnedMeshRenderer _renderer;
    public Material newMaterial;
    public NavMeshAgent _navMesh;
    public Animator anim;
    public GameObject Target;
    public GameManager _gameManager;
    private bool collisionExist;
    
    private void LateUpdate()
    {
        if (collisionExist)
        {
            _navMesh.SetDestination(Target.transform.position);
        }
        
    }
    
    Vector3 GivePosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LowerCharacters") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("EmptyCharacter"))
            {
                TakeTheChar();
                collisionExist = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else if (other.CompareTag("PinBox"))
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
    }

    void TakeTheChar()
    {
        Material[] mats = _renderer.materials;
        mats[0] = newMaterial;
        _renderer.materials = mats;
        anim.SetBool("Attack",true);
        gameObject.tag = "LowerCharacters";
        GameManager.momentaryCharacterCount++;
        Debug.Log(GameManager.momentaryCharacterCount);
    }
    
}
