using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonCtrl : MonoBehaviour
{
    Pause pause;
    public bool startOn = false;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.FindWithTag("MainCamera").GetComponent<Pause>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startClick()
    {
        startOn = true;
    }
    public void reStart()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.isPause = false;
        pause.startScene();
        SceneManager.LoadScene("Stage1");
    }
    public void conti()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.isPause = false;
        pause.startScene();
    }

    public void title()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.isPause = false;
        pause.startScene();
        SceneManager.LoadScene("Home");
    }

    public void quit()
    {
        Application.Quit();
    }
    public void mainquit()
    {
        Application.Quit();
    }

}
