using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Lasercontroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private GameObject m_object = null;
    public GameObject Point;
    // Start is called before the first frame update
    void Start()
    {
        Point = GameObject.Find("Player/SteamVRObjects/RightHand/PR_Pointer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Point.SetActive(true);
        if (Gather1.m_object == null) return;
        foreach (Transform obj in Gather1.m_object.transform)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }



    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Point.SetActive(false);
        if (Gather1.m_object == null) return;
        foreach (Transform obj in Gather1.m_object.transform)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }

    }
    
}
