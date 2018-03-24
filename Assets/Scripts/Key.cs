using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Colors color;
    public Box.Index index;
    public Texture[] textures;
    public float  maxDistanceToWall=3;
    public float minFlyRange=3, maxFlyRange=5;
    public float flightHight = 5,flightSpeed=0.1f;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if(other.GetComponent<PlayerController>().Color==color)
            {
                other.GetComponent<PlayerController>().HasKey = true;
                gameObject.transform.parent = other.transform;
                gameObject.SetActive(false);
            }
            /*else
            {
                LevelGenerator.levelGenerator.ReplacePowerUp(index);
            }*/
            
        }
    }

    public IEnumerator KeyFly()
    {
        List<Vector3> directions = new List<Vector3>();
        Vector3 destination;
        Vector3 currentDestination=new Vector3(0,0,0);
        RaycastHit hit;

        for(int i=0;i<36;i++)
        {
            transform.Rotate(Vector3.up,10);
            if (Physics.Raycast(transform.position, transform.forward, out hit,maxDistanceToWall))
            {
                if(hit.transform.tag!="Player") //hit everything but player
                {
                    directions.Add(transform.forward);
                }
            }
        }

        destination = directions[Random.Range(0, directions.Count)]*Random.Range(minFlyRange,maxFlyRange);
        currentDestination=destination/2;
        currentDestination.y = flightHight;
        gameObject.SetActive(true);

        while(Vector3.Distance(transform.position,destination)<=0.2)
        {
            transform.position = Vector3.MoveTowards(transform.position,currentDestination,flightSpeed);
            if(Vector3.Distance(transform.position,currentDestination)<=0.2)
            {
                currentDestination = destination;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
