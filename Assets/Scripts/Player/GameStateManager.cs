using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject restartPanel;

    public bool IsGameOver { get; private set; }

    private void OnEnable()
    {
        var health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.OnDeath += HandlePlayerDeath;
        }
    }

    private void OnDisable()
    {
        var health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.OnDeath -= HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        IsGameOver = true;

        var score = GetComponent<PlayerScore>();
        if (score != null)
        {
            score.ResetScore();
        }

        StartCoroutine(ShowRestartPanel());
    }

    private IEnumerator ShowRestartPanel()
    {
        yield return new WaitForSeconds(1f);
        restartPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}