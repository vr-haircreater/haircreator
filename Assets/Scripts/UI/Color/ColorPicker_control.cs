using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Valve.VR;
using UnityEngine.UI;

public class ColorPicker_control : MonoBehaviour
{    
    public static float color_H, color_S, color_V;
    public GameObject sv_img, h_img, sv_slider, h_slider;
    public GameObject dot;
    Vector3 svPos,hPos,mousePos, SVsliderPos, HsliderPos;
    public float svWidth=200, svHeight=200, hWidth=16, hHeight=200, color_Htemp, color_Stemp, color_Vtemp;
    public static Material colorshow;
    Vector3 Crossx, Crossy, Crossz,SVNewPos,dotPos;
    Vector3 degx, degy;

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
    }
    
    void Update()
    {
        VectorCross();
        svPos = sv_img.transform.position;
        hPos = h_img.transform.position;
 
        SVsliderPos = sv_slider.transform.position;
        HsliderPos = h_slider.transform.position;

        if (Gather1.RightDown==true)
        {
            mousePos = dot.transform.position;
            HsliderPos = h_slider.transform.position;
            //print("mouse:"+mousePos);
            if (mousePos.x >= (svPos.x - 0.1399244f / 2) && mousePos.x <= (svPos.x  + 0.1399244f/2) && mousePos.y <= (svPos.y  + 0.1299029f / 2) && mousePos.y >= (svPos.y- 0.1299029f/2))
            {
                //print("SV:"+mousePos);
                //SVsliderPos = sv_slider.transform.position;
                sv_slider.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
                color_Stemp = (mousePos.x - (svPos.x - (0.1399244f / 2))) / 0.1399244f;
                color_Vtemp = (mousePos.y - svPos.y + 0.1299029f / 2) / 0.1299029f;
                Debug.Log("Y");
            }
            if (mousePos.x >= hPos.x - 0.1483383f/2 && mousePos.x <= (hPos.x + 0.1483383f/2) && mousePos.y <= hPos.y + 0.006989386f/2 && mousePos.y >= hPos.y - 0.006989386f/2)
            {
                //print("H:"+mousePos);
                
                h_slider.transform.position = new Vector3(mousePos.x, hPos.y, hPos.z);
                color_Htemp = (mousePos.x - hPos.x + (0.1483383f / 2)) / 0.1483383f;
            }
            //Debug.Log("Pos:" + mousePos);
            //Debug.Log("SVPos" + svPos);
        }


        color_H = color_Htemp;
        color_S = color_Stemp;
        color_V = color_Vtemp;
        colorshow.color = Color.HSVToRGB(color_H,color_S, color_V);
        //print(/*color_H + "," + color_S + "," + */color_V);
    }

    public void VectorCross() 
    {
        Crossx = Vector3.Cross(sv_img.transform.forward, sv_img.transform.up).normalized;//x 
        Crossy = Vector3.Cross(sv_img.transform.forward, sv_img.transform.right).normalized;//y
        Crossz = Vector3.Cross(sv_img.transform.up, sv_img.transform.right);//z*/

        Vector3 degx0 = new Vector3(sv_img.transform.position.x* Crossx.x, sv_img.transform.position.y * Crossx.y, sv_img.transform.position.z * Crossx.z);
        Vector3 degy0 = new Vector3(sv_img.transform.position.x * Crossy.x, sv_img.transform.position.y * Crossy.y, sv_img.transform.position.z * Crossy.z);

        degx = sv_img.transform.position - degx0;
        degy = sv_img.transform.position - degy0;



        Debug.Log("沒轉"+sv_img.transform.position + "轉" + Crossx);
        Debug.Log("差"+degx);
        //Debug.Log("轉"+ SVNewPos);

        
    
    }
}