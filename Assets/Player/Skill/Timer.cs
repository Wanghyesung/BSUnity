using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;

    [SerializeField] private TextMeshProUGUI timeText;
    private void Update()
    {
        time += Time.deltaTime;

        int sec = (int)(time % 60);
        int min = (int)(time / 60);

        string strsec = "";
        if (sec == 0)
            strsec = "0";
        else
            strsec = sec.ToString();

        string strmin = "";
        if (min == 0)
            strmin = "0";
        else
            strmin = min.ToString();

        timeText.text = $"{strmin} : {strsec}";
    }

    public void StopAndAction()
    {
        //Skill선택시 무시
        if(Time.timeScale <= 0.0f)
            Time.timeScale = 1.0f;
        else
            Time.timeScale = 0.0f;
    }
    
}
