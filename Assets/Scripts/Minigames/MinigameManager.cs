using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MinigameManager : MonoBehaviour
{
    private int currentMinigame;

    private GameObject _min1, _min2, _min3;
    // private string[] _welcome = {"event:/VO/Circus Forest/Minigame 1/vo_circusforest_minigame1_clown_welcome_01", "event:/VO/Circus Forest/Minigame 2/vo_circusforest_minigame1_clown_welcome_01", "event:/VO/Circus Forest/Minigame 3/vo_circusforest_minigame1_clown_welcome_01"};
    // private string[] _explanation = {"event:/VO/Circus Forest/Minigame 1/vo_circusforest_minigame1_clown_explanation_01", "event:/VO/Circus Forest/Minigame 2/vo_circusforest_minigame1_clown_explanation_01", "event:/VO/Circus Forest/Minigame 3/vo_circusforest_minigame1_clown_explanation_01" };
    // private string[] _win = {"event:/VO/Circus Forest/Minigame 1/vo_circusforest_minigame1_clown_won_01", "event:/VO/Circus Forest/Minigame 2/vo_circusforest_minigame1_clown_won_01", "event:/VO/Circus Forest/Minigame 3/vo_circusforest_minigame1_clown_won_01" };
    // private string[] _loss = {"event:/VO/Circus Forest/Minigame 1/vo_circusforest_minigame2_clown_lost_01", "event:/VO/Circus Forest/Minigame 2/vo_circusforest_minigame2_clown_lost_01", "event:/VO/Circus Forest/Minigame 3/vo_circusforest_minigame2_clown_lost_01"};
    // private string[] _after = { "event:/VO/Circus Forest/Minigame 1/vo_circusforest_minigame2_player_after_01", "event:/VO/Circus Forest/Minigame 2/vo_circusforest_minigame2_player_after_01", "event:/VO/Circus Forest/Minigame 3/vo_circusforest_minigame2_player_after_01" };


    private void Awake()
    {
        _min1 = GameObject.Find("Minigame1-Origin");
        _min2 = GameObject.Find("Minigame2-Origin");
        _min3 = GameObject.Find("Minigame3-Origin");
    }
    public void MinigameComplete()
    {
        currentMinigame++;
    }
    public void Suicide()
    {
        gameObject.SetActive(false);
    }
    public void Welcome()
    {
        switch (currentMinigame)
        {
            case 0:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame1_clown_welcome_01", _min1);
                break;
            case 1:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame2_clown_welcome_01", _min2);
                break;
            case 2:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame3_clown_welcome_01", _min3);
                break;
        }
    }

    public void Explanation()
    {
        switch (currentMinigame)
        {
            case 0:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame1_clown_explanation_01", _min1);
                break;
            case 1:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame2_clown_explanation_01", _min2);
                break;
            case 2:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame3_clown_explanation_01", _min3);
                break;
        }
    }

    public void Win()
    {
        switch (currentMinigame)
        {
            case 0:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame1_clown_won_01", _min1);
                break;
            case 1:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame2_clown_won_01", _min2);
                break;
            case 2:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame3_clown_won_01", _min3);
                break;
        }
    }

    public void Loss()
    {
        switch (currentMinigame)
        {
            case 0:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame1_clown_lost_01", _min1);
                break;
            case 1:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame2_clown_lost_01", _min2);
                break;
            case 2:
                SubtitleSystem.PlayVoiceLineFrom("vo_circusforest_minigame3_clown_lost_01", _min3);
                break;
        }
    }
}
