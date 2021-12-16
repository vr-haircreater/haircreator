using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEditor;
using System.Text;
using System.IO;
using VRM;
using VRMShaders;

public class CreateHair : MonoBehaviour
{
    int TriggerDown = 0;  //沒被按下
    int HairCounter = 0; //Hair片數
    public static int HairWidth = 1;//髮片寬度
    
    public static bool HairTail = true;

    float length = 0.015f; //點距離，原本0.05
    //public int InputRange = 3;//(寬度Range 1~6)
    //public int InputRangeThickness = 1; //(厚度Range 1~6)
    public static float Curve1 = 0.6f;
    public static float Curve2 = 0.6f;
    public static Vector3 NewPos, OldPos; //抓新舊點

    public static List<Vector3> PointPos = new List<Vector3>(); //Pos座標存取
    public static List<Vector3> UpdatePointPos = new List<Vector3>();//變形更新點座標
    public static List<Vector3> direction = new List<Vector3>();
    public List<GameObject> HairModel = new List<GameObject>(); //髮片Gameobj存取
    public List<GameObject> HairModelRig = new List<GameObject>();//髮片骨架

    public MeshGenerate MeshCreater; //呼叫 MeshGenerate.cs 中的東西給 MeshCreater 用
    public PosGenerate PosCreater; //呼叫 PosGenerate.cs 中的東西給 PosCreater 用

    public SteamVR_Action_Boolean TriggerClick = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");//板機鍵按鈕
    public static SteamVR_Behaviour_Pose Pose;//手把偵測與座標

    public SteamVR_Action_Boolean spawn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    public Texture HairTexture, HairNormal;
    public GameObject HairModelG,HairPos;

    //undo & redo
    public static List<GameObject> ListExistHair = new List<GameObject>(); //給Undo用
    public static Stack<GameObject> StackExistHair = new Stack<GameObject>(); //給Redo用
    public static GameObject PushObj, PopObj;
    public static GameObject ExistHair;
    public static int u_Freq = 0, c_Freq = 0;
    public static int TempListExistHair = 0;

    //頭髮動態
    public VRMSpringBone SpringDyanmic;
    VRMSpringBoneColliderGroup[] OBJCollider;
    SphereCollider Ecollder;

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
        HairModelG = GameObject.Find("Girl/Hairs");
        HairPos = GameObject.Find("Salon/Trolley/paint1/HairPoint");
        //HairPos = GameObject.Find("Player/SteamVRObjects/RightHand/Sphere");
        PosCreater = gameObject.AddComponent<PosGenerate>(); //加入PosGenerate
        //HairTexture = Resources.Load<Texture2D>("Textures/F00_000_Hair_00");
        //HairNormal = Resources.Load<Texture2D>("Textures/F00_000_Hair_00_nml");

