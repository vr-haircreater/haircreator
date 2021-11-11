using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserCollider : MonoBehaviour
{
    public static GameObject Contact;
    public static int ForContact=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void OnTriggerEnter(Collider other) //Eraser
    {
        Contact = GameObject.Find(other.gameObject.name);
        if (Contact.tag == "Hairs") ForContact = 1;
    }*/
    void OnCollisionEnter(Collision other) //Eraser
    {
        Debug.Log("GET");
        Contact = GameObject.Find(other.gameObject.name);
        if (Contact.tag == "Hairs") ForContact = 1;
    }
}
