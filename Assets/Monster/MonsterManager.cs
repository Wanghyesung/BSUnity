using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

    [SerializeField] private List<SOMonsterInfo> m_listMonsterInfo;
    [SerializeField] private List<Monster> m_listMonster;


    [SerializeField] private float m_fSpawnTime = 2.0f;

    private float m_fCurTime = 0.0f;
    private void Update()
    {
        m_fCurTime += Time.deltaTime;

        if(m_fCurTime >= m_fSpawnTime)
        {
            m_fCurTime = 0.0f;
            int idx = UnityEngine.Random.Range(0, m_listMonster.Count);
            Spawn(m_listMonster[idx]);
        }


    }

    private void Spawn(Monster _refMon)
    {
        float randomX = UnityEngine.Random.Range(0f, 1f);
        float randomY = (float)UnityEngine.Random.Range(-1, 2);
        Vector3 viewportPoint = new Vector3(randomX, randomY, Camera.main.nearClipPlane);

        // 2. Viewport 좌표를 World 좌표로 변환
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportPoint);


        Monster refMon = GameObject.Instantiate(_refMon, worldPos, Quaternion.identity);
        int idx = UnityEngine.Random.Range(0, m_listMonsterInfo.Count);
        refMon.Init(m_listMonsterInfo[idx]);

    }



}
