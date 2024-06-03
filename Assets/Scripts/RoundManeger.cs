using System.Collections;
using UnityEngine;

public class RoundManeger : MonoBehaviour
{

    [SerializeField] float TimeBetwenRounds;
    [SerializeField] float TimeToShoot;
    [SerializeField] float SuspetionTimer = 0.3f;
    [SerializeField] AudioClip TowerBell, ShotdownMusic;
    bool isAllowShoting = false;


    float PreparationTimer, shotdownMusicTimer;
    PositionManeger positionManeger;
    AudioSource audioSource;

    public bool IallowShoting { get { return isAllowShoting; } }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        positionManeger = FindObjectOfType<PositionManeger>();
        PreparationTimer = TimeBetwenRounds + 10 + Time.realtimeSinceStartup;
        shotdownMusicTimer = Mathf.Abs(ShotdownMusic.length - PreparationTimer);
        Debug.Log(shotdownMusicTimer);
    }

    void Update()
    {
        Timer();


    }

    private void Timer()
    {
        if (Time.realtimeSinceStartup >= shotdownMusicTimer)
        {
            if (!audioSource.isPlaying)
            {
                AudioSelector(1);

            }
            shotdownMusicTimer += 5f;
        }

        if (Time.realtimeSinceStartup >= PreparationTimer)
        {
            if (!audioSource.isPlaying) { AudioSelector(0); }
            StartCoroutine(AllowShoting());
            positionManeger.BroadcastMessage("ShootingRoutine", SendMessageOptions.DontRequireReceiver);
            return;
        }
        else
        {
            positionManeger.BroadcastMessage("IsShoting", SendMessageOptions.DontRequireReceiver);
            isAllowShoting = false;
        }

    }
    IEnumerator AllowShoting()
    {
        isAllowShoting = true;
        yield return new WaitForSeconds(TimeToShoot);
        PreparationTimer = Time.realtimeSinceStartup + TimeBetwenRounds;
        shotdownMusicTimer = Mathf.Abs(ShotdownMusic.length - PreparationTimer);
    }
    void AudioSelector(int track)
    {
        switch (track)
        {
            case 0:
                audioSource.clip = TowerBell;
                Debug.Log($"paling TowerBell at{Time.realtimeSinceStartup}");
                break;
            case 1:
                audioSource.clip = ShotdownMusic;
                Debug.Log($"paling shotdonwMusic at{Time.realtimeSinceStartup}");
                break;
        }
        audioSource.Play();
    }
}
