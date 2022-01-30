using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] panels;

    int nGame;
    public int unfixedSatellites = 3;


    // Start is called before the first frame update
    void Start()
    {
        nGame = 0;

        foreach(GameObject go in panels)
        {
            go.SetActive(false);
        }
    }

    public void Minigame()
    {
        nGame = Random.Range(0, panels.Length);
        panels[nGame].gameObject.SetActive(true);

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
            Debug.Log("Se acabo");
            SceneManager.LoadScene("Credits");
        }
    }
}
