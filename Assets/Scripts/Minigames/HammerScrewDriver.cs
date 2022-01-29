using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerScrewDriver : MonoBehaviour
{
    public Slider bar;
    public Image action;
    public Sprite [] allActions;

    bool hammer;
    bool screwdriver;
    bool fixing;

    // Start is called before the first frame update
    void Start()
    {
        action.sprite = allActions[0];
        bar.value = 0;

        hammer = false;
        screwdriver = false;
        fixing = true;

        Time.timeScale = 0f; //Luego se ha de papar desde GameManager, no desde aqui
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && (screwdriver || (!screwdriver && !hammer)))
        {
            screwdriver = false;

            action.sprite = allActions[1];
            bar.value += 0.2f;

            hammer = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (hammer || (!screwdriver && !hammer)))
        {
            hammer = false;

            action.sprite = allActions[2];
            bar.value += 0.1f;

            screwdriver = true;
        }
        else
        {
            hammer = false;
            screwdriver = false;

            action.sprite = allActions[0];
        }

        if (bar.value > 0) bar.value -= 0.001f;

        if (bar.value == 1) fixing = false;

    }

    public bool getFinished()
    {
        return fixing;
    }
}
