using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Gather1 : MonoBehaviour
{
    public static int icon;
    public static int state;
    public static bool GridState;
    GameObject RightHand;
    public static bool RightDown = false;

    
    public static SteamVR_Behaviour_Pose Pose = null;
    public FixedJoint m_Joint = null;
    public static GameObject m_object = null;

    public SteamVR_Action_Boolean TriggerClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");//板機鍵按鈕
    public SteamVR_Action_Boolean m_Grip = null;
    int tempforstate23 = 0;

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
        GetobjPos();
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
            else if (TriggerClick.GetStateUp(Pose.inputSource) && m_object != null) 
            {
                icon = state;//放開才能換功能
            }
        }
        if (TriggerClick.GetStateDown(Pose.inputSource))
        {
            //Debug.Log(Pose.transform.position);
            RightDown = true; 
        }
        if (TriggerClick.GetStateUp(Pose.inputSource)) RightDown = false;

        /*if(m_object!= null)
        {
            if (m_object.name == "Eraser1")//if(state==2)
                forState23();

            if(state==3)//if (m_object.name == "Clear1")
                forState23();
        }
        */
        if(m_object!=null)
        forState23();

    }
    public void forState23()
    {       
        if (m_object.name == "eraser1") //if(state ==2)
        {
            //Debug.Log("into state 2");
            if (TriggerClick.GetStateDown(Pose.inputSource) && tempforstate23 == 1)
            {
                CreateHair.Eraser();
                Debug.Log("2");
            }
            if (TriggerClick.GetStateDown(Pose.inputSource)&& tempforstate23==0)
            {
                tempforstate23 = 1;                 
                Debug.Log("0");                
            }
            if (TriggerClick.GetStateUp(Pose.inputSource))
            {
                Debug.Log("1");                
            }
            

        }
        if (m_object.name == "clear1")
        {
            //Debug.Log("into state 3");
            if (TriggerClick.GetStateDown(Pose.inputSource) && tempforstate23 == 1)
            {
                CreateHair.Clear();
                Debug.Log("2");
            }
            if (TriggerClick.GetStateDown(Pose.inputSource) && tempforstate23 == 0)
            {
                tempforstate23 = 1;
                Debug.Log("0");
            }
            if (TriggerClick.GetStateUp(Pose.inputSource))
            {
                Debug.Log("1");
            }
            
        }
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
            //forState23();
        }
        if (other.gameObject.CompareTag("Clear"))
        {
            m_object = other.gameObject;
            state = 3;
            //forState23();
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


        //if (m_object != null) Debug.Log("1234");
        //if (m_object == null) Debug.Log("mobj is null now.");
    }

    public void Pickup()
    {
        if (m_object == null) return;
        /*
        m_object.transform.SetParent(GameObject.Find("Player/SteamVRObjects/RightHand").transform);
        m_object.transform.localPosition = new Vector3(0f, 0f, 0f);
        m_object.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        */

        //ObjectPos = m_object.transform.localPosition;
        //objRotatePos = m_object.transform.localRotation;
        //Debug.Log("PickUp" + ObjectPos);
        if (m_object.GetComponent<InteractableContrallor>().m_ActiveHand != null)
        {
            m_object.GetComponent<InteractableContrallor>().m_ActiveHand.Drop();
        }
        Rigidbody target = m_object.GetComponent<Rigidbody>();
        m_Joint.connectedBody = target;
        m_object.GetComponent<InteractableContrallor>().m_ActiveHand = this;

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
        if (PaintPos.localPosition.y < 0.1f) PaintPos.position = paintcopy.position;
        if (ClearPos.localPosition.y < 0.1f) ClearPos.position = clearcopy.position;
        if (EraserPos.localPosition.y < 0.1f) EraserPos.position = erasercopy.position;

    }

}
