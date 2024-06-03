using System.Collections.Generic;
using UnityEngine;

public class GunSFXscript : MonoBehaviour
{
    public List<AudioClip> clipList = new List<AudioClip>();

    private void Awake()
    {
        gameObject.GetComponent<AudioSource>().clip = clipList[Random.Range(0, clipList.Count - 1)];
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        gameObject.GetComponent<AudioSource>().clip = clipList[Random.Range(0, clipList.Count - 1)];
    }
}
