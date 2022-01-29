using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] panels;

    int nGame;
    bool fixEvent;

    // Start is called before the first frame update
    void Start()
    {
        nGame = 0;
        fixEvent = false;

        foreach(GameObject go in panels)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            fixEvent = true;
        }

        if (fixEvent)
        {
            Minigame();

            switch (nGame)
            {
                case 0:
                    fixEvent = panels[nGame].GetComponent<HammerScrewDriver>().getFinished();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
        else
        {
            Resume();
        }

    }

    void Resume()
    {
        panels[nGame].SetActive(false);
        Time.timeScale = 1.0f;
    }

    void Minigame()
    {
        nGame = Random.Range(0, panels.Length);
        //panels[nGame].gameObject.SetActive(true);

        Time.timeScale = 0f;

    }
}
