using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImgControl : MonoBehaviour {

    CanvasGroup canvasGroup;

    public float invokeTime;
    public float startTime;
    public float ImgScale;
    private void Awake()
    {
       
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Use this for initialization
    void Start () {
       
        canvasGroup.alpha = 0;
        InvokeRepeating("SetScale", startTime, invokeTime);

    }
	// Update is called once per frame
	void Update ()
    {

	}
    void SetScale()
    {
        Tween tween = canvasGroup.DOFade(1, 2);
        tween.onComplete = (() => {

            canvasGroup.DOFade(0.1f, 2);
        });

    }
}
