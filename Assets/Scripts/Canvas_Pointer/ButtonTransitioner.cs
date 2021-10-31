﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour,  IPointerDownHandler
{
    public static Image[] colorimgs = new Image[11];
    public static GameObject showimg, slider1img;
    public static GameObject[] btns = new GameObject[12];
    public static int btn_color = 0;
    public static Color[] Colorbtns = new Color[11];
    public static Slider Sslider1, Sslider2, Sslider3;
    public static GameObject Gslider1, Wslider1, Tslider1;
    public static Color HairColor;
    public GameObject PadA,role,rolehair;
  
    public static int HairWidth;
    public static int HairThickness;

    public static bool HairTail = true;

    float H, S, V;

    //private Image m_Image = null;
    //public GameObject PadB, PadC;

    public static int HairStyleState = 1;
    public Animator animator;

    private void Awake()
    {

        //PadB = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB");
        //PadC = GameObject.Find("Player/SteamVRObjects/LeftHand/PadC");
        PadA = GameObject.Find("Player/SteamVRObjects/LeftHand/PadA");

        if (PadA.activeSelf == true) FindPadAObject();

    }

    public void Update()
    {

        if (PadA.activeSelf == true)
        {
            HairColor = showimg.GetComponent<Image>().color;
            SliderW();
            SliderT();  
            Slider1();
        }
    }
   
   
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("state=" + CallerPad.state);
        if (CallerPad.state == 0)
        {
            FindPadATag();
            Slider1();
            SliderW();
            SliderT();
        }
        if (CallerPad.state == 1) FindPadBTag();
        if (CallerPad.state == 2) FindPadDTag();

        /*if (PadA.activeSelf == true) { 
           
        }*/
    }
   

    public void Slider1() //調動深淺
    {
        Color.RGBToHSV(showimg.GetComponent<Image>().color, out H, out S, out V);
        Sslider1.value = S * 100.0f;
        Sslider1.onValueChanged.AddListener(delegate { ValueChangeCheck0(); }); //一直ADD
    }
    public void SliderW() //粗細
    {
        HairWidth = (int)Sslider2.value;
    }
    public void SliderT() //薄厚
    {
        HairThickness = (int)Sslider3.value;
    }
    public void ValueChangeCheck0()
    {
        showimg.GetComponent<Image>().color = Color.HSVToRGB(H, Sslider1.value / 100f, V);
        HairColor = showimg.GetComponent<Image>().color;
    }

    public void FindPadATag()
    {
        //===== PadA-Buttons
        if (gameObject.tag == "button0")
        {
            btn_color = 0;
            showimg.GetComponent<Image>().color = Colorbtns[0];
        }
        else if (gameObject.tag == "button1")
        {
            btn_color = 1;
            showimg.GetComponent<Image>().color = Colorbtns[1];
        }
        else if (gameObject.tag == "button2")
        {
            btn_color = 2;
            showimg.GetComponent<Image>().color = Colorbtns[2];
        }
        else if (gameObject.tag == "button3")
        {
            btn_color = 3;
            showimg.GetComponent<Image>().color = Colorbtns[3];
        }
        else if (gameObject.tag == "button4")
        {
            btn_color = 4;
            showimg.GetComponent<Image>().color = Colorbtns[4];
        }
        else if (gameObject.tag == "button5")
        {
            btn_color = 5;
            showimg.GetComponent<Image>().color = Colorbtns[5];
        }
        else if (gameObject.tag == "button6")
        {
            btn_color = 6;
            showimg.GetComponent<Image>().color = Colorbtns[6];
        }
        else if (gameObject.tag == "button7")
        {
            btn_color = 7;
            showimg.GetComponent<Image>().color = Colorbtns[7];
        }
        else if (gameObject.tag == "button8")
        {
            btn_color = 8;
            showimg.GetComponent<Image>().color = Colorbtns[8];
        }
        else if (gameObject.tag == "button9")
        {
            btn_color = 9;
            showimg.GetComponent<Image>().color = Colorbtns[9];
        }
        else if (gameObject.tag == "button10")
        {
            btn_color = 10;
            showimg.GetComponent<Image>().color = Colorbtns[10];
        }
        else if (gameObject.tag == "button11")
        {
            //btn_color = 11;
            CallerPad.PadBShow();
            Debug.Log("am i ?");
            //PadB.SetActive(true);
            //PadC.SetActive(false);
        }
        //===== PadA-Bar 在上面一堆slider的地方
        else if (gameObject.tag == "Gslider1") //
        {
            Slider1();
        }
        //===== PadA-choose 直/波浪/螺旋
        else if (gameObject.tag == "Straight_HairStyle") //直
        {
            HairStyleState = 1;
        }
        else if (gameObject.tag == "WaveHairStyle") //波浪
        {
            HairStyleState = 2;
        }
        else if (gameObject.tag == "TwistHairStyle") //螺旋
        {
            HairStyleState = 3;
        }
        else if (gameObject.tag == "ArrowButton")
        {
            CallerPad.PadCShow();
        }
        //===== PadA-lower (BUG:undo刪兩個)
        else if (gameObject.tag == "UndoButton")
        {
            CreateHair.Undo();
        }
        else if (gameObject.tag == "SaveButton")
        {
            //animator.SetTrigger("Pose1");
            animator = GameObject.Find("Girl").GetComponent<Animator>();//之後要考慮男女
            animator.SetTrigger("Pose");
            role = GameObject.Find("Girl");
            role.transform.position = new Vector3(0.094f,0.12f,3.92f);
            CallerPad.PadDShow();
            
        }
        else if (gameObject.tag == "RedoButton")
        {
            CreateHair.Redo();
        }

    }
    public void FindPadBTag()
    {
        //===== PadB-yes/no (顏色等敏真寫完)
        if (gameObject.tag == "PadBYes")
        {
            CallerPad.PadAShow();
            showimg.GetComponent<Image>().color = ColorPicker_control.colorshow.color;
        }
        else if (gameObject.tag == "PadBNo")
        {
            CallerPad.PadAShow();
        }

    }
  
    public void FindPadDTag()
    {
        //===== PadD-yes/no (宜均要加其他tag上去)
        if (gameObject.tag == "PadDYes")
        {
            CallerPad.PadAShow();
        }
        else if (gameObject.tag == "PadDNo")
        {
            CallerPad.PadAShow();
        }
        else if (gameObject.tag == "HairTip")
        {
            CreateHair.HairTail = true;
        }
        else if (gameObject.tag == "HairStraight")
        {
            CreateHair.HairTail = false;
        }
        else if (gameObject.tag == "SmallRoll")
        {
            CreateHair.Curve = 0.6f;
        }
        else if (gameObject.tag == "MediumRoll")
        {
            CreateHair.Curve = 0.8f;
        }
        else if (gameObject.tag == "BigRoll")
        {
            CreateHair.Curve = 1.0f;
        }


    }

    public void FindPadAObject()
    {
        //m_Image = GetComponent<Image>();

        btns[0] = GameObject.FindGameObjectWithTag("button0");
        btns[1] = GameObject.FindGameObjectWithTag("button1");
        btns[2] = GameObject.FindGameObjectWithTag("button2");
        btns[3] = GameObject.FindGameObjectWithTag("button3");
        btns[4] = GameObject.FindGameObjectWithTag("button4");
        btns[5] = GameObject.FindGameObjectWithTag("button5");
        btns[6] = GameObject.FindGameObjectWithTag("button6");
        btns[7] = GameObject.FindGameObjectWithTag("button7");
        btns[8] = GameObject.FindGameObjectWithTag("button8");
        btns[9] = GameObject.FindGameObjectWithTag("button9");
        btns[10] = GameObject.FindGameObjectWithTag("button10");
        btns[11] = GameObject.FindGameObjectWithTag("button11");

        Colorbtns[0] = Color.HSVToRGB(34.0f / 360.0f, 39.0f / 100.0f, 87.0f / 100.0f);
        Colorbtns[1] = Color.HSVToRGB(60.0f / 360.0f, 21.0f / 100.0f, 1.0f);
        Colorbtns[2] = Color.HSVToRGB(319.0f / 360.0f, 13.0f / 100.0f, 99.0f / 100.0f);
        Colorbtns[3] = Color.HSVToRGB(0, 0, 0);
        Colorbtns[4] = Color.HSVToRGB(0, 0f, 68.0f / 100.0f);
        Colorbtns[5] = Color.HSVToRGB(25.0f / 360.0f, 43.0f / 100.0f, 99.0f / 100.0f);
        Colorbtns[6] = Color.HSVToRGB(194.0f / 360.0f, 29.0f / 100.0f, 1.0f);
        Colorbtns[7] = Color.HSVToRGB(296.0f / 360.0f, 27.0f / 100.0f, 1.0f);
        Colorbtns[8] = Color.HSVToRGB(0, 0, 1.0f);
        Colorbtns[9] = Color.HSVToRGB(78.0f / 360.0f, 1.0f, 84.0f / 100.0f);
        Colorbtns[10] = Color.HSVToRGB(0, 49.0f / 100.0f, 77.0f / 100.0f);

        showimg = GameObject.FindGameObjectWithTag("showimage");
        slider1img = GameObject.FindGameObjectWithTag("slider1img");
        Gslider1 = GameObject.FindGameObjectWithTag("Gslider1");
        Wslider1 = GameObject.FindGameObjectWithTag("Wslider1");
        Tslider1 = GameObject.FindGameObjectWithTag("Tslider1");

        for (int i = 0; i < 11; i++)
        {
            //btns[i].AddComponent<Image>();
            colorimgs[i] = btns[i].GetComponent<Image>();
            colorimgs[i].GetComponent<Image>().color = Colorbtns[i];
        }

        showimg.GetComponent<Image>();
        slider1img.GetComponent<Image>();

        //將gameobject slider的slider功能指給程式中的Sslider用
        Sslider1 = Gslider1.GetComponent<Slider>();
        Sslider2 = Wslider1.GetComponent<Slider>();
        Sslider3 = Tslider1.GetComponent<Slider>();

        showimg.GetComponent<Image>().color = Colorbtns[0];
        

    }




}