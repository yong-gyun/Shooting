using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnData
{
    public int stage = 0;
    public float destTime = 0f;
    public float spawnInterval = 0f;
    public List<SpawnPointData> spawnPoints = new List<SpawnPointData>();
    public List<Define.MonsterType> spawnMonsters = new List<Define.MonsterType>();
}

[Serializable]
public class SpawnPointData
{
    public Transform point;
    public bool isSpawned;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnData> _spawnDatas = new List<SpawnData>();
    private SpawnData _currentSpawnData;
    private Coroutine _coroutine;
    private float _currentTime;

    private void Start()
    {
        Managers.Stage.OnStageStartEvent -= StartSpawn;
        Managers.Stage.OnStageStartEvent += StartSpawn;

    }

    public void StartSpawn(int stage)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _currentSpawnData = _spawnDatas[stage - 1];
        _coroutine = StartCoroutine(CoSpawning());
    }

    private void Update()
    {
        if (Managers.Stage.IsStageStarted)
        {
            _currentTime += Time.deltaTime;
        }
    }

    IEnumerator CoSpawning()
    {
        _currentTime = 0f;
        while (_currentTime < _currentSpawnData.destTime)
        {
            WaitForSeconds delay = new WaitForSeconds(_currentSpawnData.spawnInterval);

            if (CheckFullSpawned() == true)
            {
                var monsters = _currentSpawnData.spawnMonsters.Where(type => Managers.Object.CheckFixPosMosnter(type) == false).ToList();
                Define.MonsterType monsterType = monsters[Random.Range(0, monsters.Count)];
                
                int spawnPoint = Random.Range(0, _currentSpawnData.spawnPoints.Count);
                SpawnPointData info = _currentSpawnData.spawnPoints[spawnPoint];
                MonsterController mc = Managers.Object.SpawnMonster(monsterType, info.point);
            }
            else
            {
                var spawnPoints = _currentSpawnData.spawnPoints.Where(info => info.isSpawned == false).ToList();
                int spawnPoint = Random.Range(0, spawnPoints.Count);
                SpawnPointData info = _currentSpawnData.spawnPoints[spawnPoint];

                Define.MonsterType monsterType = _currentSpawnData.spawnMonsters[Random.Range(0, _currentSpawnData.spawnMonsters.Count)];
                MonsterController mc = Managers.Object.SpawnMonster(monsterType, info.point);

                if (Managers.Object.CheckFixPosMosnter(monsterType) == true)
                {
                    info.isSpawned = true;
                    mc.Status.OnDeadEvent += () =>
                    {
                        SpawnPointData spawnPointData = info;
                        spawnPointData.isSpawned = true;
                    };
                }
                
            }

            yield return delay;
        }

        //TODO 보스 스폰
    }

    private bool CheckFullSpawned()
    {
        bool result = true;
        foreach (SpawnPointData pointData in _currentSpawnData.spawnPoints)
        {
            result &= pointData.isSpawned;
        }

        return result;
    }

    public void Clear()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = null;
    }
}