using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ButtonTransitioner : MonoBehaviour,  IPointerDownHandler
{
    public static Image[] colorimgs = new Image[11];
    public static GameObject showimg, slider1img;
    public static GameObject[] btns = new GameObject[12];
    public static Color[] Colorbtns = new Color[11];
    public static Slider Sslider1, Sslider2, Sslider3;
    public static GameObject Gslider1, Wslider1, Tslider1;
    public static Color HairColor;
    public GameObject PadA,PadB,role,rolehair,pointer;
    public static Image[] coloraddimgs = new Image[5];
    public static GameObject[] addbtns = new GameObject[5];
    public static Color[] Coloradd = new Color[5];
    

    public static int HairWidth;//調整寬度
    public static int HairThickness;//調整厚度
    public static int btn10down=0, headcolorN=0, coloraddcount = 0;

    public static bool HairTail = true,HeadState = false;
    public Material Head;
    public bool advancedColor = false;

    float H, S, V;

    public static int HairStyleState = 1;
    public Animator animator;

    private void Awake()
    {
        Head = Resources.Load<Material>("Materials/F00_000_00_HairBack_00_HAIR");
        PadA = GameObject.Find("Player/SteamVRObjects/LeftHand/PadA");
        if (PadA.activeSelf == true) FindPadAObject();
        PadB = GameObject.Find("Player/SteamVRObjects/LeftHand/PadB");
        if (PadB.activeSelf == true) FindPadBObject();
    }

    public void Update()
    {
        if (PadA.activeSelf == true)
        {
            HairColor = showimg.GetComponent<Image>().color;
            Slider1();
            SliderWT();
        }
    }
   
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Debug.Log("state=" + CallerPad.state);
        if (CallerPad.state == 0)
        {
            FindPadATag();
            Slider1();
            SliderWT();
        }
        if (CallerPad.state == 1) FindPadBTag();
        if (CallerPad.state == 2) FindPadDTag();
        if (CallerPad.state == 4) FindPadETag();
        if (CallerPad.state == 5) FindAnimationTag();
    }


    public void Slider1() //調動深淺
    {
        Color.RGBToHSV(showimg.GetComponent<Image>().color, out H, out S, out V);
        Sslider1.value = S * 100.0f;
        showimg.GetComponent<Image>().color = Color.clear;
        showimg.GetComponent<Image>().color = Color.HSVToRGB(H, Sslider1.value / 100f, V);
        HairColor = showimg.GetComponent<Image>().color;
        Sslider1.onValueChanged.AddListener(delegate { ValueChangeCheck0(); }); //一直ADD
    }
    public void ValueChangeCheck0()
    {
        showimg.GetComponent<Image>().color = Color.HSVToRGB(H, Sslider1.value / 100f, V);
        HairColor = showimg.GetComponent<Image>().color;
    }
    public void Slider_Drag() 
    {
        Color.RGBToHSV(showimg.GetComponent<Image>().color, out H, out S, out V);
        S = Sslider1.value/100f;
        showimg.GetComponent<Image>().color = Color.clear;
        showimg.GetComponent<Image>().color = Color.HSVToRGB(H, S, V);


    }
    
    public void SliderWT() //薄厚
    {
        HairWidth = (int)Sslider2.value;
        if (Sslider3.value > Sslider2.value) Sslider3.value = Sslider2.value;
        HairThickness = (int)Sslider3.value;
    }

    public void FindPadATag()
    {
        //===== PadA-Buttons
        if (gameObject.tag == "button0")
        {
            showimg.GetComponent<Image>().color = Color.clear;//先清顏色才不會一直覆蓋
            showimg.GetComponent<Image>().color = Colorbtns[0];
        }
        else if (gameObject.tag == "button1")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[1];
        }
        else if (gameObject.tag == "button2")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[2];
        }
        else if (gameObject.tag == "button3")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[3];
        }
        else if (gameObject.tag == "button4")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[4];
        }
        else if (gameObject.tag == "button5")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[5];
        }
        else if (gameObject.tag == "button6")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[6];
        }
        else if (gameObject.tag == "button7")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[7];
        }
        else if (gameObject.tag == "button8")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[8];
        }
        else if (gameObject.tag == "button9")
        {
            showimg.GetComponent<Image>().color = Color.clear;
            showimg.GetComponent<Image>().color = Colorbtns[9];
        }
        else if (gameObject.tag == "button10") //頭皮顏色
        {
            headcolorN = 0;
            HeadState = true;
            CallerPad.PadBShow();
        }
        else if (gameObject.tag == "button11") //髮片顏色
        {
            CallerPad.PadBShow();
        }
        //===== PadA-Bar 在上面一堆slider的地方
        else if (gameObject.tag == "Gslider1") //
        {
            Slider1();
        }
        //===== PadA-choose 回到原點/進階髮片/動畫
        else if (gameObject.tag == "StartPoint")
        {
            Transform Pos = GameObject.Find("Player").transform;
            Pos.position = new Vector3(-0.1f,0.1f,2.6f);
            Pos.rotation = new Quaternion(0f,-110f,0f,0f);
        }
        else if (gameObject.tag == "ChooseHair")
        {
            CallerPad.PadCShow();
        }
        else if (gameObject.tag == "Animation")
        {
            CallerPad.PadEShow();
            Transform Pos = GameObject.Find("Player").transform;
            Pos.position = new Vector3(-0.1f, 0.1f, 2.6f);
            Pos.rotation = new Quaternion(0f, 110f, 0f, 0f);

            animator = GameObject.Find("Girl").GetComponent<Animator>();
            animator.SetTrigger("Pose");
            role = GameObject.Find("Girl");
            role.transform.position = new Vector3(0f, 0f, 0f);
        }
        else if (gameObject.tag == "ArrowButton")
        {
            CallerPad.AnimationShow();
        }
        //===== PadA-lower (BUG:undo刪兩個)
        else if (gameObject.tag == "UndoButton")
        {
            CreateHair.Undo();
        }
        else if (gameObject.tag == "SaveButton")
        {
            animator = GameObject.Find("Girl").GetComponent<Animator>();
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

        Debug.Log("Hi B");
        //===== PadB-yes/no 
        if (gameObject.tag == "PadBYes")
        {
            Debug.Log("Yes color");
            
            
            CallerPad.PadAShow();
            if (HeadState == false)
            {
                showimg.GetComponent<Image>().color = Color.clear;
                showimg.GetComponent<Image>().color = ColorPicker_control.colorshow.color;
            }
            else
            {
                //頭皮暫存成功
                Head.color = Color.clear;
                Head.color = ColorPicker_control.colorshow.color;
                //ColorPicker_control.headcolort.color = Color.clear;
                //ColorPicker_control.headcolort.color = Head.color;
                HeadState = false;
            }
            
        }
        else if (gameObject.tag == "PadBNo") //按取消返回前一個顏色
        {
            
            if (HeadState == true)
            {
                Head.color = Color.clear;
                Head.color = ColorPicker_control.headcolort.color;
                HeadState = false;
            }
                
            CallerPad.PadAShow();

        }
        else if (gameObject.tag == "PadBAdd") //新增一個顏色
        {
            Debug.Log("Addcolor");
            Coloradd[coloraddcount] = ColorPicker_control.colorshow.color;
            coloraddimgs[coloraddcount] = addbtns[coloraddcount].GetComponent<Image>();
            coloraddimgs[coloraddcount].GetComponent<Image>().color = Color.clear;
            coloraddimgs[coloraddcount].GetComponent<Image>().color = Coloradd[coloraddcount];
            coloraddcount++;
            if (coloraddcount >= 5) coloraddcount = 0;
        }
        else if (gameObject.tag == "ButtonA")
        {

            if (HeadState == true)
            {
                Head.color = Color.clear;
                Head.color = Coloradd[0];
                ColorPicker_control.headcolort.color = Color.clear;
                ColorPicker_control.headcolort.color = Head.color;
                HeadState = false;
            }
            else
            {
                showimg.GetComponent<Image>().color = Color.clear;
                showimg.GetComponent<Image>().color = Coloradd[0];
                Color.RGBToHSV(Coloradd[0], out ColorPicker_control.addcolorH, out ColorPicker_control.addcolorS, out ColorPicker_control.addcolorV);
            }          
            CallerPad.PadAShow();
        }
        else if (gameObject.tag == "ButtonB")
        {
            if (HeadState == true)
            {
                Head.color = Color.clear;
                Head.color = Coloradd[1];
                ColorPicker_control.headcolort.color = Color.clear;
                ColorPicker_control.headcolort.color = Head.color;
                HeadState = false;
            }
            else
            {
                showimg.GetComponent<Image>().color = Color.clear;
                showimg.GetComponent<Image>().color = Coloradd[1];
                Color.RGBToHSV(Coloradd[1], out ColorPicker_control.addcolorH, out ColorPicker_control.addcolorS, out ColorPicker_control.addcolorV);
            }
            CallerPad.PadAShow();
        }
        else if (gameObject.tag == "ButtonC")
        {
            if (HeadState == true)
            {
                Head.color = Color.clear;
                Head.color = Coloradd[2];
                ColorPicker_control.headcolort.color = Color.clear;
                ColorPicker_control.headcolort.color = Head.color;
                HeadState = false;
            }
            else
            {
                showimg.GetComponent<Image>().color = Color.clear;
                showimg.GetComponent<Image>().color = Coloradd[2];
                Color.RGBToHSV(Coloradd[2], out ColorPicker_control.addcolorH, out ColorPicker_control.addcolorS, out ColorPicker_control.addcolorV);
            }
            CallerPad.PadAShow();
        }
        else if (gameObject.tag == "ButtonD")
        {
            if (HeadState == true)
            {
                Head.color = Color.clear;
                Head.color = Coloradd[3];
                ColorPicker_control.headcolort.color = Color.clear;
                ColorPicker_control.headcolort.color = Head.color;
                HeadState = false;
            }
            else
            {
                showimg.GetComponent<Image>().color = Color.clear;
                showimg.GetComponent<Image>().color = Coloradd[3];
                Color.RGBToHSV(Coloradd[3], out ColorPicker_control.addcolorH, out ColorPicker_control.addcolorS, out ColorPicker_control.addcolorV);
            }
            CallerPad.PadAShow();
        }
        else if (gameObject.tag == "ButtonE")
        {
            if (HeadState == true)
            {
                Head.color = Color.clear;
                Head.color = Coloradd[4];
                ColorPicker_control.headcolort.color = Color.clear;
                ColorPicker_control.headcolort.color = Head.color;
                HeadState = false;
            }
            else
            {
                showimg.GetComponent<Image>().color = Color.clear;
                showimg.GetComponent<Image>().color = Coloradd[4];
                Color.RGBToHSV(Coloradd[4], out ColorPicker_control.addcolorH, out ColorPicker_control.addcolorS, out ColorPicker_control.addcolorV);
            }
            CallerPad.PadAShow();
        }

    }

    public void FindPadDTag()
    {
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
            CreateHair.Curve1 = 1.0f;
            CreateHair.Curve2 = 1.1f;
        }
        else if (gameObject.tag == "MediumRoll")
        {
            CreateHair.Curve1 = 0.8f;
            CreateHair.Curve2 = 0.8f;
        }
        else if (gameObject.tag == "BigRoll")
        {
            CreateHair.Curve1 = 0.6f;
            CreateHair.Curve2 = 0.4f;
        }
        else if (gameObject.tag == "Straight_HairStyle") //直髮
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

    }
    public void FindPadETag() 
    {

        animator = GameObject.Find("Girl").GetComponent<Animator>();
        role = GameObject.Find("Girl");
        role.transform.position = new Vector3(0f, 0f, 0f);
        if (gameObject.tag == "Action1")
        {
            animator.SetBool("wave", true);
            animator.SetBool("look", false);
            animator.SetBool("dan", false);
        }
        else if (gameObject.tag == "Action2")
        {
            animator.SetBool("look", true);
            animator.SetBool("dan", false);
            animator.SetBool("wave", false);
        }
        else if (gameObject.tag == "Action3")
        {
            animator.SetBool("dan", true);
            animator.SetBool("wave", false);
            animator.SetBool("look", false);
        }
        else if (gameObject.tag == "Back") 
        {
            CallerPad.PadAShow();
            animator.SetBool("dan", false);
            animator.SetBool("wave", false);
            animator.SetBool("look", false);
            
            animator.SetTrigger("Pose2");
            role.transform.position = new Vector3(0.094f, -0.179f, 3.478f);
        }


    }
    public void FindAnimationTag() 
    {
        
        if (gameObject.tag == "closevedio") 
        {
            pointer = GameObject.Find("Player/SteamVRObjects/RightHand/PR_Pointer");
            pointer.SetActive(false);
            CallerPad.PadAShow();
        }


    }

    public void FindPadAObject()
    {

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
        Colorbtns[7] = Color.HSVToRGB(0, 0, 1.0f);
        Colorbtns[8] = Color.HSVToRGB(78.0f / 360.0f, 1.0f, 84.0f / 100.0f);
        Colorbtns[9] = Color.HSVToRGB(0, 49.0f / 100.0f, 77.0f / 100.0f);

        showimg = GameObject.FindGameObjectWithTag("showimage");
        slider1img = GameObject.FindGameObjectWithTag("slider1img");
        Gslider1 = GameObject.FindGameObjectWithTag("Gslider1");
        Wslider1 = GameObject.FindGameObjectWithTag("Wslider1");
        Tslider1 = GameObject.FindGameObjectWithTag("Tslider1");

        for (int i = 0; i < 10; i++)
        {
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
    public void FindPadBObject()
    {
        addbtns[0] = GameObject.FindGameObjectWithTag("ButtonA");
        addbtns[1] = GameObject.FindGameObjectWithTag("ButtonB");
        addbtns[2] = GameObject.FindGameObjectWithTag("ButtonC");
        addbtns[3] = GameObject.FindGameObjectWithTag("ButtonD");
        addbtns[4] = GameObject.FindGameObjectWithTag("ButtonE");
    }




}