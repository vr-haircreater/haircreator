using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSave : MonoBehaviour, IPointerDownHandler
{
    public Animator animator;
    public VRMSave SaveModel;
    public GameObject role;
    // Start is called before the first frame update

    public void OnPointerDown(PointerEventData eventData)
    {
        FindPadCTag();
    }

    public void FindPadCTag()
    {
        if (gameObject.tag == "PadCYes")
        {
          
            if (gameObject.GetComponent<VRMSave>() == null) SaveModel = gameObject.AddComponent<VRMSave>();
            else SaveModel = gameObject.GetComponent<VRMSave>();
            SaveModel.OnExportClicked();
            CallerPad.PadAShow();
            animator = GameObject.Find("Girl").GetComponent<Animator>();
            animator.SetTrigger("Pose2");
            role = GameObject.Find("Girl");
            role.transform.position = new Vector3(0.094f, -0.179f, 3.478f);

        }
        else if (gameObject.tag == "PadCNo")
        {
            CallerPad.PadAShow();
            animator = GameObject.Find("Girl").GetComponent<Animator>();
            animator.SetTrigger("Pose2");
            role = GameObject.Find("Girl");
            role.transform.position = new Vector3(0.094f, -0.179f, 3.478f);
        }

    }
}
