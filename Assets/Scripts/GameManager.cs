using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] panels;

    int nGame;
    public int unfixedSatellites;

    GameObject[] satellites;


    // Start is called before the first frame update
    void Start()
    {
        nGame = 0;

        foreach(GameObject go in panels)
        {
            go.SetActive(false);
        }

        satellites = GameObject.FindGameObjectsWithTag("Satellite");
        unfixedSatellites = satellites.Length;
        
    }

    public void Minigame(Satellite s)
    {
        nGame = Random.Range(0, panels.Length);
        panels[nGame].gameObject.SetActive(true);
        panels[nGame].gameObject.GetComponent<HammerScrewDriver>().setSatellite(s);

        Time.timeScale = 0f;

    }

    public void EndMinigame()
    {
        Debug.Log("Un satelite menos");
        if (unfixedSatellites > 0)
        {
            unfixedSatellites--;
        }
        if (unfixedSatellites == 0)
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
