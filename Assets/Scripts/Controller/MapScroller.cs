using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    private Material _background;
    private Vector3 _offset;
    private float _speed = 0.2f;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        _background = renderer.material;
    }

    private void Update()
    {
        Vector3 offset = _background.mainTextureOffset;
        offset += Vector3.down * _speed * Time.deltaTime;
        _offset = offset;
        _background.mainTextureOffset = _offset;
    }
}
