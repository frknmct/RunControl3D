using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public GameManager gameManager;
    public Camera _camera;
    public bool levelFinished;
    public GameObject lastPosition;
    public Slider _slider;
    public GameObject passPoint;

    private void Start()
    {
        float distance = Vector3.Distance(transform.position, passPoint.transform.position);
        _slider.maxValue = distance;
    }

    private void FixedUpdate()
    {
        if (!levelFinished)
        {
            transform.Translate(Vector3.forward * .5f * Time.deltaTime); 
        }
        
    }
    

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (levelFinished)
            {
                transform.position = Vector3.Lerp(transform.position, lastPosition.transform.position, .015f);
                if (_slider.value != 0)
                {
                    _slider.value -= .005f;
                }
            }
            else
            {
                float distance = Vector3.Distance(transform.position, passPoint.transform.position);
                _slider.value = distance;

                if (Time.timeScale != 0)
                {
                

                    if (Input.GetAxis("Mouse X")<0)
                    {
                        transform.position = Vector3.Lerp(transform.position,
                            new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                    }
                    if (Input.GetAxis("Mouse X")>0)
                    {
                        transform.position = Vector3.Lerp(transform.position,
                            new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                    }
                
                }
            
            
            }  
        }

        
        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Times") || other.CompareTag("Plus") || other.CompareTag("Minus") || other.CompareTag("Divide"))
        {
            int number = int.Parse(other.name);
            gameManager.MenManagment(other.tag,number,other.transform);
        }
        else if (other.CompareTag("LastTrigger"))
        {
            _camera.levelFinished = true;
            gameManager.TrigEnemies();
            levelFinished = true;
        }
        else if (other.CompareTag("EmptyCharacter"))
        {
            gameManager.Characters.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Pole") || collisionInfo.gameObject.CompareTag("PinBox") || collisionInfo.gameObject.CompareTag("FanPin"))
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + .2f,transform.position.y,transform.position.z);
            }
            
        }
    }
}
