using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform m_refTarget;


    private void LateUpdate()
    {
        Vector3 vPos = m_refTarget.position;

        gameObject.transform.position = new Vector3(vPos.x, vPos.y, transform.position.z);
    }
}
