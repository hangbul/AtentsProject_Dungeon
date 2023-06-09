using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatDataBase", menuName = "ScriptableObjects/CharacterDB", order = 4)]


public class CharacterDB :  ScriptableObject
{

    [SerializeField]
    public List<Character> characterList = new List<Character>(4);


}
