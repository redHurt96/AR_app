using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(CanvasGroup))]
public class Tutorial : MonoBehaviour
{
    [Space] [SerializeField] private Image              _staticFon;
    [Space] [SerializeField] private RectTransform      _topPanelToRelocate;
            [SerializeField] private RectTransform      _botPanelToRelocate;
    [Space] [SerializeField] private ScrollRect         _projectsScrollRect;
            [SerializeField] private ScrollRect         _aboutScrollRect;
    
    [Space] [SerializeField] private RectTransform      _tutorialElementsParent;
            [SerializeField] private GameObject         _startScreen;
            [SerializeField] private GameObject         _projectsScreen;
            [SerializeField] private GameObject         _cameraScreen;
            [SerializeField] private TutorialButton     _slideButton;
            [SerializeField] private TutorialButton     _hideButton;
            [SerializeField] private TutorialButton      _hideButton2;
            [SerializeField] private TutorialButton     _projectsButton;
            [SerializeField] private TutorialButton     _backToMenuProjectButton;
            [SerializeField] private TutorialButton     _aboutButton;
            [SerializeField] private TutorialButton     _backToMenuAboutButton;
            [SerializeField] private TutorialButton     _ARButton;
            [SerializeField] private TutorialButton     _backToMenuARButton;
            [SerializeField] private TutorialButton     _contactsButton;
            [SerializeField] private TutorialButton     _backToMenuContactsButton;

            [SerializeField] private TutorialButton     _buttonToURL;
            [SerializeField] private TutorialButton     _buttonToDownloadTags;
            [SerializeField] private TutorialButton     _buttonToDownloadTags2;

    [Space] [SerializeField] private AnimationCurve     _slideCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            [SerializeField] private AnimationCurve     _fadeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space] [SerializeField] private CanvasScaler       _canvasScaler;
            [SerializeField] private RectTransform      _canvasRT;

    [Space] [SerializeField] private float              _slideTime = .5f;
            [SerializeField] private float              _fadeTime = .5f;

    [Space] [SerializeField] private GameObject         _cameraUI;

    [Space] [SerializeField] private RectTransform      _bigButton;
            [SerializeField] private RectTransform      _parentOfBigButton;
            [SerializeField] private float              _bigButtonPercentSizeOfPhone = 0.8f;
            [SerializeField] private float              _bigButtonPercentSizeOfPads  = 0.7f;

                             private bool               _isPhone                     = true;
            [SerializeField] private GameObject[]       _phoneBlocks;
            [SerializeField] private GameObject[]       _tabletBlocks;

    private bool _inTransitionState = false;


    private void Start()
    {
        Application.targetFrameRate = 60;

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
        _hideButton2.AddListener(() =>
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

        _buttonToURL.AddListener(GoToSite);
        _buttonToDownloadTags.AddListener(DownloadTags);
        _buttonToDownloadTags2.AddListener(DownloadTags);

        RescaleVerticalBlocksPositions();
        float ratio = (float)Screen.height / (float)Screen.width;
        _isPhone = ratio > 1.55f;
        ResizeInterfaceToRatio();
    }

    private void ResizeInterfaceToRatio()
    {
        SetSize(_bigButton, new Vector2(_bigButton.sizeDelta.x, _parentOfBigButton.rect.height * (_isPhone ? _bigButtonPercentSizeOfPhone : _bigButtonPercentSizeOfPads)));

        foreach (var curr in _phoneBlocks) curr.SetActive(_isPhone);
        foreach (var curr in _tabletBlocks) curr.SetActive(!_isPhone);
    }
    
    private void SetSize(RectTransform self, Vector2 size)
    {
        Vector2 oldSize = self.rect.size;
        Vector2 deltaSize = size - oldSize;
 
        self.offsetMin = self.offsetMin - new Vector2(
            deltaSize.x * self.pivot.x,
            deltaSize.y * self.pivot.y);
        self.offsetMax = self.offsetMax + new Vector2(
            deltaSize.x * (1f - self.pivot.x),
            deltaSize.y * (1f - self.pivot.y));
    }

    public void SendEmail()
    {
        string url = string.Format("mailto:{0}?subject={1}&body={2}", "info@formadecor.ru", WWW.EscapeURL("Тема"), WWW.EscapeURL("Текст сообщения"));
        Application.OpenURL(url);
    }

    public void CallPhone()
    {
        Application.OpenURL("tel://+78005054702");
    }

    private void OnDestroy()
    {
        _slideButton.RemoveAllListeners();
        _hideButton.RemoveAllListeners();
    }

    private void RescaleVerticalBlocksPositions()
    {
        var rect = _canvasRT.rect;
        _topPanelToRelocate.offsetMin = new Vector2(_topPanelToRelocate.offsetMin.x, -rect.y * 2); //bot
        _topPanelToRelocate.offsetMax = new Vector2(_topPanelToRelocate.offsetMax.x, -rect.y * 2); //top
        _botPanelToRelocate.offsetMin = new Vector2(_botPanelToRelocate.offsetMin.x, rect.y * 2); //bot
        _botPanelToRelocate.offsetMax = new Vector2(_botPanelToRelocate.offsetMax.x, rect.y * 2); //top
    }

    public void BackToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasRT.rect.y * 2;

        _tutorialElementsParent.localPosition = _tutorialElementsParent.localPosition - new Vector3(0f, movingDistance, 0f);
        gameObject.SetActive(true);
        _cameraUI.SetActive(false);
        _staticFon.enabled = true;
        

        UITransitions.Fade(new FadeUITransition(
            gameObject.GetComponent<CanvasGroup>(),
            0f,
            1f,
            _fadeCurve,
            _fadeTime,
            () =>
            {
                _cameraScreen.SetActive(false);
                _inTransitionState = false;
            }
            ));
    }

    private void GoToSite()
    {
        Application.OpenURL("http://forma-decor.ru/");
    }
    
    private void DownloadTags()
    {
        Application.OpenURL("https://formadecor.ru/metki/");
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
            () => { _startScreen.SetActive(false); 
                _projectsScreen.SetActive(true); _inTransitionState = false;  }
            ));
    }

    private void SlideToARTutorial()
    {
        _inTransitionState = true;

        float movingDistance = _canvasRT.rect.y * 2;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, -movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => {  _inTransitionState = false; }
            ));
    }
    private void SlideARToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasRT.rect.y * 2;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => { _projectsScrollRect.normalizedPosition = Vector2.up;  _inTransitionState = false; }
            ));
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
            () => { _aboutScrollRect.normalizedPosition = Vector2.up; _inTransitionState = false; }
            ));
    }


    private void SlideToProjects()
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
    }
    private void SlideProjectsToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasScaler.referenceResolution.x;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(movingDistance, 0f, 0f),
            _slideCurve,
            _slideTime,
            () =>
            {
                _projectsScrollRect.normalizedPosition = Vector2.up;
                _inTransitionState = false;
            }));
    }



    private void SlideToContacts()
    {
        _inTransitionState = true;

        float movingDistance = _canvasRT.rect.y * 2;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));
    }
    private void SlideContactsToMenu()
    {
        _inTransitionState = true;

        float movingDistance = _canvasRT.rect.y * 2;

        UITransitions.SlideTo(new SlideUITransition(
            _tutorialElementsParent,
            _tutorialElementsParent.localPosition - new Vector3(0f, -movingDistance, 0f),
            _slideCurve,
            _slideTime,
            () => _inTransitionState = false
            ));
    }



    private void HideTutorial()
    {
        _inTransitionState = true;
        _cameraScreen.SetActive(true);

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
                    _staticFon.enabled = false;
                }
            ));
    }
}
