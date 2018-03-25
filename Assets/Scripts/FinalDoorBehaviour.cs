using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorBehaviour : MonoBehaviour {

	public Material[] keyHoleMaterials;
	public List<Colors> colorList;
	public GameObject[] locks;

	private int locksDone;
	private Animator animController;
	private bool isOpen;
	private static int playersDoneCounter = 0;

	public void Init()
	{
		locksDone = 0;
		isOpen = false;
		animController = GetComponent<Animator>();
		for(int i = 0; i < locks.Length; i++)
		{
			locks[i].GetComponent<MeshRenderer>().material = keyHoleMaterials[(int)colorList[i]];
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			if(colorList.Contains(player.Color))
			{
				locksDone++;
				if(locksDone == colorList.Count)
				{
					animController.SetTrigger("openDoor");
					isOpen = true;
				}
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag.Equals("Player") && isOpen)
		{
			playersDoneCounter++;
			if (playersDoneCounter < 3) return;
			GameManager.INSTANCE.ReturnToMainMenu();
		}
	}
}
