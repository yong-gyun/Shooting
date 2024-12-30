using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Action<Projectile> OnProjectileEvent = null;
    protected float _damage;
    protected float _speed;
    protected string _targetTag;
    protected BaseController _owner;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetInfo(BaseController owner, float speed = 12f)
    {
        _owner = owner;
        _damage = owner.Status.Attack;
        _speed = speed;
        _targetTag = owner.TargetTag;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag) == true)
        {
            other.GetComponent<PlayerStatus>().OnDamaged(_owner.Status);
            Destroy(gameObject);
        }
    }

    public void OnNext()
    {
        if (OnProjectileEvent != null)
            OnProjectileEvent.Invoke(this);
    }
}
