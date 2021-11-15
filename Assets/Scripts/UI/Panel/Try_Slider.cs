using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Try_Slider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public RectTransform Slider_Handle;
    public GameObject dot;
    // Start is called before the first frame update
    void Start()
    {
        Slider_Handle = GameObject.Find("Player/SteamVRObjects/LeftHand/PadA/PanelA/Middle/Slider/Handle Slide Area/Handle").GetComponent<RectTransform>();
        dot = GameObject.Find("Player/SteamVRObjects/RightHand/PR_Pointer/Dot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("They started dragging " + this.name);
        //Debug.Log("IN");
        //if (gameObject.tag == "Handle1")
        //{
        //    Debug.Log("IN");
        //    gameObject.transform.position = dot.transform.position;
        //}

    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("IN");
        gameObject.transform.position = dot.transform.position;
        
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    
}
