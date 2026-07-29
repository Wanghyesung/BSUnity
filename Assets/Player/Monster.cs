using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] private Rigidbody2D m_refRigid;
    [SerializeField] private float m_fSpeed = 2.0f;

    [SerializeField] private float m_fAttackLen = 8.0f;

    private float m_fCurLen = 0.0f;
    private Vector2 m_vDir;

    private void Awake()
    {
        m_refRigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        var Target = Player.MainPlayer;

        Vector3 vTargetPos =  Target.transform.position;
        Vector2 vTarget2D = new Vector2(vTargetPos.x, vTargetPos.y);

        Vector2 diff = m_refRigid.position - vTarget2D;
        m_fCurLen = diff.magnitude;


        m_vDir = diff.normalized;
        Vector2 vPos = m_refRigid.position;

        m_refRigid.MovePosition(vPos + (m_vDir * m_fSpeed));
    }


    private bool CheckLen()
    {
        
        if(m_fCurLen <= m_fAttackLen)
        {
            //Attack;
            //Player.MainPlayer.
        }

        return false;
    }




}