        OBJCollider = new VRMSpringBoneColliderGroup[10];
        OBJCollider[0] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck/J_Bip_C_Head").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[1] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_L_Shoulder/J_Bip_L_UpperArm").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[2] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[3] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_L_Shoulder/J_Bip_L_UpperArm/J_Bip_L_LowerArm").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[4] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[5] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_L_Shoulder/J_Bip_L_UpperArm/J_Bip_L_LowerArm/J_Bip_L_Hand").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[6] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[7] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[8] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine").GetComponent<VRMSpringBoneColliderGroup>();
        OBJCollider[9] = GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck").GetComponent<VRMSpringBoneColliderGroup>();

        
    }

    void Update()
    {
        Dawer();                
    }
    void Dawer() 
    {

        if (TriggerDown == 0 && GatherControl.icon == 1) //沒被按下
        {
            if (TriggerClick.GetStateDown(Pose.inputSource)) //偵測被按下的瞬間
            {
                GameObject Model = new GameObject(); //創建model gameobj
                HairModel.Add(Model); //加入list
                Model.transform.SetParent(HairModelG.transform);//還需要判定到底是做男做女
                HairModel[HairCounter].name = "HairModel" + HairCounter; //設定名字 
                HairModel[HairCounter].tag = "Hairs";
                OldPos = NewPos = HairPos.transform.position;
                PosCreater.VectorCross(HairPos.transform.up, HairPos.transform.forward, HairPos.transform.right);
                PointPos.Add(OldPos);
                GameObject HairRig = new GameObject();
                HairRig.transform.SetParent(GameObject.Find("Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck/J_Bip_C_Head").transform);
                HairRig.transform.position = OldPos;
                HairModelRig.Add(HairRig);
                HairModelRig[HairCounter].name = "HairRig" + HairCounter;

                
                Ecollder = HairModel[HairCounter].AddComponent<SphereCollider>();
                Ecollder.center = OldPos;
                Ecollder.radius = 0.005f;


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
                if (ButtonTransitioner.HairStyleState == 1) PosCreater.Straight_HairStyle(PointPos, ButtonTransitioner.HairWidth, ButtonTransitioner.HairThickness, HairTail);
                if (ButtonTransitioner.HairStyleState == 2) PosCreater.WaveHairStyle(PointPos, ButtonTransitioner.HairWidth, ButtonTransitioner.HairThickness, Curve1, HairTail);
                if (ButtonTransitioner.HairStyleState == 3) PosCreater.TwistHairStyle(PointPos, ButtonTransitioner.HairWidth, Curve2, HairTail);
                
                Ecollder = HairModel[HairCounter].AddComponent<SphereCollider>();
                Ecollder.center = OldPos;
                Ecollder.radius = 0.005f;
                OldPos = NewPos;
            }

            if (PointPos.Count >= 2)
            {
                if (HairModel[HairCounter].GetComponent<MeshGenerate>() == null)
                    MeshCreater = HairModel[HairCounter].AddComponent<MeshGenerate>();
                else MeshCreater = HairModel[HairCounter].GetComponent<MeshGenerate>();
                MeshCreater.GenerateMesh(PointPos,UpdatePointPos, HairWidth, HairCounter);
                MeshGenerate.GethairColor.SetTexture("_MainTex", HairTexture);
                MeshGenerate.GethairColor.SetTexture("_BumpMap", HairNormal);
            }

            if (TriggerClick.GetStateUp(Pose.inputSource)) //放開
            {
                if (PointPos.Count >= 2)
                {
                    AddDymatic();
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
                    Destroy(HairModelRig[least]);
                    HairModelRig.RemoveAt(least);
                }
                PointPos.Clear();
                direction.Clear();
                TriggerDown = 0;

            }
        }
        
    }

    private void AddDymatic()
    {
        GameObject Spring = GameObject.Find("Girl/secondary");
        SpringDyanmic = Spring.AddComponent<VRMSpringBone>();
        SpringDyanmic.m_center = GameObject.Find("Girl/Root").transform;
        SpringDyanmic.RootBones.Add(GameObject.Find($"Girl/Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_C_Neck/J_Bip_C_Head/HairRig{HairCounter}").transform);
        SpringDyanmic.ColliderGroups = OBJCollider;
        SpringDyanmic.m_dragForce = 0.4f;
        SpringDyanmic.m_stiffnessForce = 0.4f;
        SpringDyanmic.m_hitRadius = 0.01f;
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

    public static void Clear() //state == 3;
    {
        //if clear excuted, rerecord the undo count. (till next clear)
        u_Freq = 0; // Undo count return to zero.
        StackExistHair.Clear();
        TempListExistHair = ListExistHair.Count;
        for (int i = 0; i < TempListExistHair; i++)  //All in.
        {
            PushStuff();
        }
        c_Freq = 1; //clear functions had been excuted. (for undo)
        //Debug.Log("uF:" + u_Freq + "cF:" + c_Freq);  
    }

    public static void Eraser() //Gather1.state ==2;
    {
        if (EraserCollider.ForContact==1)
        {
            PushObj = Instantiate(EraserCollider.Contact);
            PushObj.tag = "Hairs";
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
        PushObj.tag = "Hairs";
        StackExistHair.Push(PushObj); //生成的物件(pushobj)push進stack存，之後要redo要用
        PushObj.SetActive(false); //場景上不再看得見
        Destroy(ListExistHair[ListExistHair.Count - 1]); //刪除ListExitstHair中count-1的物件
        ListExistHair.RemoveAt(ListExistHair.Count - 1); //從ListExistHair中移除count-1的物件
    }

    public static void PopStuff()
    {
        PopObj = StackExistHair.Pop(); //從stack中pop東西出來
        ListExistHair.Add(PopObj); //加回ListExitstsHair中
        PopObj.tag = "Hairs";
        PopObj.SetActive(true); //場景上要看得見
    }

}
