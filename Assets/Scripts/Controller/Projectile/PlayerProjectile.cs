using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    [SerializeField] private List<GameObject> _upgradeObjects = new List<GameObject>();

    public override void SetInfo(BaseController owner, float speed = 12)
    {
        PlayerStatus status = Managers.Object.Player.Status as PlayerStatus;
        int index = Mathf.Abs(status.FireIndex - status.UpgradeAttackCount);
        for (int i = 0; i < _upgradeObjects.Count; i++)
        {
            if (i == index)
            {
                _upgradeObjects[i].SetActive(true);
            }
            else
            {
                _upgradeObjects[i].SetActive(false);
            }
        }

        base.SetInfo(owner, speed);
    }
}
