using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	
	public int  Minutes = 0;
	public int  Seconds = 0;
	public GameObject[] enemy;
	public GameObject god;
	
	private Text    m_text;
	private float   m_leftTime;
    bool fin;

	private void Start()
	{
        fin = false;
        enemy = GameObject.FindGameObjectsWithTag("Police");
		god = GameObject.FindGameObjectWithTag ("Pathfinding");
		god.SetActive(false); 
        foreach(GameObject g in enemy)
        {
            g.SetActive(false);
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
			if (Minutes == 0 && Seconds == 0 && !fin) {
                fin = true;
				god.SetActive (true); 
                foreach (GameObject g in enemy)
                {
                    g.SetActive(true);
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
