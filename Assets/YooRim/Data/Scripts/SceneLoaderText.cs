using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SceneLoaderText : Singleton<SceneLoaderText>
{
    public Image MapImage;
    public Sprite Map1;
    public Sprite Map2;
    public Sprite Title;

    public TMP_Text mapText;
    public TMP_Text loadingText;
    string[] textList = new string[3];
    int rnd;

    // Start is called before the first frame update
    void Start()
    {
        textList[0] = "바람의 냄새를 맡으며 우리는 잃어버린 낙원의 기억을 아련하게 떠올린다.";
        textList[1] = "그날의 슬픔은 우리는 기억하지 못한다. 그저 한 마리의 짐승이 서글퍼 할 뿐이였다.";
        textList[2] = "짐승의 노래를 들어라, 슬픔의 노래를 들어라 , 통한의 노래를 들어라.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int i)
    {
        MapImage.sprite = i == 2 ? Map1 : i == 3 ? Map2 : Title;
        mapText.text = i == 2 ? "실낙원" : i == 3 ? "어스름의 미궁" : "";
        rnd = Random.Range(0, 3);
        loadingText.text = textList[rnd];
    }
}
