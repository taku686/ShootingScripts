using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public interface Iterator
    {
        bool HasNext();
        System.Object Next();
    }
    public interface Aggregate
    {
        Iterator iterator();
    }
    public enum EnemyType
    {
        Weak, Normal, Strong
    }
    [System.Serializable]
    public class Enemy
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int HP;
        [SerializeField]
        private int MP;
        [SerializeField]
        private EnemyType enemyType;
        public Enemy(string name, int HP, int MP, EnemyType enemyType)
        {
            this.name = name;
            this.HP = HP;
            this.MP = MP;
            this.enemyType = enemyType;
        }
        public string getName()
        {
            return name;
        }
        public int getHP()
        {
            return HP;
        }
        public int getMP()
        {
            return MP;
        }
        public EnemyType getEnemyType()
        {
            return enemyType;
        }
    }
    public class EnemyGroup : Aggregate
    {
        private Enemy[] enemies;
        private int last = 0;
        public EnemyGroup(int maxSize)
        {
            this.enemies = new Enemy[maxSize];
        }
        public void AddEnemy(Enemy enemy)
        {
            this.enemies[last] = enemy;
            last++;
        }
        public int GetLength()
        {
            return enemies.Length;
        }
        public Enemy GetEnemyAt(int index)
        {
            return enemies[index];
        }
        public Iterator iterator()
        {
            return new EnemyGroupIterator(this);
        }
    }
    public class EnemyGroupIterator : Iterator
    {
        private EnemyGroup enemyGroup;
        private int index;
        public EnemyGroupIterator(EnemyGroup enemyGroup)
        {
            this.enemyGroup = enemyGroup;
            this.index = 0;
        }
        public bool HasNext()
        {
            if (index < enemyGroup.GetLength()) return true;
            return false;
        }
        public System.Object Next()
        {
            Enemy enemy = enemyGroup.GetEnemyAt(index);
            index++;
            return enemy;
        }
    }
