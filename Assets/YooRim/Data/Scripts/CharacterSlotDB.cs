using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotDB : MonoBehaviour
{
    public CharacterDB chosenDB;
    public CharacterDB charDB;

    public CharcterSet charSet;

    static public CharacterSlotDB cdb = null;
    

    //ChosenCharList
    public GameObject[] ChosenCharList;

    //text
    public TMP_Text[] chosenCharTextList;

    public Button[] characterButtonList;

    private Animator ani;

    int count = 0;
    int[] temp = new int[4];

    private void Start()
    {
        cdb = this;
        ani = GetComponent<Animator>();
        chosenDB.characterList.Clear();

        for (int i = 0; i<4; i++)
        {
            chosenCharTextList[i].text = "0";
        }
        count = 0;
}

    private void Update()
    {
    }


    public void ChooseCharacters(int idx)
    {
        charSet.SendIdx(idx);
    }

    public void ChosenCharacterButtonsActive(int idx)
    {
        if(chosenDB.characterList.Count < 4)
        {
            chosenDB.characterList.Add(charDB.characterList[idx]);
            chosenDB.characterList = chosenDB.characterList.Distinct().ToList();
            //버튼활성화 비활성화 && 텍스트 수정
            if (!ChosenCharList[0].activeSelf)
            {
                ChosenCharList[0].SetActive(true);
                chosenCharTextList[0].text = charDB.characterList[idx].Name;
                temp[0] = idx;
            }
            else if (!ChosenCharList[1].activeSelf)
            {
                ChosenCharList[1].SetActive(true);
                chosenCharTextList[1].text = charDB.characterList[idx].Name;
                temp[1] = idx;
            }
            else if (!ChosenCharList[2].activeSelf)
            {
                ChosenCharList[2].SetActive(true);
                chosenCharTextList[2].text = charDB.characterList[idx].Name;
                temp[2] = idx;
            }
            else if (!ChosenCharList[3].activeSelf)
            {
                ChosenCharList[3].SetActive(true);
                chosenCharTextList[3].text = charDB.characterList[idx].Name;
                temp[3] = idx;
            }
        }
    }

    public void CharacterSelection(int i) //Settings
    {
        charSet.gameObject.SetActive(true);
    }

    public void DeleteCharacters(int idx)
    {
        if (ChosenCharList[idx].activeSelf)
        {
            ChosenCharList[idx].SetActive(false);
            charSet.gameObject.SetActive(true);
            ActiveCharacters(idx);
            charSet.SendIdx(temp[idx]);
            chosenDB.characterList.Remove(charDB.characterList[temp[idx]]);
        }
    }

    public void ActiveCharacters(int idx)
    {
        if (!characterButtonList[temp[idx]].interactable)
        {
            characterButtonList[temp[idx]].interactable = true;
            count--;
            Debug.Log(count);
        }
    }

    public void DeactiveCharacters(int idx)
    {
        if (characterButtonList[idx].interactable && count < 4)
        {
            characterButtonList[idx].interactable = false;
            count++;
            Debug.Log(count);
        }
    }

    public void OverCharacterList()
    {
        if (chosenDB.characterList.Count == 4)
        {
            ani.SetTrigger("Notify");
        }
    }
}
