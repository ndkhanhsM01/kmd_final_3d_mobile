using System;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class DebugUtil
{

    [Conditional("UNITY_EDITOR")]
    public static void Log(object message)
    {
        Debug.Log(message);
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogError(object message)
    {
        Debug.LogError(message);
    }

    [Conditional("UNITY_EDITOR")]
    public static void DrawGizmoArc(Vector3 center, Vector3 forward, Vector3 normal, float radius, float degrees, int segments = 32)
    {
        float angleBetweenSegments = degrees / segments;
        Vector3 radiusVector = Quaternion.AngleAxis(-degrees/2f, normal) * forward;
        radiusVector *= radius;
        Vector3 previousCircumferencePoint = center + radiusVector;
        for (int i = 0; i < segments; ++i)
        {
            radiusVector = Quaternion.AngleAxis(angleBetweenSegments, normal) * radiusVector;
            Vector3 newCircumferencePoint = center + radiusVector;
            Gizmos.DrawLine(previousCircumferencePoint, newCircumferencePoint);
            previousCircumferencePoint = newCircumferencePoint;
        }
    }
}