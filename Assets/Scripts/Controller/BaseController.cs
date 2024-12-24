using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : InitBase
{
    public Define.CreatureType CreatureType { get; set; }
    public BaseStatus Status { get; private set; }
    
    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }        //정규화된 벡터 리턴
    protected Vector3 _dir;

    [SerializeField] protected GameObject _model;
    [SerializeField] protected ParticleSystem _hitEffect;
    [SerializeField] protected ParticleSystem _deadEffect;

    protected virtual void Start()
    {
        Status = GetComponent<BaseStatus>();

        Status.OnDamagedEvent -= OnDamagedEvent;
        Status.OnDamagedEvent += OnDamagedEvent;

        Status.OnDeadEvent -= OnDeadEvent;
        Status.OnDeadEvent += OnDeadEvent;
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Status.Damageable = true;
        return true;
    }

    public override bool Clear()
    {
        if (base.Clear() == false)
            return false;

        Status.Damageable = false;
        return true;
    }

    public virtual void OnDamagedEvent(float damage)
    {
        _hitEffect.Play();
    }

    public virtual void OnDeadEvent()
    {
        _deadEffect.Play();
        Managers.Object.Despawn(gameObject);
    }
}