using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScoreStarAlphaUpdate : MonoBehaviour
{
    [SerializeField] private bool _loadScoreFromSaveFile;
    [SerializeField] private int _levelNumber;
    [SerializeField] private int _scoreStarIndex;
    [SerializeField] [Range(0.0f, 1.0f)] private float _onEnabledAlhpa;
    private ShipBehaviour _player;
    private Image _scoreStarImage;

    private void OnEnable()
    {
        _player = GameObject.FindObjectOfType<ShipBehaviour>();
        _scoreStarImage = GetComponent<Image>();
        if (_loadScoreFromSaveFile)
        {
            if (GetHighestScore() >= _scoreStarIndex)
            {
                UpdateImageAlpha(_onEnabledAlhpa);
            }
        }
        else if (GetCurrentScore() >= _scoreStarIndex)
        {
            UpdateImageAlpha(_onEnabledAlhpa);
        }
    }

    private void UpdateImageAlpha(float intensity)
    {
        var tempColor = _scoreStarImage.color;
        tempColor.a = intensity;
        _scoreStarImage.color = tempColor;
    }

    private int GetHighestScore()
    {
        return GameManager.Instance.GetHighestScoreOfIndex(_levelNumber - 1);
    }

    private int GetCurrentScore()
    {
        if (_player != null)
        {
            return _player.GetCurrentScore();
        }
        else return 0;
    }
}
