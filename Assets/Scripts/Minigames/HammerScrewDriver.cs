using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HammerScrewDriver : MonoBehaviour
{
    public Slider bar;
    public Image action;
    public Sprite [] allActions;

    bool hammer;
    bool screwdriver;

    Satellite satellite;

    [SerializeField] UnityEvent minigameFinished;

    //Sound
    [SerializeField] AudioSource hammerSound;
    [SerializeField] AudioSource victorySound;

    // Start is called before the first frame update
    void Start()
    {
        if (minigameFinished == null)
            minigameFinished = new UnityEvent();

        if (allActions.Length > 0)
            action.sprite = allActions[0];
        bar.value = 0;

        hammer = false;
        screwdriver = false;

        Time.timeScale = 0f; //Luego se ha de parar desde GameManager, no desde aqui
    }

    // Update is called once per frame
    void Update()
    {

        if (bar.value >= 0.9f)
        {
            victorySound.PlayOneShot(victorySound.clip);

            satellite.fixedSatellite();
            minigameFinished.Invoke();
            StopCoroutine(ChangeSprite());
            if (allActions.Length > 0)
                action.sprite = allActions[0];
            bar.value = 0;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            
        }
        

        if (Input.GetKeyDown(KeyCode.S) && (screwdriver || (!screwdriver && !hammer)))
        {
            screwdriver = false;

            hammerSound.PlayOneShot(hammerSound.clip);

            if (allActions.Length > 0)
                action.sprite = allActions[1];

            bar.value += 0.05f;

            hammer = true;

            StopCoroutine(ChangeSprite());
            if(gameObject.activeSelf)
                StartCoroutine(ChangeSprite());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && (hammer || (!screwdriver && !hammer)))
        {
            hammer = false;

            hammerSound.PlayOneShot(hammerSound.clip);

            if (allActions.Length > 0)
                action.sprite = allActions[2];

            bar.value += 0.05f;

            screwdriver = true;

            StopCoroutine(ChangeSprite());
            if (gameObject.activeSelf)
                StartCoroutine(ChangeSprite());
        }
        

        if (bar.value > 0) bar.value -= 0.001f;

    }

    IEnumerator ChangeSprite()
    {
        
        yield return new WaitForSecondsRealtime(0.1f);

        if (allActions.Length > 0)
            action.sprite = allActions[0];

    }

    public void setSatellite(Satellite s)
    {
        satellite = s;
    }

}
