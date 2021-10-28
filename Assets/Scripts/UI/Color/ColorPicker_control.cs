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
    Vector3 N_MousePos;

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
        svPos = sv_img.transform.position;
        hPos = h_img.transform.position; 

        
        if (Gather1.RightDown==true)
        {            
            mousePos = dot.transform.position;
            HsliderPos = h_slider.transform.position;
            SVsliderPos = sv_slider.transform.position;

      
            sv_slider.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
            
            if (mousePos.x >= (svPos.x - 0.1399244f / 2) && mousePos.x <= (svPos.x  + 0.1399244f/2) && mousePos.y <= (svPos.y  + 0.1299029f / 2) && mousePos.y >= (svPos.y- 0.1299029f/2))
            {
                /*color_Stemp = (mousePos.x - (svPos.x - 0.1399244f / 2)) / 0.1399244f;
                color_Vtemp = (mousePos.y - (svPos.y + 0.1299029f / 2)) / 0.1299029f;
                Debug.Log("Y");*/

                Vector3 Now_mos = mousePos - (svPos + new Vector3(-0.1399244f / 2f, 0.1299029f / 2f, 0f));
                Vector3 vlux = Vector3.Cross(sv_img.transform.up, sv_img.transform.forward).normalized;//x
                Vector3 vluy = Vector3.Cross(sv_img.transform.right, sv_img.transform.forward).normalized;//y
                Vector3 vluz = Vector3.Cross(sv_img.transform.up, sv_img.transform.right).normalized;//z
           
                N_MousePos = new Vector3(Vector3.Dot(Now_mos, vlux), Vector3.Dot(Now_mos, vluy), Vector3.Dot(Now_mos, vluz));
                
                color_Stemp = Mathf.Abs(N_MousePos.x) / 0.1399244f;
                color_Vtemp = Mathf.Abs(N_MousePos.y) / 0.1299029f;

                Debug.Log("X:" + color_Stemp);
                Debug.Log("Y:" + color_Vtemp);
            }
            if (mousePos.x >= hPos.x - 0.1483383f/2 && mousePos.x <= (hPos.x + 0.1483383f/2) && mousePos.y <= hPos.y + 0.006989386f/2 && mousePos.y >= hPos.y - 0.006989386f/2)
            {
                 h_slider.transform.position = new Vector3(mousePos.x, hPos.y, hPos.z);
                color_Htemp = (mousePos.x - hPos.x + (0.1483383f / 2)) / 0.1483383f;
            }
        }
        color_H = color_Htemp;
        color_S = color_Stemp;
        color_V = color_Vtemp;
        colorshow.color = Color.HSVToRGB(color_H,color_S, color_V);
    }

    public void VectorCross() 
    {
        Crossx = Vector3.Cross(sv_img.transform.forward, sv_img.transform.up);//x 
        Crossy = Vector3.Cross(sv_img.transform.forward, sv_img.transform.right);//y
        Crossz = Vector3.Cross(sv_img.transform.up, sv_img.transform.right);//z*/
        N_MousePos = new Vector3(Vector3.Dot(mousePos, Crossx), Vector3.Dot(mousePos, Crossy), Vector3.Dot(mousePos, Crossz));//初始V1 * 轉換矩陣 = V2;
    }
}