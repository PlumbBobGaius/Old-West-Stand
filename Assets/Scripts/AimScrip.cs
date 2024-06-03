using UnityEngine;

public class AimScrip : MonoBehaviour
{
    Position position;
    EnemyScript enemyScript;
    Vector3 laTarget, raTarget;

    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();


        FindTargets();

    }

    void Update()
    {

    }

    void FindTargets()
    {

    }

}
