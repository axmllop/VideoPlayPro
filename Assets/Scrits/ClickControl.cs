using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickControl : MonoBehaviour {
    int cache = 0;

    public GameObject[] pages;

    public Button[] btns;

    void OnEnable()
    {
        OnClick(cache);
    }
    public void OnClick(int index)
    {
        if (index!=cache)
        {
            pages[cache].SetActive(false);
            btns[cache].interactable = true;
        }

        pages[index].SetActive(true);
        btns[index].interactable = false;
        cache = index;
    }

    void OnDisable()
    {
        cache = 0;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            btns[i].interactable = true;
        }
    }
   
}
