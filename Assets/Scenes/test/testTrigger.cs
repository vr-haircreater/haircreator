using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class testTrigger : MonoBehaviour
{
    public static SteamVR_Behaviour_Pose Pose = null;
    public SteamVR_Action_Boolean TriggerClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");//板機鍵按鈕
    GameObject RightHand;
    int temp = 0;
    void Awake()
    {
        RightHand = GameObject.Find("Player/SteamVRObjects/RightHand");
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    void Update()
    {
        if (TriggerClick.GetStateDown(Pose.inputSource) && temp == 1)
        {
            Debug.Log("qaq");
        }
        if (TriggerClick.GetStateDown(Pose.inputSource)&&temp==0)
        {
            Debug.Log("qqqqqqqq");
            temp = 1;
        }
        if (TriggerClick.GetStateUp(Pose.inputSource))
        {
            Debug.Log("HanDingSheng");

        }
        
    }
}
