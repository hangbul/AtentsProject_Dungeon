using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public SkillSet _my_skill;

    public void CastingSkill()
    {
        if(_my_skill != null)
            GameManager.GM.Players[GameManager.GM.currentPlayer].GetComponent<Player>().OnSkilCastStart(_my_skill);
    }
}
