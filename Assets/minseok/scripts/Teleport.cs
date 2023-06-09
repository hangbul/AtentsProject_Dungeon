using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Vector2Int pos;
    public LayerMask pickMask;
    public List<TileStatus> tiles;
    public GameObject TPEffect;
    void Start()
    {

        Ray ray = new Ray(transform.position + new Vector3(0,0.5f,0), new Vector3(0, -1, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, 10.0f, pickMask))
        {
            pos = MapManager.Inst.GetTileIndex(hit.transform.gameObject);
            MapManager.Inst.tiles[pos].my_target = this.gameObject;
            MapManager.Inst.tiles[pos].my_obj = OB_TYPES.TELEPORT;
        }
        Setting();
    }

    Vector3 mypos = Vector3.zero;
    int x = 0, y = 0;
    
    public void tp() //������ǥ�� �̵���Ű��
    {
        x = Random.Range(1, 20);
        y = Random.Range(1, 20);

        Vector2Int my_Pos = new Vector2Int(x, y);

        float half = MapManager.Inst.scale * 0.5f;
        mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);

        while (MapManager.Inst.tiles[pos].GetComponent<TileStatus>().isVisited < -1 ||
            MapManager.Inst.tiles[pos].GetComponent<TileStatus>().isVisited == 0)
        {
            x = Random.Range(1, 20);
            y = Random.Range(1, 20);

            my_Pos.x = x;
            my_Pos.y = y;

            half = MapManager.Inst.scale * 0.5f;
            mypos = new Vector3((float)my_Pos.x + half, 0, (float)my_Pos.y + half);
        }

        pos = new Vector2Int(x, y);
        Vector2Int pos2 = new Vector2Int();
        foreach (var tile in tiles) //�̵�
        {
            if (tile.my_obj == OB_TYPES.PLAYER)
            {
                STATE _curState = tile.my_target.GetComponent<Player>().GetState();
                if (_curState == STATE.ACTION)
                {
                    pos2.x = tile.my_target.GetComponent<Player>().my_Pos.x;
                    pos2.y = tile.my_target.GetComponent<Player>().my_Pos.y;

                    GameObject obj1 = Instantiate(TPEffect, tile.my_target.transform.position, Quaternion.identity);

                    tile.my_target.transform.position = mypos;
                    tile.my_target.GetComponent<Player>().my_Pos = pos;

                    GameObject obj2 = Instantiate(TPEffect, tile.my_target.transform.position, Quaternion.identity);

                    MapManager.Inst.tiles[pos].my_target = tile.my_target.gameObject;
                    MapManager.Inst.tiles[pos].my_obj = tile.my_target.GetComponent<Player>().myType;
                    MapManager.Inst.tiles[pos].isVisited = 0;
                    MapManager.Inst.tiles[pos].my_target = null;
                    MapManager.Inst.tiles[pos].my_obj = OB_TYPES.NONE;
                    MapManager.Inst.tiles[pos].isVisited = -1;

                    
                }
            }
        }
    }
    public void Setting()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) // && GameManager.GM.CheckIncludedIndex(new Vector2Int(pos.x+i,pos.y+j))
                {
                    continue;
                }
                Vector2Int newPos = pos + new Vector2Int(i, j);
                if (MapManager.Inst.tiles.ContainsKey(newPos))
                {
                    MapManager.Inst.tiles[newPos].my_obj = OB_TYPES.TELEPORT;
                    MapManager.Inst.tiles[newPos].my_target = this.gameObject;
                    MapManager.Inst.tiles[newPos].is_blocked = true;
                    tiles.Add(MapManager.Inst.tiles[newPos]);
                }
            }
        }
        
            

    }
}
