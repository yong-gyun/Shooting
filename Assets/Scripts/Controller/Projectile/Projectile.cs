using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : InitBase
{
    public Action<Projectile> OnProjectileEvent = null;
    private float _damage;
    private float _speed;
    private string _targetTag;

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        gameObject.SetActive(false);
        return true;
    }

    public void SetInfo(float damage, float speed, string targetTag = "Player")
    {
        _damage = damage;
        _speed = speed;
        _targetTag = targetTag;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_init == false)
            return;

        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag) == true)
        {
            other.GetComponent<PlayerStatus>().OnDamaged(_damage);
            Destroy(gameObject);
        }
    }

    public void OnNext()
    {
        if (OnProjectileEvent != null)
            OnProjectileEvent.Invoke(this);
    }
}
