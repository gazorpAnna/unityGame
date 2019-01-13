using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	
	public int  Minutes = 0;
	public int  Seconds = 0;
	public GameObject[] enemy;
	
	private Text    m_text;
	private float   m_leftTime;

	private void Start()
	{
        enemy = GameObject.FindGameObjectsWithTag("Police");
        for (int i = 0; i < enemy.Length; i++)
        {

            enemy[i].SetActive(false);
        }
        m_text = GetComponent<Text>();
		m_leftTime = GetInitialTime();
	}

	private void Update()
	{
		if (m_leftTime > 0f)
		{
			//  Update countdown clock
			m_leftTime -= Time.deltaTime;
			Minutes = GetLeftMinutes();
			Seconds = GetLeftSeconds();

			//  Show current clock
			if (m_leftTime > 0f)
			{
				m_text.text =  Minutes.ToString ("00") + ":" + Seconds.ToString("00");
			}
			else
			{
				//  The countdown clock has finished
				m_text.text = "STOP 00:00";
			}
			if (Minutes == 0 && Seconds == 0) {
               

                for (int i = 0; i < enemy.Length; i++) {
					
					enemy [i].SetActive (true);
				}

			
			}
           

		}
	}

	private float GetInitialTime()
	{
		return Minutes * 60f + Seconds;
	}

	private int GetLeftMinutes()
	{
		return Mathf.FloorToInt(m_leftTime / 60f);
	}

	private int GetLeftSeconds()
	{
		return Mathf.FloorToInt(m_leftTime % 60f);
	}
}
