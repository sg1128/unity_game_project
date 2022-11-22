using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Pause : MonoBehaviour
{
    bool isPause = false;
    public GameObject panel;
    public AudioCtrl audioctrl;
    public GameObject setting1;
    public GameObject setting2;
    public GameObject setting3;
    public GameObject setting4;
    public GameObject setting5;
    public Lose lose;
    public win win;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (lose.escStop == false && win.escStop == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause)
                {
                    stopScene();
                }
                else
                {
                    startScene();
                }
            }
        }
    }

    void stopScene()
    {
        Time.timeScale = 0;
        isPause = true;
        panel.SetActive(true);
        return;
    }
    public void startScene()
    {
        Time.timeScale = 1;
        isPause = false;
        panel.transform.GetChild(0).gameObject.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(false);
        setting1.GetComponent<PauseUI>().info.SetActive(false);
        setting2.GetComponent<PauseUI>().info.SetActive(false);
        setting3.GetComponent<PauseUI>().info.SetActive(false);
        setting4.GetComponent<PauseUI>().info.SetActive(false);
        setting5.GetComponent<PauseUI>().info.SetActive(false);
        panel.SetActive(false);
        return;
    }
}