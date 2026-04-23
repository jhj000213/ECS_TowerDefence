using Unity.Mathematics;
using UnityEngine;

public class JDebugLogger
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(string message)
    {
        Debug.Log(message);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(string message, int value)
    {
        Debug.Log($"{message} : {value}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(string message, float value)
    {
        Debug.Log($"{message} : {value}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(string message, float3 value)
    {
        Debug.Log($"{message} : [{value.x}, {value.y}, {value.z}]");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(string message, float value)
    {
        Debug.LogError($"{message} : {value}");
    }
}
