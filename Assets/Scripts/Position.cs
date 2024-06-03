using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{

    Vector3 coordinates;
    public Vector3 Coordiantes
    {
        get
        {
            return coordinates;
        }
    }

    bool ocupied = false;
    public bool Ocupied
    {
        get
        {
            return ocupied;
        }
    }

    bool isplayer = false;
    public bool Isplayer
    {
        get
        {
            return isplayer;
        }
    }

    public bool isDead = false;

    readonly Dictionary<Vector3, int> enemyDictionary = new();
    public Dictionary<Vector3, int> EnemyDictionary { get { return enemyDictionary; } }

    public Queue<Vector3> positionManegerQueue = new Queue<Vector3>();

    [SerializeField] List<Material> RockMatiral = new List<Material>();
    [SerializeField] List<GameObject> RockList = new List<GameObject>();

    PositionManeger positionManeger;



    private void Awake()
    {
        coordinates = transform.position;
    }

    private void Start()
    {
        positionManeger = FindObjectOfType<PositionManeger>();
        ListEnemys();
    }
    public void SetOcupied()
    {
        if (ocupied) return;
        ocupied = true;
    }

    public void IsPlayer(bool player)
    {
        isplayer = player;
    }
    public void PutEnemyInQeueu(Vector3 coords)
    {
        if (positionManegerQueue.Contains(coords)) return;
        positionManegerQueue.Enqueue(coords);
    }


    public void Cahngecolor(int color)
    {
        foreach (GameObject rock in RockList)
        {
            rock.GetComponent<MeshRenderer>().material = RockMatiral[color];
        }
    }


    void ListEnemys()
    {
        List<Position> list = positionManeger.L_positon;
        for (int i = 0; i < list.Count; i++)
        {
            {
                if (list[i].coordinates == coordinates)
                {
                    enemyDictionary.Add(coordinates, i);
                }
                else
                {
                    enemyDictionary.Add(list[i].transform.position, i);
                }

            }

        }
    }
}
