using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    [SerializeField] private List<GameObject> _upgradeObjects = new List<GameObject>();

    public override void SetInfo(BaseController owner, float speed = 25f)
    {
        PlayerStatus status = Managers.Object.Player.Status as PlayerStatus;
        int index = Mathf.Abs(status.FireIndex - status.UpgradeAttackCount);
        _upgradeObjects[index].SetActive(true);

        base.SetInfo(owner, speed);
    }
}
