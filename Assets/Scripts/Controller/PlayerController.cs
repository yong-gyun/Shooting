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
        _playerStatus = Status as PlayerStatus;
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _playerStatus.SetInfo();
        return true;
    }

    private void Update()
    {
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
        foreach (Transform t in _firePos)
        {
            Projectile projectile = Managers.Object.CreateProjectile("Player_Projectile", true);
            projectile.transform.position = t.position;
            projectile.SetInfo(_playerStatus.Attack, 5f, "Monster");
        }
    }
}