using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ImageSlider : MonoBehaviour {

    public Sprite[] m_Sprites;

    GameObject[] objs;

    int index;

    enum State:int
    {
        ONCE,
        PLAY,
    }

    State runSTATE = State.ONCE;

    int length;
	void Start () {
       
        length = m_Sprites.Length;
        objs = new GameObject[length];
        runSTATE = State.PLAY;
        index = 0;
        for (int i = 0; i < length; i++)
        {
            GameObject obj = new GameObject();
            obj.name = m_Sprites[i].name;
            objs[i] = obj;
            obj.transform.SetParent(this.transform);
            obj.transform.localPosition = new Vector3(i * 1015, 0, 0);
            Image img = obj.AddComponent<Image>();
            img.transform.localScale = new Vector3(1, 1, 1);
            img.sprite = m_Sprites[i];
            img.SetNativeSize();
        }

	}
    void OnEnable()
    {
        if (runSTATE == State.ONCE)
            return;
        for (int i = 0; i < length; i++)
        {
            objs[i].transform.localPosition = new Vector3(i * 1015, 0, 0);
        }
        index = 0;
    }
    

    public Button btn_Left;
    public Button btn_Right;
   
    public void LeftClick()
    {
        
        if (index == 0)
            return;
        btn_Left.interactable = false;
        index -= 1;
        for (int i = 0; i < length; i++)
        {
            Vector3 vec3 = objs[i].transform.localPosition;
            Tween tween = objs[i].transform.DOLocalMove(vec3 + new Vector3(1015, 0, 0), 0.5f);
            tween.onComplete = (() => {
                
                btn_Left.interactable = true;
               

            });
        }
    }

    public void RightClick()
    {
        if (index == length - 1)
            return;
        btn_Right.interactable = false;
        index += 1;
        for (int i = 0; i < length; i++)
        {
            Vector3 vec3 = objs[i].transform.localPosition;
            Tween tween = objs[i].transform.DOLocalMove(vec3 - new Vector3(1015, 0, 0), 0.5f);
            tween.onComplete = (() => {

                btn_Right.interactable = true;

            });
        }
    }
}
