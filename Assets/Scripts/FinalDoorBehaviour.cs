using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorBehaviour : MonoBehaviour {

    [Tooltip("order: yellow,red,green,blue")]
	public Material[] bottomMaterials;
    [Tooltip("order: yellow,red,green,blue")]
    public Material[] topMaterials;
    public List<Colors> colorList;
	public GameObject[] bottomParts;
    public GameObject[] topParts;

	private int locksDone;
	private Animator animController;
	private bool isOpen;
	private static int playersDoneCounter = 0;
    private List<GameObject> triggerList;

	public void Init()
	{
        triggerList = new List<GameObject>();
		locksDone = 0;
		isOpen = false;
		animController = GetComponent<Animator>();
		for(int i = 0; i < bottomParts.Length; i++)
		{
            //color each part in the right color; if there is only one color in the list, give all parts that color
            bottomParts[i].GetComponent<MeshRenderer>().material = colorList.Count>1 ? bottomMaterials[(int)colorList[i]] : bottomMaterials[(int)colorList[0]];
            topParts[i].GetComponent<MeshRenderer>().material = colorList.Count > 1 ? topMaterials[(int)colorList[i]] : topMaterials[(int)colorList[0]];
			//parts[i].GetComponent<MeshRenderer>().material = partsMaterials[(int)colorList[i]];
		}
        
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			if(!triggerList.Contains(other.gameObject))
			{
                triggerList.Add(other.gameObject);
                locksDone = 0;
                foreach(GameObject trigger in triggerList)
                {
			        PlayerController player = other.gameObject.GetComponent<PlayerController>();
                    if(colorList.Contains(player.Color) && player.HasKey)
                    {
                        locksDone++;
                    }
                }
                if(locksDone>=colorList.Count)
                {
                    StartCoroutine("endGame");
                }
			}
            
		}
	}
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            if(triggerList.Contains(other.gameObject))
            {
                triggerList.Remove(other.gameObject);
            }
        }
    }

    private IEnumerator endGame()
    {
        //animController.SetTrigger("openDoor");
        AudioManager.INSTANCE.PlayWinSound();
        foreach (GameObject trigger in triggerList)
        {
            PlayerController player = trigger.gameObject.GetComponent<PlayerController>();
            player.animator.SetTrigger("playerWin");
        }
        yield return new WaitForSeconds(3f);

        GameManager.INSTANCE.GameOver(colorList);
    }

    /*private void OnTriggerStay(Collider other)
	{
        isOpen = false;
		if(other.gameObject.tag.Equals("Player") && isOpen)
		{
			playersDoneCounter++;
			if (playersDoneCounter < 3) return;
			GameManager.INSTANCE.ReturnToMainMenu();
		}


	}*/


}
