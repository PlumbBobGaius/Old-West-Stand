using System.Collections.Generic;
using UnityEngine;

public class PositionManeger : MonoBehaviour
{

    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject EnemyPrefab;
    Vector3 ArenaCenter = new Vector3(0, 0, 0);


    Position[] A_Position;
    List<Position> l_position = new List<Position>();
    public List<Position> L_positon { get { return l_position; } }

    private void Awake()
    {
        A_Position = GetComponentsInChildren<Position>();

        for (int i = 0; i < A_Position.Length; i++)
        {
            l_position.Add(A_Position[i]);
        }
    }

    void Start()
    {
        for (int i = 0; i < l_position.Count;)
        {
            int randomPos = Random.Range(0, l_position.Count);
            if (i == 0)
            {
                Instantiate(PlayerPrefab, l_position[randomPos].transform.position, Quaternion.identity, l_position[randomPos].transform);
                l_position[randomPos].SetOcupied();
                l_position[randomPos].IsPlayer(true);
                i++;
            }
            else if (!l_position[randomPos].Ocupied)
            {
                Instantiate(EnemyPrefab, l_position[randomPos].transform.position, Quaternion.identity, l_position[randomPos].transform);
                l_position[randomPos].SetOcupied();
                i++;
            }
        }
    }
}
