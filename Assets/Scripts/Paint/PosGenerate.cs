using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosGenerate : MonoBehaviour
{
    public List<Vector3> GetUpdatePointPos = CreateHair.UpdatePointPos;
    public List<Vector3> GetPointPos = CreateHair.PointPos; //拿CreateHair的PointPos來用
    List<Vector3> TempPoint = new List<Vector3>();
    public int HairWidth = CreateHair.HairWidth;
    public Vector3 cross1, cross2;
    public List<Vector3> directionA = CreateHair.direction;
    public void VectorCross(Vector3 up, Vector3 forward, Vector3 right)
    {
        cross1 = Vector3.Cross(up, forward);//x
        cross1.Normalize();
        cross2 = Vector3.Cross(up, right);//z
        cross2.Normalize();
        directionA.Add(cross1);
        directionA.Add(cross2);
    }

    public void Straight_HairStyle(List<Vector3> GetPointPos, int range, int thickness,bool HairTail)
    {
        float w = range * 0.005f * 0.2f;
        float t = thickness * 0.001f;
        float w1 = range * 0.005f / (GetPointPos.Count / 2);

        TempPoint.Clear();
        for (int i = 0, n = 0; i < GetPointPos.Count; i++, n+=2)
        {
            TempPoint.Add(GetPointPos[i] - directionA[n] * w);
            TempPoint.Add(GetPointPos[i] + directionA[n + 1] * t);
            TempPoint.Add(GetPointPos[i] + directionA[n] * w);
            TempPoint.Add(GetPointPos[i] - directionA[n + 1] * t);
            if (w < range * 0.005f && i < GetPointPos.Count / 2) w += w1;
            if (i > GetPointPos.Count / 2 && HairTail == true) w -= w1;
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

    public void WaveHairStyle(List<Vector3> GetPointPos, int range, int thickness, float WaveCurve, bool HairTail)
    {
        TempPoint.Clear();
        float w1 = range * 0.005f / (GetPointPos.Count / 2);
        float w = range * 0.005f * 0.2f;
        float t = thickness * 0.002f;
        float waveSize = 0.001f;
        float angle = -Mathf.PI;

        for (int i = 0, n = 0; i < GetPointPos.Count; i++, n+=2)
        {
            float y = -Mathf.Sin(angle);//正負的影響
            if (i == 0)
            {
                Vector3 temp = directionA[n+1] * waveSize * y;
                Vector3 Vec = GetPointPos[i] + temp;
                for (int j = 0; j < 4; j++) TempPoint.Add(Vec);
            }
            else
            {
                Vector3 temp = directionA[n+1] * waveSize * y;
                Vector3 Vec = GetPointPos[i] + temp;
                TempPoint.Add(Vec - directionA[n] * w);
                TempPoint.Add(Vec + directionA[n + 1] * t);
                TempPoint.Add(Vec + directionA[n] * w);
                TempPoint.Add(Vec - directionA[n + 1] * t);
            }
            //if (w < range * 0.005f) w += w1;
            if (w < range * 0.005f && i < GetPointPos.Count / 2) w += w1;
            else if (i > GetPointPos.Count / 2) w -= w1;
            if (waveSize < 0.02f && i % 7 == 0 && i < GetPointPos.Count/2) waveSize += 0.005f;
            if (waveSize < 0.02f && i % 7 == 0 && i > GetPointPos.Count / 2) waveSize -= 0.005f;
            //if (i > GetPointPos.Count - 5) waveSize = 0.01f;
            angle += WaveCurve;//0.9f
        }

        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

    public void TwistHairStyle(List<Vector3> GetPointPos, int range, float TwistCurve,bool HairTail)
    {
        TempPoint.Clear();
        float w1 = range * 0.005f / (GetPointPos.Count / 2);
        float w = range * 0.005f * 0.2f;
        float d = Mathf.PI;
        float a = 0.01f;

        for (int i = 0, n = 0; i < GetPointPos.Count; i++, n+=2)
        {
            float x = a * Mathf.Sin(d);
            float y = a * Mathf.Cos(d);
           
            Vector3 temp1 = directionA[n] * x, temp2 = directionA[n + 1] * y;
            Vector3 Vec = GetPointPos[i] + temp1 + temp2;
            
            TempPoint.Add(Vec - directionA[n] * w);
            TempPoint.Add(Vec + directionA[n + 1] * w);
            TempPoint.Add(Vec + directionA[n] * w);
            TempPoint.Add(Vec - directionA[n + 1] * w);

            d += TwistCurve;//原:0.5f
            if (a < 0.05f && i % (10 * TwistCurve) == 0 && i < GetPointPos.Count/2) a += 0.01f;
            if (i % (10* TwistCurve) == 0 && i > GetPointPos.Count / 2) a -= 0.01f;
            if (w < range * 0.005f && i < GetPointPos.Count / 2) w += w1;
            if (i > GetPointPos.Count / 2 && HairTail == true) w -= w1;
       
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

}

