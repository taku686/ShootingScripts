using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy[] enemies;
    private EnemyGroup enemyGroup;
    public GameObject enemyTag;//Text3Dのprefabを割り当て
    // Use this for initialization
    void Start()
    {
        enemyGroup = new EnemyGroup(enemies.Length);
        Iterator it = enemyGroup.iterator();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyGroup.AddEnemy(enemies[i]);
        }
        while (it.HasNext())
        {
            var e = it.Next();
            GenerateEnemy((Enemy)e);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateEnemy(Enemy enemy)
    {
        GameObject g = null;
        switch (enemy.getEnemyType())
        {
            case EnemyType.Weak:
                g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case EnemyType.Normal:
                g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case EnemyType.Strong:
                g = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
        }
        g.transform.position = new Vector3Int(Random.Range(-5, 5), Random.Range(-5, 5), 0);
    //    Instantiate(enemyTag, g.transform);
   //     enemyTag.GetComponent<TextMesh>().text = enemy.getName() + "\nHP:" + enemy.getHP() + "\nMP:" + enemy.getMP();

    }
}
