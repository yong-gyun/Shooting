using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    [SerializeField] private Vector3 _dir;
    [SerializeField] private float _speed = 0f;

    private Material _background;
    private Vector3 _offset;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        _background = renderer.material;
    }

    private void Update()
    {
        Vector3 offset = _background.mainTextureOffset;
        offset += _dir.normalized * _speed * Time.deltaTime;
        _offset = offset;
        _background.mainTextureOffset = _offset;
    }
}
