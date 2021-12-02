using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GatherControl : MonoBehaviour
{
    //拿起物件
    public SteamVR_Action_Boolean m_Pinch = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");//板機鍵按鈕
    public SteamVR_Action_Boolean m_Grip = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");//側鍵按鈕
    public static SteamVR_Behaviour_Pose Pose = null;
    public FixedJoint m_Joint = null;
    public static GameObject m_object = null;

    //功能狀態表示
    public static int icon=0;
    public static int state = 0;

    //顏色判定
    public static bool ColorRightDown = false;

    //讓物件回到原位
    public Transform ClearPos, EraserPos, PaintPos, erasercopy, paintcopy, clearcopy,ObjPos;

    public bool GetObjstate = true;


    // Start is called before the first frame update
    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
        gameObject.AddComponent<CreateHair>();
        ClearPos = GameObject.Find("Salon/Trolley/clear1").transform;
        EraserPos = GameObject.Find("Salon/Trolley/eraser1").transform;
        PaintPos = GameObject.Find("Salon/Trolley/paint1").transform;
        erasercopy = GameObject.Find("Salon/Trolley/erasercopy").transform;
        clearcopy = GameObject.Find("Salon/Trolley/clearcopy").transform;
        paintcopy = GameObject.Find("Salon/Trolley/paintcopy").transform;
        ObjPos = GameObject.Find("Player/SteamVRObjects/RightHand/ObjPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //拿起物件 & 切功能
        if (icon == 0 && GetObjstate==false)
        {
            if (m_Grip.GetStateDown(Pose.inputSource))
            {
                if(m_object!=null)Pickup();    
            }
            if (m_Grip.GetStateUp(Pose.inputSource) && m_object != null)//拿起後放開物件功能狀態才轉變
            {
                icon = state;
                GetObjstate = true;
            }
        }
        else if (icon == 2) //eraser
        {
            if (m_Pinch.GetStateDown(Pose.inputSource)) CreateHair.Eraser();
        }
        else if (icon == 3) //clear
        {
            if (m_Pinch.GetStateDown(Pose.inputSource)) CreateHair.Clear();
        }

        //顏色判定
        if (m_Pinch.GetStateDown(Pose.inputSource)) ColorRightDown = true;
        else if (m_Pinch.GetStateUp(Pose.inputSource)) ColorRightDown = false;

        //放下物件
        if (m_Grip.GetStateDown(Pose.inputSource) && GetObjstate == true) Drop();


    }

    public void Pickup()
    {
        if (m_object == null) return;
        
        if (m_object.GetComponent<InteractableContrallor>().m_ActiveHand != null)
        {
            m_object.GetComponent<InteractableContrallor>().m_ActiveHand.Drop();
        }
        Rigidbody targetBody = m_object.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
        m_object.GetComponent<InteractableContrallor>().m_ActiveHand = this;
        m_object.transform.position = ObjPos.position;

    }
    public void Drop()
    {
        if (m_object == null) return;

        Rigidbody targetBody = m_object.GetComponent<Rigidbody>();
        targetBody.velocity = Pose.GetVelocity();
        targetBody.angularVelocity = Pose.GetAngularVelocity();
        m_Joint.connectedBody = null;
        m_object.GetComponent<InteractableContrallor>().m_ActiveHand = null;

        //把物件位置歸位
        if (m_object.name == "eraser1")
        {
            m_object.transform.rotation = erasercopy.rotation;
            m_object.transform.position = erasercopy.position;
        }
        else if (m_object.name == "paint1")
        {
            m_object.transform.rotation = paintcopy.rotation;
            m_object.transform.position = paintcopy.position;
        }
        else if (m_object.name == "clear1")
        {
            m_object.transform.rotation = clearcopy.rotation;
            m_object.transform.position = clearcopy.position;
        }
        GetObjstate = false;
        m_object = null;
        state = 0;
        icon = 0;
    }

    private void OnTriggerEnter(Collider other)//判定碰沒碰到
    {
        if (other.gameObject.CompareTag("Paint"))
        {
            m_object = other.gameObject;
            state = 1;
        }
        else if (other.gameObject.CompareTag("Eraser"))
        {
            m_object = other.gameObject;
            state = 2;
        }
        else if (other.gameObject.CompareTag("Clear"))
        {
            m_object = other.gameObject;
            state = 3;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Paint"))
        {
            m_object = null;
        }
        else if (other.gameObject.CompareTag("Eraser"))
        {
            m_object = null;
        }
        else if (other.gameObject.CompareTag("Clear"))
        {
            m_object = null;
        }
    }

}
