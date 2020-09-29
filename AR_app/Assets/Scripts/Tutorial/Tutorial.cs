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
    [SerializeField] private Image _staticFon;
    [SerializeField] private Image _menuFon;
    [Space]
    [SerializeField] private RectTransform _tutorialElementsParent;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _ARScreen;
    [SerializeField] private TutorialButton _slideButton;
    [SerializeField] private TutorialButton _hideButton;
    [SerializeField] private TutorialButton _projectsButton;
    [SerializeField] private TutorialButton _backToMenuProjectButton;
    [SerializeField] private TutorialButton _aboutButton;
    [SerializeField] private TutorialButton _backToMenuAboutButton;
    [SerializeField] private TutorialButton _ARButton;
    [SerializeField] private TutorialButton _backToMenuARButton;
    [SerializeField] private TutorialButton _contactsButton;
    [SerializeField] private TutorialButton _backToMenuContactsButton;

    [SerializeField] private TutorialButton _buttonToURL;

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
        Tutorial, Passed
    }

    private void Start()
    {
        _slideButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideTutorial();
        });

        _hideButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            HideTutorial();
        });

        _projectsButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideToProjects();
        });

        _backToMenuProjectButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideProjectsToMenu();
        });

        _aboutButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideToAbout();
        });

        _backToMenuAboutButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideAboutToMenu();
        });


        _ARButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideToARTutorial();
        });

        _backToMenuARButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideARToMenu();
        });


        _contactsButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideToContacts();
        });

        _backToMenuContactsButton.AddListener(() =>
        {
            if (_inTransitionState) return;
            SlideContactsToMenu();
        });

        _buttonToURL.AddListener(() =>
        {
            GoToSite();
        });

        

    }

    private void OnDestroy()
    {
        _slideButton.RemoveAllListeners();
        _hideButton.RemoveAllListeners();
    }

    private void GoToSite()
    {
        Application.OpenURL("http://forma-decor.ru/");
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
            () => { _startScreen.SetActive(false); _ARScreen.SetActive(true); _staticFon.enabled = false; _menuFon.enabled = true; _inTransitionState = false;  }
            ));

        _state++;
    }

    private void SlideToProjects()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.y;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        //_state++;
    }
    private void SlideProjectsToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.y;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, -movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        //_state++;
    }



    private void SlideToAbout()
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

        //_state++;
    }
    private void SlideAboutToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.x;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(-movingDistance, 0f, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        //_state++;
    }


    private void SlideToARTutorial()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.x;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(-movingDistance, 0f, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        //_state++;
    }
    private void SlideARToMenu()
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

        //_state++;
    }



    private void SlideToContacts()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.y;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, -movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        //_state++;
    }
    private void SlideContactsToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.y;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));

        //_state++;
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
