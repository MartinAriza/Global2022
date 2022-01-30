using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Satellite : MonoBehaviour
{
    float time = 0.0f;
    public float maxTime = 4.0f;
    bool touching = false;
    bool isFixed = false;

    public Slider bar;
    public GameObject panel;

    [SerializeField] UnityEvent collision;
    

    // Start is called before the first frame update
    void Start()
    {
        if (collision == null)
            collision = new UnityEvent();

        bar.value = 0;
        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (touching && !isFixed)
        {
            panel.SetActive(true);
            time += Time.deltaTime;
            bar.value = time;
        }
        else
        {
            time = 0.0f;
        }

        if (time >= maxTime)
        {
            panel.SetActive(false);
            collision.Invoke();
            time = 0.0f;
            touching = false;
            isFixed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Ship")
        {
            touching = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Ship")
        {
            touching = false;

        }
    }
}
