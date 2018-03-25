using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Colors color;
    public Box.Index index;
    public Texture[] textures;
    public float  maxDistanceToWall=3;
    public float minFlyRange=3, maxFlyRange=5;
    public float flightHight = 5,flightSpeed=0.5f;
    public GameObject[] parts;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController.Color==color)
            {
                playerController.HasKey = true;
                playerController.PlayerUI.SetKeyColor(color);
                playerController.currentKey = this;
                gameObject.transform.parent = playerController.transform;
                gameObject.SetActive(false);
            }
            
        }
    }

    public IEnumerator KeyFly()
    {
        transform.parent = null;
        List<Vector3> directions = new List<Vector3>();
        Vector3 destination;
        Vector3 currentDestination=new Vector3(0,0,0);
        RaycastHit hit;

        for(int i=0;i<36;i++)
        {
            transform.Rotate(Vector3.up,10);
            Debug.DrawRay(transform.position, transform.forward, Color.green);
            if (Physics.Raycast(transform.position, transform.forward, out hit,maxDistanceToWall))
            {
                if(hit.transform.tag!="Player") //hit everything but player
                {
                    directions.Add(transform.forward);
                }
            }
        }
        if (directions.Count == 0) yield break ;
        destination = directions[Random.Range(0, directions.Count)]*Random.Range(minFlyRange,maxFlyRange);
        currentDestination=destination/2;
        currentDestination.y = flightHight;

        while(Vector3.Distance(transform.position,destination)>=0.2)
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
