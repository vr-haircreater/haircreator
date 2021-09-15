using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChange : MonoBehaviour
{
  
    public static void Change()
    {
        UIManager.Instance.ShowPanel("PadB");
        UIManager.Instance.ClosePanel("PadC");
    }

    
}
