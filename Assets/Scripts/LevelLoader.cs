using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    List<GameObject> Enemys = new List<GameObject>();
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI Condition;
    [SerializeField] GameObject SecondCmaera;

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        if (SceneManager.GetActiveScene().buildIndex == 1 && Enemys.Count == 0)
        {
            WinScren();
        }
        else
        {
            Enemys.Clear();
        }
    }

    void WinScren()
    {
        canvas.GetComponent<Canvas>().enabled = true;
        Condition.text = ("congratulation you are the last one standing!");
    }

    public void Lose()
    {
        SecondCmaera.SetActive(true);
        canvas.GetComponent<Canvas>().enabled = true;
        Condition.text = ("Better luck next time");
    }

}
