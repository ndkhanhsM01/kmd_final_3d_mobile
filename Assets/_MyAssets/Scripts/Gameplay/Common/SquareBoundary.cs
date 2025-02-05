using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public struct SquareBoundary
{
    public Range horizontal;
    public Range vertical;

    public bool CheckInside(Vector3 position)
    {
        bool betweenHor = position.x > horizontal.min 
                        && position.x < horizontal.max;

        bool betweenVer = position.z > vertical.min
                        && position.z < vertical.max;

        return betweenHor && betweenVer;
    }

    public Vector3 GetRandomPosition(float defaultY = 0f)
    {
        float x = Random.Range(horizontal.min, horizontal.max);
        float z = Random.Range(vertical.min, vertical.max);

        return new Vector3(x, defaultY, z);
    }

#if UNITY_EDITOR
    public void DrawEditor(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(new Vector3(horizontal.min, 0f, vertical.max), new Vector3(horizontal.max, 0f, vertical.max));
        Gizmos.DrawLine(new Vector3(horizontal.min, 0f, vertical.max), new Vector3(horizontal.min, 0f, vertical.min));
        Gizmos.DrawLine(new Vector3(horizontal.max, 0f, vertical.min), new Vector3(horizontal.max, 0f, vertical.max));
        Gizmos.DrawLine(new Vector3(horizontal.max, 0f, vertical.min), new Vector3(horizontal.min, 0f, vertical.min));
    }
#endif
}