using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTarget : MonoBehaviour
{
    [SerializeField] private DefaultTrackableEventHandler _defaultTrackableEventHandler;
    [SerializeField] private Transform _snowman;
    [SerializeField] private Transform _imageTarget;

    private Vector3 _targetPos;
    private Quaternion _targetRot;

    private void Start()
    {
        _defaultTrackableEventHandler.OnTargetLost.AddListener(() =>
        {
            if (_snowman.parent != transform)
            {
                _snowman.parent = transform;
                _targetPos = _snowman.localPosition;
                _targetRot = _snowman.localRotation;

                var renderers = _snowman.GetComponentsInChildren<MeshRenderer>(true);

                foreach (var item in renderers)
                    if (!item.enabled) 
                        item.enabled = true;
            }
        });

        _defaultTrackableEventHandler.OnTargetFound.AddListener(() =>
        {
            if (_snowman.parent != _imageTarget)
            {
                _snowman.parent = _imageTarget;
                _snowman.localPosition = _targetPos;
                _snowman.localRotation = _targetRot;
            }
        });
    }
}
