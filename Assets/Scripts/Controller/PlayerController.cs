using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField] private List<Transform> _firePos = new List<Transform>();
    private PlayerStatus _playerStatus;
    private float _attackTime;
    private int _attackIndex;

    protected override void Start()
    {
        base.Start();
        TargetTag = "Monster";

        _playerStatus = Status as PlayerStatus;
        _playerStatus.SetInfo();
        Init();
    }

    public override void Init()
    {
        _playerStatus.SetFullCondition();
    }

    private void Update()
    {
        if (_playerStatus.Hp <= 0f)
            return;

        if (Managers.Stage.IsStageStarted == true)
        {
            _playerStatus.Fuel -= Time.deltaTime * 0.75f;
            if (_playerStatus.Fuel <= 0f)
            {
                _playerStatus.OnDead();
            }
        }

        UpdateMove();
        UpdateAttack();
    }

    private void UpdateMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Dir = new Vector3(horizontal, 0f, vertical);
        if (horizontal > 0f)
        {
            Quaternion qua = Quaternion.Euler(0f, 0f, -20f);
            _model.transform.rotation = Quaternion.Lerp(_model.transform.rotation, qua, 10f * Time.deltaTime);
        }
        else if (horizontal < 0f)
        {
            Quaternion qua = Quaternion.Euler(0f, 0f, 20f);
            _model.transform.rotation = Quaternion.Lerp(_model.transform.rotation, qua, 10f * Time.deltaTime);
        }
        else
        {
            _model.transform.rotation = Quaternion.Lerp(_model.transform.rotation, Quaternion.identity, 10f * Time.deltaTime);
        }

        transform.position += Dir * _playerStatus.MoveSpeed * Time.deltaTime;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x > 1f)
            viewPos.x = 1f;
        if (viewPos.x < 0f)
            viewPos.x = 0f;

        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }

    private void UpdateAttack()
    {
        if (_attackTime > 0f)
            _attackTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            if (_attackTime <= 0f)
                OnAttack();
        }
    }

    private void OnAttack()
    {
        _attackTime = _playerStatus.AttackSpeed;
        for (int i = 0; i < _playerStatus.FireIndex; i++)
        {
            Transform t = _firePos[i];
            Projectile projectile = Managers.Object.CreateProjectile("Player_Projectile", true);
            projectile.transform.position = t.position;
            projectile.SetInfo(this);
        }
    }
}