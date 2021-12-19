using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Valve.VR;
using UnityEngine.UI;
using UnityEditor;

public class ColorPicker_control : MonoBehaviour
{    
    public static float color_H, color_S, color_V,addcolorH, addcolorS, addcolorV;
    public GameObject sv_img, h_img, sv_slider, h_slider;
    public GameObject dot, ColorPanel;
    Vector3 svPos,hPos,mousePos, SVsliderPos, HsliderPos;
    public float svWidth=200, svHeight=200, hWidth=16, hHeight=200, color_Htemp, color_Stemp, color_Vtemp;
    public static Material colorshow;
    public Material headcolor;
    public static Material headcolort = null;

    Vector3 p0, pvx1, pvx0;//起始原點、滑鼠座標、相對座標
    Vector3 V1, N_mouse;



    void Start()
    {
        svPos = sv_img.transform.position;
        hPos = h_img.transform.position;
        HsliderPos = h_slider.transform.position;
        color_Htemp = 0f;
        color_Stemp = 1f;
        color_Vtemp = 1f;
        dot = GameObject.Find("Player/SteamVRObjects/RightHand/PR_Pointer/Dot");
        colorshow = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB/PanelB/colorshow").GetComponent<Image>().material;
        ColorPanel = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB");
        pvx0 = new Vector3(-0.1399244f/2, -0.1399244f / 2, 0);//改

        headcolort = new Material(Shader.Find("Specular"));
        headcolort.color = headcolor.color;
    }
    
    void Update()
    {
        GetTurePos();
        svPos = sv_img.transform.position;
        hPos = h_img.transform.position; 

        
        if (GatherControl.ColorRightDown==true)//要按下才變顏色
        {            
            mousePos = dot.transform.position;
            HsliderPos = h_slider.transform.position;
            SVsliderPos = sv_slider.transform.position;

            if (ColorPicker_sv.CollGet == true)
            {
                color_Stemp =  V1.x / 0.1399244f;
                color_Vtemp =  1-(V1.y/ 0.1399244f); 
                sv_slider.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
            }
            if (ColorPicker_h.CollGet_H == true)
            {
                h_slider.transform.position = mousePos;
                color_Htemp = V1.x / 0.2011f;
                //Debug.Log("H:" + color_Htemp);
            }
        }
        color_H = color_Htemp;
        color_S = color_Stemp;
        color_V = color_Vtemp;
        if (ButtonTransitioner.btn10down==1) //頭皮顏色 
        {
            headcolor.color = Color.HSVToRGB(color_H, color_S, color_V);
            colorshow.color = Color.HSVToRGB(color_H, color_S, color_V);
        }
        else//髮片顏色
        {
            colorshow.color = Color.HSVToRGB(color_H, color_S, color_V);
        }
        
    }


    private static Vector3 Calcute_Cross(GameObject obj, Vector3 Vec)
    {
        Vector3 cx = Vector3.Cross(obj.transform.up, obj.transform.forward).normalized;
        Vector3 cy = Vector3.Cross(obj.transform.right, obj.transform.forward).normalized;
        Vector3 cz = Vector3.Cross(obj.transform.up, obj.transform.right).normalized;
        Vector3 newVec = new Vector3(Vector3.Dot(Vec, cx), Vector3.Dot(Vec, cy), Vector3.Dot(Vec, cz));
        return newVec;
    }

    public void GetTurePos()
    {
        pvx1 = Calcute_Cross(ColorPanel, pvx0);
        p0 = sv_img.transform.position + pvx1;

        if (GatherControl.ColorRightDown == true) mousePos = dot.transform.position;

        //滑鼠Move
        N_mouse = mousePos - p0;
        V1 = Calcute_Cross(ColorPanel, N_mouse);

        //XVector
        Vector3 cx1 = Vector3.Cross(ColorPanel.transform.forward, ColorPanel.transform.up).normalized;

        //YVector
        Vector3 cy1 = Vector3.Cross(ColorPanel.transform.forward, ColorPanel.transform.right).normalized;

    }
}