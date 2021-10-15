using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Gather1 : MonoBehaviour
{
    public static int icon;
    public int state;
    public static bool GridState;
    GameObject RightHand;
    public static bool RightDown = false;

    
    public static SteamVR_Behaviour_Pose Pose = null;
    public FixedJoint m_Joint = null;
    public static GameObject m_object = null;

    public SteamVR_Action_Boolean TriggerClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");//板機鍵按鈕
    public SteamVR_Action_Boolean m_Grip = null;
    

    public Transform ClearPos,EraserPos,PaintPos,erasercopy,paintcopy,clearcopy;


    Vector3 ObjectPos;
    Quaternion objRotatePos,TrolleyRotate;

    void Awake()
    {
        RightHand = GameObject.Find("Player/SteamVRObjects/RightHand");
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
        
    }

    void Start()
    {
        icon = 0;
        state = 0;
        GridState = false;
        RightHand.AddComponent<CreateHair>();
        GetComponent<CreateHair>().enabled = true;

    }

    void Update()
    {
        //Debug.Log("右:"+ Pose.transform.position);
        //cpicker_material.color = cpicker.color;
    
        if (m_Grip.GetStateDown(Pose.inputSource))
        {
            Drop();
        }

        if (icon == 0)
        {
            if (TriggerClick.GetStateDown(Pose.inputSource))
            {
                Pickup();
            }
        }
        if (TriggerClick.GetStateDown(Pose.inputSource))
        {
            //Debug.Log(Pose.transform.position);
            RightDown = true;
        }
        if (TriggerClick.GetStateUp(Pose.inputSource)) RightDown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paint"))
        {
            m_object = other.gameObject;
            state = 1;
        }
        if (other.gameObject.CompareTag("Eraser"))
        {
            m_object = other.gameObject;
            state = 2;
        }
        if (other.gameObject.CompareTag("Clear"))
        {
            m_object = other.gameObject;
            state = 3;
        }
        if (other.gameObject.CompareTag("Grid")) 
        {
            GridState = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Paint"))
        {
            m_object = null;
        }
        if (other.gameObject.CompareTag("Eraser"))
        {
            m_object = null;
        }
        if (other.gameObject.CompareTag("Clear"))
        {
            m_object = null;
        }
        if (other.gameObject.CompareTag("Grid"))
        {
            GridState = false;
        }

    }

    public void Pickup()
    {
        if (m_object == null) return;

        //something = m_object;
        
        ObjectPos = m_object.transform.localPosition;
        objRotatePos = m_object.transform.localRotation;
        Debug.Log("PickUp" + ObjectPos);
        if (m_object.GetComponent<InteractableContrallor>().m_ActiveHand != null)
        {
            m_object.GetComponent<InteractableContrallor>().m_ActiveHand.Drop();
        }
        Rigidbody target = m_object.GetComponent<Rigidbody>();
        m_Joint.connectedBody = target;
        m_object.GetComponent<InteractableContrallor>().m_ActiveHand = this;

        icon = state;
    }
    public void Drop()
    {
        if (m_object == null) return;
        
        Rigidbody target = m_object.GetComponent<Rigidbody>();
        target.velocity = Pose.GetVelocity();
        target.angularVelocity = Pose.GetAngularVelocity();
        
        m_Joint.connectedBody = null;
        m_object.GetComponent<InteractableContrallor>().m_ActiveHand = null;

        if (m_object.name == "eraser1")
        {
            m_object.transform.rotation = erasercopy.rotation;
            m_object.transform.position = erasercopy.position;
        }
        if (m_object.name == "paint1")
        {
            m_object.transform.rotation = paintcopy.rotation;
            m_object.transform.position = paintcopy.position;
        }
        if (m_object.name == "clear1")
        {
            m_object.transform.rotation = clearcopy.rotation;
            m_object.transform.position = clearcopy.position;
        }
        
        m_object = null;
        icon = 0;
        state = 0;
    }

    public void GetobjPos() 
    {
        if (PaintPos.position.y < 0.2f) PaintPos.position = paintcopy.position;
        if (PaintPos.position.y < 0.2f) PaintPos.position = erasercopy.position;
        if (PaintPos.position.y < 0.2f) PaintPos.position = clearcopy.position;

    }

}
