using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserCollider : MonoBehaviour
{
    public static GameObject Contact;
    public static int ForContact=0;
    // Start is called before the first frame update
 
    void OnTriggerEnter(Collider other) //Eraser
    {
        
        Contact = GameObject.Find(other.gameObject.name);
        Debug.Log("collider (name) : " + Contact.name);
        if (Contact.tag == "Hairs") ForContact = 1;
    }

}
