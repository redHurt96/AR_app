using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialState _state;

    [Space]
    [SerializeField] private RectTransform _tutorialElementsParent;
    [SerializeField] private TutorialButton _slideButton;

    [Space]
    [SerializeField] private AnimationCurve _slideCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space]
    [SerializeField] private CanvasScaler _canvasScaler;

    [Space]
    [SerializeField] private float _slideTime = .5f;
    [SerializeField] private float _fadeTime = .5f;

    [Space]
    [SerializeField] private GameObject _cameraUI;

    private bool _inTransitionState = false;

    private enum TutorialState
    { 
        FirstPage, SecondPage, Passed
    }

    private void Start()
    {
        _slideButton.AddListener(OnTutorialButtonClick);
    }

    private void OnDestroy()
    {
        _slideButton.RemoveListener(OnTutorialButtonClick);
    }

    private void OnTutorialButtonClick()
    {
        if (_inTransitionState) return;

        switch (_state)
        {
            case TutorialState.FirstPage:
                SlideTutorial();
                break;
            case TutorialState.SecondPage:
                HideTutorial();
                break;
            case TutorialState.Passed:
                break;
            default:
                break;
        }
    }

    private void SlideTutorial()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.x;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(movingDistance, 0f, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        _state++;
    }

    private void HideTutorial()
    {
        _inTransitionState = true;

        UITransitions.Fade(new FadeUITransition(
            GetComponent<CanvasGroup>(),
            1f,
            0f,
            _fadeCurve,
            _fadeTime,
            () =>
                {
                    _inTransitionState = false;
                    gameObject.SetActive(false);
                    _cameraUI.SetActive(true);
                }
            ));

        _state++;
    }
}
