using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneColl : MonoBehaviour
{
    public Transform erasercopy, paintcopy, clearcopy;
    // Start is called before the first frame update
    void Start()
    {
        erasercopy = GameObject.Find("Salon/Trolley/erasercopy").transform;
        clearcopy = GameObject.Find("Salon/Trolley/clearcopy").transform;
        paintcopy = GameObject.Find("Salon/Trolley/paintcopy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)//判定碰沒碰到
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log("FFF");
        if (other.gameObject.CompareTag("Paint"))
        {
            other.gameObject.transform.rotation = paintcopy.rotation;
            other.gameObject.transform.position = paintcopy.position;
        }
        else if (other.gameObject.CompareTag("Eraser"))
        {
            other.gameObject.transform.rotation = erasercopy.rotation;
            other.gameObject.transform.position = erasercopy.position;
        }
        else if (other.gameObject.CompareTag("Clear"))
        {
            other.gameObject.transform.rotation = clearcopy.rotation;
            other.gameObject.transform.position = clearcopy.position;
        }
    }
    
}
