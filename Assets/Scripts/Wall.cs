using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Transform newWall;
    [SerializeField] Transform newObject;
    [SerializeField] string wallName;
    [SerializeField] string wallDadName = "WallDad";
    [SerializeField] GameObject spike;
    [SerializeField] GameObject coin;
    [SerializeField] LayerMask spikeLayer;

    private Object wallObj;
    private Transform player;
    private bool madeWall;
    private bool madeObject;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        transform.SetParent(GameObject.Find(wallDadName).transform);

        wallObj = Resources.Load(wallName);

        if(!Physics2D.OverlapCircle(newObject.position, 3, spikeLayer) && Random.Range(0, 100) < 25){
            var temp = Instantiate(spike, newObject.position, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            temp.transform.SetParent(transform);
            GameObject.Destroy(newObject.gameObject);
        } else if(Random.Range(0, 100) < 5){
            var temp = Instantiate(coin, newObject.position, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            temp.transform.SetParent(transform);
            GameObject.Destroy(newObject.gameObject);
        }
    }

    void Update()
    {
        if(!madeWall && Vector3.Distance(transform.position, player.position) < 20){
            var temp = Instantiate(wallObj, newWall.position, Quaternion.identity);
            GameObject.Destroy(newWall.gameObject);
            madeWall = true;
        }

        if(Vector3.Distance(transform.position, player.position) > 25 && transform.position.y < player.position.y){
            GameObject.Destroy(gameObject);
        }
    }
}
