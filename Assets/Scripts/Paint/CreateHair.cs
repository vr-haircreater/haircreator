using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class CreateHair : MonoBehaviour
{
    int TriggerDown = 0;  //沒被按下
    int HairCounter = 0; //Hair片數
    public static int HairWidth = 1;//髮片寬度
    
    public bool HairTail = true;

    float length = 0.015f; //點距離，原本0.05
    //public int InputRange = 3;//(寬度Range 1~6)
    //public int InputRangeThickness = 1; //(厚度Range 1~6)
    public float TwistCurve = 0.6f;
    public float WaveCurve = 1.0f;

    public static Vector3 NewPos, OldPos; //抓新舊點

    public static List<Vector3> PointPos = new List<Vector3>(); //Pos座標存取
    public static List<Vector3> UpdatePointPos = new List<Vector3>();//變形更新點座標
    public static List<Vector3> direction = new List<Vector3>();
    public List<GameObject> HairModel = new List<GameObject>(); //髮片Gameobj存取

    public MeshGenerate MeshCreater; //呼叫 MeshGenerate.cs 中的東西給 MeshCreater 用
    public PosGenerate PosCreater; //呼叫 PosGenerate.cs 中的東西給 PosCreater 用

    public SteamVR_Action_Boolean TriggerClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");//板機鍵按鈕
    public SteamVR_Action_Boolean LClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnRight");//left按鈕
    public SteamVR_Action_Boolean RClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnLeft");//right按鈕
    public static SteamVR_Behaviour_Pose Pose;//手把偵測與座標

    public SteamVR_Action_Boolean spawn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    public Texture HairTexture, HairNormal;
    public GameObject HairModelG,HairModelB,HairPos;
    float AimHairG;

    //undo & redo
    public static List<GameObject> ListExistHair = new List<GameObject>(); //給Undo用
    public static Stack<GameObject> StackExistHair = new Stack<GameObject>(); //給Redo用
    public static GameObject PushObj, PopObj;
    public static GameObject ExistHair;
    public static int u_Freq = 0, c_Freq = 0;
    public static int TempListExistHair = 0;

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
        HairModelG = GameObject.Find("GirlSit/Hairs");
        HairModelB = GameObject.Find("BoySit/Hairs");
        HairPos = GameObject.Find("Salon/Trolley/Salon_tool/paint1/pCylinder6ylinder6");
        PosCreater = gameObject.AddComponent<PosGenerate>(); //加入PosGenerate
        HairTexture = Resources.Load<Texture2D>("Textures/F00_000_Hair_00");
        HairNormal = Resources.Load<Texture2D>("Textures/F00_000_Hair_00_nml");
    }

    void Update()
    {
        Dawer();
        //Undo();
        //Redo();
        //Clear();
        //Eraser();
        Control();
        AimHairG = Vector3.Distance(HairModelG.transform.position, Pose.transform.position);
                
    }
    void Dawer() 
    {
        if (TriggerDown == 0 && Gather1.icon == 1) //沒被按下
        {
            if (TriggerClick.GetStateDown(Pose.inputSource)) //偵測被按下的瞬間
            {
                Debug.Log("Aim:" + AimHairG);
                GameObject Model = new GameObject(); //創建model gameobj
                HairModel.Add(Model); //加入list
                Model.transform.SetParent(HairModelG.transform);//還需要判定到底是做男做女
                HairModel[HairCounter].name = "HairModel" + HairCounter; //設定名字 
                OldPos = NewPos = HairPos.transform.position;
                PosCreater.VectorCross(HairPos.transform.up, HairPos.transform.forward, HairPos.transform.right);
                PointPos.Add(OldPos);

                Debug.Log("HairStytle" + ButtonTransitioner.HairStyleState);

                TriggerDown = 1;
            }
        }
        if (TriggerDown == 1) //被按下
        {
            NewPos = HairPos.transform.position;
            float dist = Vector3.Distance(OldPos, NewPos); //計算舊點到新點，位置的距離
            if (dist > length) //距離大於設定的長度
            {
                //正規化
                Vector3 NormaizelVec = NewPos - OldPos;
                NormaizelVec = Vector3.Normalize(NormaizelVec);
                NormaizelVec = new Vector3(NormaizelVec.x * length, NormaizelVec.y * length, NormaizelVec.z * length);
                NewPos = NormaizelVec + OldPos;
                PointPos.Add(NewPos);
                PosCreater = gameObject.GetComponent<PosGenerate>(); //加入PosGenerate
                PosCreater.VectorCross(HairPos.transform.up, HairPos.transform.forward, HairPos.transform.right);
                //PosCreater.GetPosition(OldPos, NewPos, InputRange);
                if (ButtonTransitioner.HairStyleState == 1) PosCreater.Straight_HairStyle(PointPos, ButtonTransitioner.HairWidth, ButtonTransitioner.HairThickness, HairTail);
                if (ButtonTransitioner.HairStyleState == 2) PosCreater.WaveHairStyle(PointPos, ButtonTransitioner.HairWidth, ButtonTransitioner.HairThickness, WaveCurve, HairTail);
                if (ButtonTransitioner.HairStyleState == 3) PosCreater.TwistHairStyle(PointPos, ButtonTransitioner.HairWidth, TwistCurve, HairTail);
                OldPos = NewPos;
            }

            if (PointPos.Count >= 2)
            {
                if (HairModel[HairCounter].GetComponent<MeshGenerate>() == null)
                    MeshCreater = HairModel[HairCounter].AddComponent<MeshGenerate>();
                else MeshCreater = HairModel[HairCounter].GetComponent<MeshGenerate>();
                MeshCreater.GenerateMesh(UpdatePointPos, HairWidth);
                MeshGenerate.GethairColor.SetTexture("_MainTex", HairTexture);
                MeshGenerate.GethairColor.SetTexture("_BumpMap", HairNormal);
            }

            if (TriggerClick.GetStateUp(Pose.inputSource)) //放開
            {
                if (PointPos.Count >= 2)
                {
                    HairCounter++;
                    ExistHair = GameObject.Find("HairModel" + (HairCounter - 1)); //找到相應的hairmodel名稱丟給 ExistHair GameObj
                    ListExistHair.Add(ExistHair);
                    c_Freq = 0;
                }
                else
                {
                    //清除不夠長所以沒加到程式碼的的髮片gameobj
                    int least = HairModel.Count - 1;
                    Destroy(HairModel[least]);
                    HairModel.RemoveAt(least);
                }
                PointPos.Clear();
                direction.Clear();
                Gather1.GridState = false;
                TriggerDown = 0;

                /*for(int i = 0; i < HairCounter; i++)
                {
                    ListExistHair.Add(HairModel[i]);
                }*/
            }
        }
        
    }
    void Control()
    {

        if (Input.GetKeyDown("s") && WaveCurve > 0.2f) WaveCurve -= 0.1f;
        if (Input.GetKeyDown("w") && WaveCurve < 0.8f) WaveCurve += 0.1f;
        if (Input.GetKeyDown("a") && TwistCurve > 0.5f) TwistCurve -= 0.1f;
        if (Input.GetKeyDown("d") && TwistCurve < 0.8f) TwistCurve += 0.1f;//越大越捲

        if (Gather1.icon == 2) 
        { 
        
        }
        if (Gather1.icon == 3) 
        {
        
        }

    }

    public static void Undo()
    {
        
        if (c_Freq == 0)
        {
            u_Freq += 1;
            PushStuff();
        }
        if(c_Freq == 1)
        {
            u_Freq = 1; // after clear function excuted, undo can be excuted once.
            for (int i = 0; i < TempListExistHair; i++)
            {
                PopStuff();
            }
        }

    }
    public static void Redo()
    {
        //redo need to be excuted after undo, but not after clear function.
        
        if (u_Freq != 0 && c_Freq == 0)
        {
            PopStuff();
            u_Freq -= 1;
        }
        if (u_Freq != 0 && c_Freq == 1)
        {
            for (int i = 0; i < TempListExistHair; i++)
            {
                PushStuff();
            }
            u_Freq = 0;
        }
    }

    public static void Clear()
    {
        //if clear excuted, rerecord the undo count. (till next clear)
        if (Input.GetKeyDown("c"))
        {
            u_Freq = 0; // Undo count return to zero.
            StackExistHair.Clear();
            TempListExistHair = ListExistHair.Count;
            for (int i = 0; i < TempListExistHair; i++)  //All in.
            {
                PushStuff();
            }
            c_Freq = 1; //clear functions had been excuted. (for undo)
            Debug.Log("uF:" + u_Freq + "cF:" + c_Freq);
        }
    }

    public static void Eraser()
    {
        if (EraserCollider.Contact != null)
        {
            PushObj = Instantiate(EraserCollider.Contact);
            StackExistHair.Push(PushObj);
            EraserCollider.Contact.SetActive(false);
            Destroy(EraserCollider.Contact);
        }
        
    }

    public static void ResetPos()
    {
        OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }

    public static void PushStuff() //push staff into 
    {
        PushObj = Instantiate(ListExistHair[ListExistHair.Count - 1]); //生成ListExitstHair中count-1的物件
        StackExistHair.Push(PushObj); //生成的物件(pushobj)push進stack存，之後要redo要用
        PushObj.SetActive(false); //場景上不再看得見
        Destroy(ListExistHair[ListExistHair.Count - 1]); //刪除ListExitstHair中count-1的物件
        ListExistHair.RemoveAt(ListExistHair.Count - 1); //從ListExistHair中移除count-1的物件
    }

    public static void PopStuff()
    {
        PopObj = StackExistHair.Pop(); //從stack中pop東西出來
        ListExistHair.Add(PopObj); //加回ListExitstsHair中
        PopObj.SetActive(true); //場景上要看得見
    }

}
