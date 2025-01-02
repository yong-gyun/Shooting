using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectManager
{
    public Transform Root
    {
        get
        {
            if (_root == null)
            {
                _root = new GameObject("@Object_Root").transform;
            }

            return _root;
        }
    }

    public Transform MonsterProjectileRoot { get { return GetParent("MonsterProjectile_Root", ref _monsterProjectileRoot); } }
    public Transform PlayerProjectileRoot { get { return GetParent("PlayerProjectile_Root", ref _playerProjectileRoot); } }
    public Transform MonsterRoot { get { return GetParent("Monster_Root", ref _monsterRoot); } }
    public PlayerController Player { get; private set; }

    private Transform _root;
    private Transform _monsterProjectileRoot;
    private Transform _playerProjectileRoot;
    private Transform _monsterRoot;

    public PlayerController SpawnPlayer()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Creature/Player");
        GameObject go = Object.Instantiate(prefab);

        PlayerController pc = go.GetComponent<PlayerController>();
        Player = pc;
        return pc;
    }

    public MonsterController SpawnMonster(Define.MonsterType type, Transform spawnPos)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefab/Creature/Monster/{type}");
        GameObject go = Object.Instantiate(prefab, MonsterRoot);

        MonsterController mc = go.GetComponent<MonsterController>();
        mc.SetInfo(spawnPos);
        return mc;
    }

    public Projectile CreateProjectile(string name, bool isPlayer = false, Action<Projectile> callback = null)
    {
        Transform root = isPlayer == true ? PlayerProjectileRoot : MonsterProjectileRoot;
        GameObject prefab = Resources.Load<GameObject>($"Prefab/Projectile/{name}");
        GameObject go = Object.Instantiate(prefab, root);
        
        Projectile projectile = go.GetComponent<Projectile>();
        if (projectile != null && callback != null)
        {
            projectile.OnProjectileEvent += callback;
        }

        return projectile;
    }

    public Transform GetParent(string name, ref Transform transform)
    {
        if (transform != null)
        {
            return transform;
        }

        GameObject go = new GameObject(name);

        go.transform.parent = Root;
        transform = go.transform;
        return transform;
    }

    public bool CheckFixPosMosnter(Define.MonsterType type)
    {
        switch (type)
        {
            case Define.MonsterType.Dron:
                return true;
        }

        return false;
    }
}
