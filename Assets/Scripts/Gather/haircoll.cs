using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haircoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)//判定碰沒碰到
    {
        if (other.gameObject.CompareTag("HairPoint"))
        {
            Debug.Log("HairPoint");
        }
       
    }
}
