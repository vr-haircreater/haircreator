using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    //public GameObject dot1,dot2;
    public VRInputModule m_InputModule;
    public static Color PointerColor;
    public static Color white, red;
    public static Material nowmaterial,m_red,m_white;

    private LineRenderer m_LineRenderer = null;
    // Start is called before the first frame update
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        white = Color.white;
        red = Color.red;
        //dot1 = GameObject.Find("Player/RightHand/PR_Pointer/Dot_red");
        //dot2 = GameObject.Find("Player/RightHand/PR_Pointer/Dot_white");
        m_red = new Material(Shader.Find("Unlit/Color"));
        m_red.color=Color.red;
        m_white = new Material(Shader.Find("Unlit/Color"));
        m_white.color=Color.white;
        nowmaterial = m_Dot.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
        if (CallerPad.state == 1)
        {
            m_LineRenderer.SetColors(white, white);
            /*dot2.SetActive(true);
            dot1.SetActive(false);
            m_Dot = dot2;*/
            nowmaterial = m_white;
        }
        else
        {
            m_LineRenderer.SetColors(white, red);
            /*dot2.SetActive(false);
            dot1.SetActive(true);
            m_Dot = dot1;*/
            nowmaterial = m_red;
        }
        
    }
    private void UpdateLine()
    {
        //use default or distance
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);
        
        //Or based on hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }
        
        //Set position of the dot
        m_Dot.transform.position = endPosition;
        
        //Set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
