//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class Laser_pool 
//{
//    public ParticleSystem bulletPrefab;
//    public int amountStart;
//    public int pool_size = 0;

//    //private List<GameObject> bulletPool = new List<GameObject>();
//    private List<ParticleSystem> bulletPool = new List<ParticleSystem>();

//    private void Start()
//    {
//        for(int i = 0; i < amountStart; i++) 
//        {
//            createBullet();
//        }
//    }

//    ParticleSystem createBullet() 
//    {
//        ParticleSystem bullet = Instantiate(bulletPrefab);
//        bullet.SetActive(false);
//        bulletPool.Add(bullet);
//        pool_size++;
//        return bullet;
//    }

//    public GameObject getBullet() 
//    {
//        GameObject tempBullet = null;

//        tempBullet = bulletPool.Find(i => i.activeInHierarchy == false);

//        if(tempBullet == null) 
//        {
//            tempBullet = createBullet();
//        }

//        tempBullet.SetActive(true);

//        return tempBullet;
//    }
//}
