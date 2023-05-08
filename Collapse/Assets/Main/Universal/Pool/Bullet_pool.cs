using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet_pool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int amountStart;
    public int pool_size = 0;

    private List<GameObject> bulletPool = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < amountStart; i++) 
        {
            createBullet();
        }
    }

    GameObject createBullet() 
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bulletPool.Add(bullet);
        pool_size++;
        return bullet;
    }

    public GameObject getBullet() 
    {
        GameObject tempBullet = null;

        tempBullet = bulletPool.Find(i => i.activeInHierarchy == false);

        if(tempBullet == null) 
        {
            tempBullet = createBullet();
        }

        tempBullet.SetActive(true);

        return tempBullet;
    }
}
