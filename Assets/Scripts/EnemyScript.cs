using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Life))]
public class EnemyScript : MonoBehaviour
{
    [Range(.3f, .99f)] public float MinReactionTime;
    [Range(1f, 2f)] public float MaxReactionTime;
    [Range(.2f, 3f)] public float Suspition, AddToTargetList;
    [SerializeField] GameObject LeftArm, RightArm, LaGun, RaGun;

    Dictionary<Vector3, int> EnemyesCoordToArayDictionary = new Dictionary<Vector3, int>();
    List<Position> ListPosition = new List<Position>();
    List<Position> notDead = new List<Position>();
    List<AudioClip> AudioClipList = new List<AudioClip>();

    RoundManeger roundManeger;
    Position Position;
    PositionManeger positionManeger;
    AudioSource audioSource;

    bool isShooting = false;
    float ChangeTargetTimer, AddToListTimer;
    int susp1, susp2;
    float time;
    int myArray;

    private void Awake()
    {
        roundManeger = FindObjectOfType<RoundManeger>();
        positionManeger = FindObjectOfType<PositionManeger>();
        Position = GetComponentInParent<Position>();
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        transform.LookAt(new Vector3(0, 1.22f, 0));
        EnemyesCoordToArayDictionary = Position.EnemyDictionary;
        ListPosition = positionManeger.L_positon;
        for (int i = 0; i < ListPosition.Count; i++)
        {
            if (ListPosition[i].Coordiantes == transform.position)
            {
                myArray = i;
            }
        }
        GetNewTarget();
        AddToListTimer = time += AddToTargetList;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > ChangeTargetTimer)
        {
            GetNewTargetFromQueue();
            GetNewTarget();
        }

        if (time > AddToListTimer)
        {
            PutThisGameObjectOnATargetList(0, susp1);
            PutThisGameObjectOnATargetList(1, susp2);
        }

    }

    void GetNewTarget()
    {
        susp1 = AimArmAT(1, GetRandonTarget());
        susp2 = AimArmAT(0, GetRandonTarget());
    }

    void GetNewTargetFromQueue()
    {
        if (Position.positionManegerQueue.Count > 1)
        {
            Vector3 target = Position.positionManegerQueue.Dequeue();

            EnemyesCoordToArayDictionary.TryGetValue(target, out int possId);

            CheckForDead();

            if (ListPosition[possId].isDead == true) { return; }

            int arm = Random.Range(0, 2);

            if (arm == 0)
                susp1 = AimArmAT(arm, target);
            else susp2 = AimArmAT(arm, target);
        }
        else return;
    }
    int AimArmAT(int arm, Vector3 target)
    {
        ChangeTargetTimer = time + Suspition;
        switch (arm)
        {
            case 0:

                LeftArm.transform.LookAt(target);
                break;
            case 1:
                RightArm.transform.LookAt(target);
                break;
        }

        EnemyesCoordToArayDictionary.TryGetValue(target, out int enemy);
        return enemy;
    }

    void PutThisGameObjectOnATargetList(int arm, int Target)
    {
        switch (arm)
        {
            case 0:
                ListPosition[Target].PutEnemyInQeueu(transform.position);
                if (!ListPosition[Target].GetComponentInChildren<EnemyScript>()) return;
                break;
            case 1:
                ListPosition[Target].PutEnemyInQeueu(transform.position);
                if (!ListPosition[Target].GetComponentInChildren<EnemyScript>()) return;
                break;
        }
    }

    Vector3 GetRandonTarget()
    {
        CheckForDead();
        int target = Random.Range(0, ListPosition.Count);
        if (ListPosition[target].isDead == true)
        {
            target = Random.Range(0, notDead.Count);
            return notDead[target].Coordiantes;
        }
        if (target == myArray)
        {
            target = Random.Range(0, notDead.Count);
            return notDead[target].Coordiantes;
        }
        return ListPosition[target].Coordiantes;
    }

    void Shot()
    {

        if (roundManeger.IallowShoting)
        {
            StartCoroutine(Trigger());
        }
    }

    IEnumerator Trigger()
    {
        float Time = ReactionTime();
        yield return new WaitForSeconds(Time);
        LaGun.SetActive(true);
        RaGun.SetActive(true);
    }

    float ReactionTime()
    {
        float reactionTime;
        reactionTime = Random.Range(MinReactionTime, MaxReactionTime);
        return reactionTime;
    }
    public void IsShoting()
    {
        isShooting = false;
    }
    public void ShootingRoutine()
    {
        if (isShooting) return;
        StartCoroutine(ShootNwait());
    }

    IEnumerator ShootNwait()
    {
        Shot();
        yield return new WaitForSeconds(.5f);
        isShooting = true;
    }

    void CheckForDead()
    {
        notDead.Clear();
        foreach (var position in ListPosition)
        {
            if (position.isDead == false && position.Coordiantes != transform.position)
            {
                notDead.Add(position);
            }
        }
    }




}

//void sus1(int target, float time)
//{
//    if (time > aimingSince1)
//    {
//        Debug.Log("alo");
//        RaiseSuspition(0, target);
//        GetNewTargetFromQueue();
//    }
//    else return;
//}
//void sus2(int target, float time)
//{
//    if (time > aimingSince2)
//    {
//        Debug.Log("alo");
//        RaiseSuspition(1, target);
//        GetNewTargetFromQueue();
//    }
//    else return;
//}