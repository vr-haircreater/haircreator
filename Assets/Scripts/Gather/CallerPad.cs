using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CallerPad : MonoBehaviour
{
    public SteamVR_Action_Boolean CallClick;
    public static SteamVR_Behaviour_Pose Pose;

    public static GameObject PadA, PadB, PadC, PadD, PadE, PadF; 
    public static int state = 5,N = 6;
    public static GameObject[] ClosePad = new GameObject[6];
    public static GameObject nowPad;
    public static GameObject[] Pads = new GameObject[6];
    public static bool ox;
    public static int i;

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    public static void FindPad()
    {
        Pads[0] = PadA = GameObject.Find("Player/SteamVRObjects/LeftHand/PadA");
        Pads[1] = PadB = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB");
        Pads[2] = PadC = GameObject.Find("Player/SteamVRObjects/LeftHand/PadD");
        Pads[3] = PadD = GameObject.Find("Player/SteamVRObjects/LeftHand/PadC");
        Pads[4] = PadE = GameObject.Find("Player/SteamVRObjects/LeftHand/PadE");
        Pads[5] = PadF = GameObject.Find("Player/SteamVRObjects/LeftHand/Animation");
        /*PadA = GameObject.Find("Player/SteamVRObjects/LeftHand/PadA");
        PadB = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB");
        PadC = GameObject.Find("Player/SteamVRObjects/LeftHand/PadD");
        PadD = GameObject.Find("Player/SteamVRObjects/LeftHand/PadC");*/
        //state = 0;
    }
    public static void PadAShow()
    {
        state = 0;
        PadSwitcher();
    }
    public static void PadBShow()
    {
        state = 1;
        PadSwitcher();
    }
    public static void PadCShow()
    {
        state = 2;
        PadSwitcher();
    }
    public static void PadDShow()
    {
        state = 3;
        PadSwitcher();
    }
    public static void PadEShow()
    {
        state = 4;
        PadSwitcher();
    }
    public static void AnimationShow()
    {
        state = 5;
        PadSwitcher();
    }
    /*public static void PadSwitcher() //A1 B2 C3 D4
    {
        FindPad();
        if (state == 0)
        {
            nowPad = PadA;
            ClosePad[0] = PadB;
            ClosePad[1] = PadC;
            ClosePad[2] = PadD;
        }
        if (state == 1)
        {
            ClosePad[0] = PadA;
            nowPad = PadB;
            ClosePad[1] = PadC;
            ClosePad[2] = PadD;
        }
        if (state == 2)
        {
            ClosePad[0] = PadA;
            ClosePad[1] = PadB;
            nowPad = PadC;
            ClosePad[2] = PadD;
        }
        if (state == 3)
        {
            ClosePad[0] = PadA;
            ClosePad[1] = PadB;
            ClosePad[2] = PadC;
            nowPad = PadD;
        }

        for (int i = 0; i < 3; i++)
        {
            ClosePad[i].SetActive(false);
        }
        nowPad.SetActive(true);

    }*/

    public static void PadSwitcher()
    {
        FindPad();
        for(i = 0; i < 6; i++)
        {
            print(state); ox = (i==state) ? true : false; print(ox);
            Pads[i].SetActive(ox);
            /*if (i == state) Pads[i].SetActive(true);
            else Pads[i].SetActive(false);
            print(state);*/
        }
    }
    void Update()
    {

        
    }
}