using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : AttackObj
{
    private Rigidbody2D m_refRigi;
    [SerializeField] private float topPadding = 1.1f;
    private Vector2 m_vDir;

    private Vector2 m_vGoalpos;

    [SerializeField] private float m_fSpeed = 7.0f;
    private void Awake()
    {
        m_refRigi = GetComponent<Rigidbody2D>();
    }

    public override void Init(int iDamage)
    {
        base.Init(iDamage);

        m_vGoalpos = transform.position;

        // 1. Viewport 좌표 생성 (X: 0~1 랜덤, Y: 화면 위 밖, Z: Camera Near Clip)
        float randomX = Random.Range(0f, 1f);
        Vector3 viewportPoint = new Vector3(randomX, topPadding, Camera.main.nearClipPlane);

        // 2. Viewport 좌표를 World 좌표로 변환
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportPoint);

        worldPos.z = 0f;

        transform.position = worldPos;

        m_vDir = m_vGoalpos - new Vector2(worldPos.x, worldPos.y);
        m_vDir = m_vDir.normalized;
    }

    public override void Update()
    {
        base.Update();

        float fLen = (m_vGoalpos - m_refRigi.position).magnitude;
        if(fLen <= 1.0f)
            Dead();
    }

    private void FixedUpdate()
    {
        Vector2 vPos = m_refRigi.position;
        vPos += (m_vDir * m_fSpeed * Time.fixedDeltaTime);
        m_refRigi.MovePosition(vPos);
    }
}
