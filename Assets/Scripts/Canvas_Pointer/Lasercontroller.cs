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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer");

        Point.SetActive(true);
        if (GatherControl.m_object == null) return;
        foreach (Transform obj in GatherControl.m_object.transform)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        GatherControl.icon = 4;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Point.SetActive(false);
        if (GatherControl.m_object == null) return;
        foreach (Transform obj in GatherControl.m_object.transform)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
        GatherControl.icon = GatherControl.state;

    }
    
}
