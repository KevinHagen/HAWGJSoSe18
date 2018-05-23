using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Colors color;
    public Box.Index index;
    public Texture[] textures;
    public float  maxDistanceToWall=3;
    //public float minFlyRange=15, maxFlyRange=25;
    //public float flightHight = 10,flightSpeed=2f;
    public float minFlyRange , maxFlyRange ;
    public float flightHight , flightSpeed ;
    public float forceMultiply ;
    public GameObject[] parts;

    public Rigidbody rb;

   

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
                other.GetComponent<PlayerAudioManager>().PlayPickUpKeySound();
                gameObject.SetActive(false);
            }
            
        }
    }

    public IEnumerator KeyFly()
    {
        transform.parent = null;
        List<Vector3> directions = new List<Vector3>();
        Vector3 destination = transform.position ;
        Vector3 currentDestination=new Vector3(0,0,0);
        Vector3 currentDirection = new Vector3(0, 0, 0);
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
        currentDirection = Vector3.Normalize(directions[Random.Range(0, directions.Count)])*Random.Range(minFlyRange,maxFlyRange);
        currentDirection.y = flightHight;
        currentDestination=transform.position+new Vector3(currentDirection.x/2,currentDirection.y/2,currentDirection.z);
        destination = transform.position + currentDirection;

        rb.AddForce(currentDirection);

        //while (Vector3.Distance(transform.position, destination) >= 0.2)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, currentDestination, flightSpeed);
        //    if (Vector3.Distance(transform.position, currentDestination) <= 0.2)
        //    {
        //        currentDestination = destination;
        //    }
        //    yield return new WaitForSeconds(0.05f);
        //}
    }
}
