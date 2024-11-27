using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestSceneManager : MonoBehaviour
{
    private bool footsteps = false;
    [SerializeField] private GameObject footstepPeople, footlessPeople;

    [SerializeField] private Button footButton;
    [SerializeField] private GameObject footText;

    public void ToggleMusic()
    {
        PlayUISound();

        MusicManager.Instance.ToggleMusic();
    }

    public void NextSong()
    {
        PlayUISound();

        MusicManager.Instance.PlayNextTrack(true);
    }

    public void ToggleFootsteps()
    {
        switch(footsteps)
        {
            case true:
                footstepPeople.SetActive(false);
                footlessPeople.SetActive(true);
                footsteps = false;
                break;
            case false:
                footstepPeople.SetActive(true);
                footlessPeople.SetActive(false);
                footsteps = true;
                footButton.interactable = false;
                footText.SetActive(true);
                break;
        }
    }

    public void PlayUISound()
    {
        UISoundManager.Instance.PlaySound(0); // playing from index 0
    }
}
