using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public int idx;
    public void SendIndex()
    {
            CharacterSlotDB.cdb.ChooseCharacters(idx);
            CharacterSlotDB.cdb.CharacterSelection(idx); //스킬창 charName.text변경
    }
}
