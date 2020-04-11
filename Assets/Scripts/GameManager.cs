using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    #endregion

    public float ScoreToNextDifficulty;

    public float ObjectSpeed;
    public float boost;
    public Vector3 ObjectSpawnPoint;

    public float Score;
    public Text[] textFields;
    public GameObject[] MenuFields;

    public string Path;

    private void Start()
    {
        Instance = this;
        StartCoroutine(SpawntheObstacle());
    }

    private void Update()
    {
        Score += 2 * Time.deltaTime;

        if(Score >= ScoreToNextDifficulty)
        {
            ObjectSpeed += boost;
            ScoreToNextDifficulty += ScoreToNextDifficulty;
        }

        for (int i = 0; i < textFields.Length; i++)
        {
            if(textFields[i].name == "Score")
            {
                textFields[i].text = Score.ToString("F0");
            }
        }

    }

    public IEnumerator SpawntheObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            InstatiateObstacle();
        }
    }

    public void InstatiateObstacle()
    {
        int RandomObstacle = Random.Range(0, 3);

        GameObject obstacle = Instantiate(Resources.Load(Path + "Obstacle " + RandomObstacle, typeof(GameObject)), ObjectSpawnPoint, Quaternion.identity) as GameObject;

        Destroy(obstacle, 60f);
    }

    public void PauseGame()
    {

    }

    public void EndGame()
    {
        for (int i = 0; i < MenuFields.Length; i++)
        {
            if(MenuFields[i].name != "Game Over Menu")
            {
                MenuFields[i].gameObject.SetActive(false);
            } else
            {
                MenuFields[i].gameObject.SetActive(true);
            }
        }

        Time.timeScale = 0f;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ObjectSpawnPoint, 0.25f);
    }
}
