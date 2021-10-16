using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSave : MonoBehaviour, IPointerDownHandler
{
    public Animator animator;
    public VRMSave SaveModel;
    public GameObject role,rolehair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        FindPadCTag();
    }

    public void FindPadCTag()
    {
        //===== PadC-yes/no (Save等宜均加存檔部分)
        if (gameObject.tag == "PadCYes")
        {
          
            if (gameObject.GetComponent<VRMSave>() == null) SaveModel = gameObject.AddComponent<VRMSave>();
            else SaveModel = gameObject.GetComponent<VRMSave>();
            SaveModel.OnExportClicked();
            CallerPad.PadAShow();
            animator = GameObject.Find("GirlSit").GetComponent<Animator>();
            animator.SetTrigger("Pose2");
            role = GameObject.Find("GirlSit");
            role.transform.position = new Vector3(0.094f, -0.204f, 3.39f);
            rolehair = GameObject.Find("GirlSit/Hairs");
            rolehair.transform.localPosition = new Vector3(0f,0f,0f);
        }
        else if (gameObject.tag == "PadCNo")
        {
            CallerPad.PadAShow();
            animator = GameObject.Find("GirlSit").GetComponent<Animator>();
            animator.SetTrigger("Pose2");
            role = GameObject.Find("GirlSit");
            role.transform.position = new Vector3(0.094f, -0.204f, 3.39f);
            rolehair = GameObject.Find("GirlSit/Hairs");
            rolehair.transform.localPosition = new Vector3(0f, 0f, 0f);
        }

    }
}
