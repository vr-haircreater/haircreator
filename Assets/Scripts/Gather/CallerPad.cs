using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CallerPad : MonoBehaviour
{
    public SteamVR_Action_Boolean CallClick;
    public static SteamVR_Behaviour_Pose Pose;

    public static GameObject PadA, PadB, PadC, PadD; //A1 B2 C3 D4
    public static int state = 1;
    public static GameObject[] ClosePad = new GameObject[1];
    public static GameObject nowPad;

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();       
    }
    public static void FindPad()
    {
        PadA = GameObject.Find("Player/SteamVRObjects/LeftHand/PadA");
        PadB = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB");
        //PadC = GameObject.Find("Player/SteamVRObjects/LeftHand/PadC");
        //PadD = GameObject.Find("Player/SteamVRObjects/LeftHand/PadD");
    }
    public static void PadAShow()
    {
        state = 1;
        PadSwitcher();
    }
    public static void PadBShow()
    {
        state = 2;
        PadSwitcher();
    }
    public static void PadCShow()
    {
        state = 3;
        PadSwitcher();
    }
    public static void PadDShow()
    {
        state = 4;
        PadSwitcher();
    }
    public static void PadSwitcher()
    {
        FindPad();
        if (state == 1)
        {
            nowPad = PadA;
            ClosePad[0] = PadB;
            
            //ClosePad[1] = PadC;
            //ClosePad[2] = PadD;
        }
        if (state == 2)
        {
            ClosePad[0] = PadA;
            nowPad = PadB;
           
            //ClosePad[1] = PadC;
            //ClosePad[2] = PadD;
        }
        /*if (state == 3)
        {
            ClosePad[0] = PadA;
            ClosePad[1] = PadB;
            nowPad = PadC;
            ClosePad[2] = PadD;
        }
        if (state == 4)
        {
            ClosePad[0] = PadA;
            ClosePad[1] = PadB;
            ClosePad[2] = PadC;
            nowPad = PadD;
        }
        nowPad.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            ClosePad[i].SetActive(false);
        }*/
        nowPad.SetActive(true);
        
        ClosePad[0].SetActive(false);
    }

    void Update()
    {

        
    }
}