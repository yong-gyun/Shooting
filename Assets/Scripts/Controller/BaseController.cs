using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Define.CreatureType CreatureType { get; set; }
    public BaseStatus Status { get; private set; }
    public string TargetTag { get; protected set; }

    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }        //정규화된 벡터 리턴
    protected Vector3 _dir;

    [SerializeField] protected GameObject _model;
    [SerializeField] protected GameObject _hitEffect;
    [SerializeField] protected GameObject _deadEffect;

    protected virtual void Start()
    {
        Status = GetComponent<BaseStatus>();

        Status.OnDamagedEvent -= OnDamagedEvent;
        Status.OnDamagedEvent += OnDamagedEvent;

        Status.OnDeadEvent -= OnDeadEvent;
        Status.OnDeadEvent += OnDeadEvent;
    }

    public virtual void Init()
    {
        Status.Damageable = true;
    }

    public virtual void Clear()
    {
        Status.Damageable = false;
    }

    public virtual void OnDamagedEvent(GameObject attacker)
    {
        GameObject go = Instantiate(_hitEffect, attacker.transform.position, Quaternion.identity, transform);
        Destroy(go, 1f);
    }

    public virtual void OnDeadEvent()
    {
        GameObject go = Instantiate(_deadEffect, transform);
        Destroy(go, 1f);
    }
}