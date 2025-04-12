using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimerEnhanced : MonoBehaviour
{
	public TMP_Text timerText;
	public TMP_Text countdownText;

    [Header("Timer Settings")]
    public float roundDuration = 60f;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip countBeep;
    public AudioClip finalBeep;
    public AudioClip buzzerSound;

    private float timeRemaining;
    private bool roundStarted = false;
    private bool finalWarningTriggered = false;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (!roundStarted) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            roundStarted = false;
            EndRound();
        }

        UpdateTimerUI();

        if (!finalWarningTriggered && timeRemaining <= 10f)
        {
            finalWarningTriggered = true;
            StartCoroutine(FinalSecondsWarning());
        }
    }

    IEnumerator StartCountdown()
    {
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            audioSource.PlayOneShot(countBeep);
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = "GO!";
        audioSource.PlayOneShot(countBeep);
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
        StartTimer();
    }

    void StartTimer()
    {
        timeRemaining = roundDuration;
        roundStarted = true;
        finalWarningTriggered = false;
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator FinalSecondsWarning()
    {
        for (int i = 0; i < 10 && roundStarted; i++)
        {
            timerText.color = Color.red;
            audioSource.PlayOneShot(finalBeep);
            yield return new WaitForSeconds(0.5f);
            timerText.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void EndRound()
    {
        Debug.Log("Round over!");
        audioSource.PlayOneShot(buzzerSound);
        // You can load a scene here or show a fail screen
        // SceneManager.LoadScene("FailScreen");
    }
}

