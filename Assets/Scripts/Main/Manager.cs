using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Xml;
using RenderHeads.Media.AVProVideo;

public class Manager : MonoBehaviour
{
    public Transform centerPos;
    [SerializeField]
    List<Button> btnList = new List<Button>();
    List<FlowUnitMove> unitMoves = new List<FlowUnitMove>();
    [SerializeField]
    List<Image> imgList = new List<Image>();
    public Transform bg;

    UdPServer udPServer;
    GameObject peoplePanel;
    GameObject showContent;

    MediaPlayer player;

    float backTimer;
    private void Awake()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(Application.streamingAssetsPath + @"/config.xml");
        XmlNodeList tempTime = xml.GetElementsByTagName("Time");
        backTimer =float.Parse(tempTime[0].InnerText);
        udPServer = GameObject.Find("UdpServer").GetComponent<UdPServer>();
    }
    private void Start()
    {
        peoplePanel = GameObject.Find("PeoplePanel");
        showContent = GameObject.Find("ContentPanel");
        player = GameObject.Find("MediaPlayer").GetComponent<MediaPlayer>();
        Button[] btns = GameObject.Find("Canvas/PeoplePanel/BtnPanel").transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < btns.Length; i++)
        {
            btnList.Add(btns[i]);
        }
        Transform imgs = GameObject.Find("Canvas/PeoplePanel/Images").transform;
        for (int i = 0; i < imgs.childCount; i++)
        {
            imgList.Add(imgs.GetChild(i).GetComponent<Image>());
        }
        
        for (int i = 0; i < imgList.Count; i++)
        {
            imgList[i].gameObject.SetActive(false);
        }

        peoplePanel.SetActive(true);
        showContent.SetActive(false);
        
    }
    float timer;
    float time;
    public bool isStart;
    private void Update()
    {
       
        if (udPServer.recvStr == "cut")
        {
            showContent.SetActive(true);
            player.Control.Rewind();
            player.Control.Stop();
            peoplePanel.SetActive(false);
        }
        if (udPServer.recvStr == "back")
        {
           
            showContent.SetActive(false);
            peoplePanel.SetActive(true);
            player.Control.Rewind();
            player.Control.Play();
            for (int i = 0; i < imgList.Count; i++)
            {
                imgList[i].gameObject.SetActive(false);
            }
        }
        if (udPServer.recvStr == "hello")
        {
                player.Control.Play();
        }
        udPServer.recvStr = "";
    }
    public void Btn_OnClick(GameObject go)
    {
       
       
        go.SetActive(true);
        go.GetComponent<CanvasGroup>().alpha = 0;
        go.GetComponent<CanvasGroup>().DOFade(1, 1);
    }

    public void Close(GameObject go)
    {
        go.SetActive(false);
        go.GetComponent<CanvasGroup>().alpha = 0; 
    }
    
}
