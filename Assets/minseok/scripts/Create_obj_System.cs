using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_obj_System : Singleton<Create_obj_System>
{
    [SerializeField]
    private int Obj_Count = 20;
    public GameObject TeleportObj;
    public GameObject ChestObj;
    public GameObject TPUI;
    public GameObject ChestUI;
    public GameObject SecretObj;
    Vector2Int pos = Vector2Int.zero;
    Vector3 mypos = Vector3.zero;
    public List<Teleport> teleporters;
    public GameObject myTPtarget = null;
    public GameObject myChesttarget = null;
    int x = 0, y = 0;
    List<int> list;


    public Vector3 random(int rd)//랜덤 좌표 설정
    {

        switch (rd)
        {
            case 0: x = 80; y = 80; break;
            case 1: x = 76; y = 44; break;
            case 2: x = 58; y = 50; break;
            case 3: x = 42; y = 89; break;
            case 4: x = 18; y = 85; break;
            case 5: x = 24; y = 10; break;
            case 6: x = 26; y = 33; break;
            case 7: x = 15; y = 62; break;
            case 8: x = 45; y = 63; break;
            case 9: x = 70; y = 15; break;
            case 10: x = 51; y = 7; break;
            case 11: x = 92; y = 34; break;
            case 12: x = 4; y = 95; break;
            case 13: x = 3; y = 35; break;
            case 14: x = 61; y = 94; break;
            case 15: x = 49; y = 26; break;
            case 16: x = 73; y = 65; break;
            case 17: x = 83; y = 11; break;
            case 18: x = 30; y = 72; break;
            case 19: x = 32; y = 50; break;
        }

        Vector2Int my_Pos = new Vector2Int(x, y);

        float half = MapManager.Inst.scale * 0.5f;
       

        return new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);
    }
    void teleportSystem() //오브젝트설치
    {
        int rd;

        for (int j = 0; j < Obj_Count/2;) //10번반복
        {
            rd = Random.Range(0, 20); //1-10 랜덤숫자 넣기
            if (list.Contains(rd)) //list에서 중복된숫자가있다면
            {
                rd = Random.Range(0, 20);
            }
            else //list에서 중복된숫자가없다면 list에 숫자넣고 j++
            {
                list.Add(rd); //0번인덱스부터 9까지 1-11랜덤숫자 넣기       
                j++;
            }
        }
        GameObject obj;
        for (int i = 0; i < 10; ++i)
        {
            obj = Instantiate(TeleportObj, random(list[i]), Quaternion.identity);
            obj.transform.parent = transform;
            //obj1.GetComponent<Teleport>().pos = pos;
        }
        for (int i = 10; i < list.Count; i++)
        {
            obj = Instantiate(ChestObj, random(list[i]), Quaternion.identity);  
            obj.transform.parent = transform;
            //obj2.GetComponent<Chest>().pos = pos;
        }
        for (int i = 0; i <= Obj_Count; ++i)
        {
            float half = MapManager.Inst.scale * 0.5f;
            Vector2Int my_Pos;
            do
            {
                x = Random.Range(2, 99);
                y = Random.Range(2, 99);
                my_Pos = new Vector2Int(x, y);
                half = MapManager.Inst.scale * 0.5f;
                mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);
            } while (!MapManager.Inst.tiles.ContainsKey(my_Pos) || (MapManager.Inst.tiles[my_Pos].GetComponent<TileStatus>().isVisited < -1 ||
                MapManager.Inst.tiles[my_Pos].GetComponent<TileStatus>().isVisited == 0));

            //pos = new Vector2Int(x, y);
            obj = Instantiate(SecretObj, mypos, Quaternion.identity);
            //obj3.GetComponent<SecretItem>().pos = pos;
            obj.transform.SetParent(this.transform);
        }


    }


    void Start()
    {
        list = new List<int>();
        teleportSystem();
        //teleporters = new List<Teleport>();
    }

    
    void Update()
    {
        
    }

    public  void Ontp() //TPUI YES누를시
    {
        TPUI.SetActive(false);
        myTPtarget.GetComponent<Teleport>().tp();
    }
    public void TPtarget(Transform target) //TP오브젝트 클릭시여기로 클릭한 오브젝트 저장
    {
        myTPtarget = target.GetComponent<TileStatus>().my_target.gameObject;
        myTPtarget.GetComponent<Animator>().SetTrigger("TP");
    }
    public void OnChest() //ChestUI 확인누를시
    {
        ChestUI.SetActive(false);
        myChesttarget.GetComponent<Chest>().chest();
    }
    public void Chesttarget(Transform target) //Chest오브젝트 클릭시여기로 클릭한 오브젝트 저장
    {
        myChesttarget = target.GetComponent<TileStatus>().my_target.gameObject;
        myChesttarget.GetComponent<Animator>().SetBool("OPEN", true);
    }
}
