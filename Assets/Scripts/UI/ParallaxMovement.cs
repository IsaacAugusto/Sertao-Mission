using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ParallaxMovement : MonoBehaviour
{
    [SerializeField] private float _absolutVelocity;
    [SerializeField] [Range(0.0f, 1.0f)] private float _relativeVelocityMultiplier;
    private RectTransform _imageRectTransform;
    private Vector3 _initialPos;
    private float _canvasWidth;

    void Start()
    {
        _imageRectTransform = GetComponent<RectTransform>();
        _initialPos = _imageRectTransform.localPosition;

        RectTransform canvasRectTransform = GameObject.FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        _canvasWidth = canvasRectTransform.rect.width;
    }

    void FixedUpdate()
    {
        _imageRectTransform.Translate(Vector3.left * _absolutVelocity * _relativeVelocityMultiplier);
        if (- _imageRectTransform.localPosition.x >= _imageRectTransform.rect.width * 0.5f)
        {
            _imageRectTransform.localPosition = _initialPos;
        }
    }
}
