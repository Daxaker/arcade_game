using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponHandles : MonoBehaviour
{
    [FormerlySerializedAs("leftHandle")] 
    public Vector3 leftHandleOrigin;

    [FormerlySerializedAs("rightHandle")] 
    public Vector3 rightHandleOrigin;

    public Vector3 GetLeftHandle()
    {
        return transform.rotation * leftHandleOrigin + transform.position;
    }
    
    public Vector3 GetRightHandle()
    {
        return transform.rotation * rightHandleOrigin + transform.position;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(GetLeftHandle(), 0.02f);
        Gizmos.DrawSphere(GetRightHandle(), 0.02f);
    }
}
