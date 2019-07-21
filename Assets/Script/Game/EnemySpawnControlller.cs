using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControlller : MonoBehaviour {

    [Tooltip("产怪的等级")]
    [SerializeField]
    private string _level = "1";

    [Tooltip("产怪的总量")]
    [SerializeField]
    private int enemyCount = 60;

    [Tooltip("产怪的间隔时间")]
    [SerializeField]
    private float interval = 2f;

    private float timeCount = 0;

    private Transform[] SpawnPoints;

    private List<GameObject> Enemy = new List<GameObject>();




    // Use this for initialization
    void Start() {
        timeCount = 2f;
        Transform SpawnPoint = gameObject.transform.FindChild("SpawnPoints");
        SpawnPoints = SpawnPoint.GetComponentsInChildren<Transform>();
        StartCoroutine(Loaditem("Monster/Level" + _level));
    }
    IEnumerator Loaditem(string path)
    {
        Object[] obj = Resources.LoadAll(path, typeof(GameObject));
        for (int i = 0; i < obj.Length; i++)
        {
            Enemy.Add((GameObject)obj[i]);
            yield return 0;
        }
        yield return 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;

        }else
        {
            int liveEnemyCount = transform.childCount - 2;
            if (liveEnemyCount < enemyCount)
                Lean.LeanPool.Spawn(Enemy[Random.Range(0, Enemy.Count - 1)], SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)].position, Quaternion.identity, transform);
            timeCount = interval;

        }
	}
}
