using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler ,IPointerClickHandler
{
    public Image image;
    
    public Sprite defaltSprite;

    public Inventory myInventory;

    private ItemSet _item;

    public DragItem myItem;

    [SerializeField]
    public ItemSet.ItemGrade myItemGrade;

    public int index;

    private GameManager GetGmInst()
    {
        return GameManager.Inst;
    }
    private void Start()
    {
        
        myItem = GetComponentInChildren<DragItem>();
        image = myItem.GetComponent<Image>();
        index = int.Parse(myItem.gameObject.name)-1;
    }
   

    public ItemSet item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.MySprite;
                image.color = new Color(1, 1, 1, 1);
               
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        
        DragItem newItem = eventData.pointerDrag.GetComponent<DragItem>();
        DragItem curItem = GetComponentInChildren<DragItem>();
        if (curItem != null)
        {
            curItem.ChageParent(newItem.orgParent, true);
        }
        newItem.ChageParent(transform);
    }
    float Power;
    public void OnPointerClick(PointerEventData eventData)
    {
        myInventory.myObj = GetGmInst().characters[GetGmInst().curCharacter];
        myInventory.myPlayer = myInventory.myObj.GetComponent<Player>();
        myItem = GetComponentInChildren<DragItem>();
        image = myItem.GetComponent<Image>();
        index = int.Parse(myItem.gameObject.name) - 1;
        if (myInventory.items[index] !=null && eventData.clickCount == 2)
        {
            //myItem = GetComponentInChildren<DragItem>();
            //image = myItem.GetComponent<Image>();
            //index = int.Parse(myItem.gameObject.name) - 1;
            image.sprite = defaltSprite;
            image.color = new Color(1, 1, 1, 0);
            switch (myItemGrade)
            {
                case ItemSet.ItemGrade.Rare:
                    Power= myInventory.items[index].power*1.0f;
                    break;
                case ItemSet.ItemGrade.Epic:
                    Power = myInventory.items[index].power * 2.0f;
                    break;
                case ItemSet.ItemGrade.Legendary:
                    Power = myInventory.items[index].power * 3.0f;
                    break;
            }
            switch (myInventory.items[index].myType)
            {
                case ItemSet.ItemType.Power:
                    Debug.Log("Power UP");
                    myInventory.myPlayer.OrgAttackPower += Power;
                    myInventory.DestroyItem(index);
                    break;
                case ItemSet.ItemType.Armor:
                    Debug.Log("Defence UP");
                    myInventory.myPlayer.OrgDeffencePower += Power;
                    myInventory.DestroyItem(index);
                    break;
            }
            
        }
    }

}