using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public TextMeshProUGUI position_text;
    public AudioSource audioSource;
    public DateTime startTime = DateTime.Now;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    bool audio_playing = false;
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

        Vector3 toExit = (transform.position - player.transform.position).normalized;
        Vector3 playerForward = player.transform.forward;

        float dot = Vector3.Dot(playerForward, toExit);
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (dot > 0.8f)
        {
            position_text.text = "Player is facing the exit!";
        }
        else
        {
            position_text.text = "Player is not facing the exit";
        }

        Debug.Log((DateTime.Now - startTime).TotalMilliseconds);
        if ((DateTime.Now - startTime).TotalMilliseconds > 40000 && !audio_playing)
        {
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
            audioSource.Play();
            audio_playing = true;
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}
