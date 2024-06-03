using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Life))]
public class PlayerScrip : MonoBehaviour
{
    [Range(.2f, 1f)] public float SusTime;
    [SerializeField] GameObject LeftArm, RightArm, LaGun, RaGun;


    PositionManeger positionManeger;
    List<Position> Positions = new List<Position>();
    RoundManeger roundManeger;

    int leftArmTarget, rightArmTarget;

    float sus1;
    float sus2;
    float time;

    private void Awake()
    {
        positionManeger = FindObjectOfType<PositionManeger>();
        roundManeger = FindObjectOfType<RoundManeger>();

    }
    void Start()
    {
        foreach (Position position in positionManeger.L_positon)
        {
            if (!position.Isplayer)
            {
                Positions.Add(position);
            }
        }
        transform.LookAt(new Vector3(0, 1.22f, 0));
    }

    void Update()
    {
        time += Time.deltaTime;
        RightArmControls();
        LeftArmControls();
        Shot();
        if (time > sus2)
        {
            BecomeSus(rightArmTarget);
        }
        if (time > sus1)
        {
            BecomeSus(leftArmTarget);
        }

    }
    void LeftArmControls()
    {
        Positions[leftArmTarget].Cahngecolor(2);
        if (Input.GetKeyDown(KeyCode.A))
        {
            sus1 = time + SusTime;
            if (leftArmTarget >= Positions.Count - 1)
            {
                leftArmTarget = 0;
            }
            else leftArmTarget++;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sus1 = time + SusTime;
            if (leftArmTarget <= 0)
            {
                leftArmTarget = Positions.Count - 1;
            }
            else leftArmTarget--;
        }
        LeftArm.transform.LookAt(Positions[leftArmTarget].transform.position);
        Positions[leftArmTarget].Cahngecolor(0);
        if (time > sus1)
        {
            BecomeSus(leftArmTarget);
        }
    }

    void RightArmControls()
    {
        Positions[rightArmTarget].Cahngecolor(2);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sus2 = time + SusTime;
            if (rightArmTarget >= Positions.Count - 1)
            {
                rightArmTarget = 0;
            }
            else rightArmTarget++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sus2 = time + SusTime;
            if (rightArmTarget <= 0)
            {
                rightArmTarget = Positions.Count - 1;
            }
            else rightArmTarget--;
        }
        RightArm.transform.LookAt(Positions[rightArmTarget].transform.position);
        Positions[rightArmTarget].Cahngecolor(1);
        if (time > sus1)
        {
            BecomeSus(rightArmTarget);
        }

    }

    void BecomeSus(int target)
    {
        if (!Positions[target].transform) return;
        Positions[target].PutEnemyInQeueu(transform.position);
    }

    void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && roundManeger.IallowShoting)
        {
            Trigger(true);
        }
    }

    private void Trigger(bool state)
    {
        LaGun.SetActive(state);
        RaGun.SetActive(state);
    }

    private void OnDestroy()
    {
        LevelLoader level = FindObjectOfType<LevelLoader>();
        level.Lose();
    }


}
