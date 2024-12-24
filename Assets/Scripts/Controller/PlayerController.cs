using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private float _attackTime;
    private PlayerStatus _playerStatus;

    protected override void Start()
    {
        base.Start();
        _playerStatus = Status as PlayerStatus;
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
            //È¸Àü
        }
        else if (horizontal < 0f)
        {

        }
        else
        {

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
    }
}
