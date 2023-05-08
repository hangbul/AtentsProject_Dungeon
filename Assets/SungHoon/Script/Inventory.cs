using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemSet> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    [SerializeField]
    public Player myPlayer;


    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        FreshSlot();
        for (int i = 0; i < slots.Length; i++)
        {
            items.Add(null);
        }
    }

    public void FreshSlot()
    {
        int i = 0;  
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }
    int count = 0;
    public void AddItem(ItemSet _item)
    {
        count++;
        for (int i = 0; i < slots.Length;)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                FreshSlot();
                break;
            }
            else
            {
                i++;
            }
        }
        //if (items.Count < slots.Length)
        //{
        //    items.Add(_item);
        //    Debug.Log($"{count}�� �ε��� �߰�");
        //    FreshSlot();
        //}
        //else
        //{
        //    print("������ ���� �� �ֽ��ϴ�.");
        //}
    }
    
    public void DestroyItem(int index) 
    {
        //items.RemoveAt(index);
        items[index] = null;
    }
}