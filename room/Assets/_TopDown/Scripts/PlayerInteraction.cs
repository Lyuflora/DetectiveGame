using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dec
{

	public class PlayerInteraction : MonoBehaviour
	{
		public static PlayerInteraction m_Instance;
		public float nearRayDistance = 200f;
		public float rotateSpeed = 200;

		public AudioClip writingSound;

		public Transform objectViewer;

		public UnityEvent OnView;
		public UnityEvent OnFinishView;

		private Camera myCam;

		[Header("Viewing")]
		[SerializeField]
		private bool isViewing;
		[SerializeField]
		private bool canFinish;

		[SerializeField]
		private Interactables currentInteractable;
		private Item currentItem;
		private Vector3 originPosition;
		private Quaternion originRotation;

		private Vector3 origionalScale;

		//private AudioPlayer audioPlayer;
		private PlayerInventory inventory;

		private void Awake()
		{
			//audioPlayer = GetComponent<AudioPlayer>();
			inventory = GetComponent<PlayerInventory>();
			m_Instance = this;
		}

		public Item GetCurrentItem()
        {
			return currentItem;

		}

		void Start()
		{
			myCam = Camera.main;
		}

		void Update()
		{
			CheckInteractables();
		}

		void CheckInteractables()
		{

			if (isViewing)
			{
				if (currentInteractable.itemInfo.grabbable && Input.GetMouseButton(0))
				{
					RotateObject();
				}

				if (canFinish && Input.GetMouseButtonDown(1))
				{
					FinishView();
				}

				return;
			}

			RaycastHit hit;
			Vector3 rayOrigin = myCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
			Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				Interactables interactable = hit.collider.GetComponent<Interactables>();
				if (interactable != null)
				{
					Debug.Log("Hit interactables");
					UIManager.m_Instance.SetHandCursor(true);
					if (Input.GetMouseButtonDown(0))
					{
						if (interactable.isMoving)
						{
							return;
						}

						currentInteractable = interactable;

						currentInteractable.OnInteract.Invoke();
						Debug.Log("Current interaction");

						if (currentInteractable.itemInfo != null)
						{
							OnView.Invoke();

							isViewing = true;

							bool hasPreviousItem = false;

							/*
							for (int i = 0; i < currentInteractable.previousItem.Length; i++)
							{
								if (inventory.itens.Contains(currentInteractable.previousItem[i].requiredItem))
								{
									Interact(currentInteractable.previousItem[i].interactionItem);
									currentInteractable.previousItem[i].OnInteract.Invoke();
									hasPreviousItem = true;
									break;
								}
							}

							if (hasPreviousItem)
							{
								return;
							}
							*/

							Interact(currentInteractable.itemInfo);

							if (currentInteractable.itemInfo.grabbable)
							{
								originPosition = currentInteractable.transform.position;
								originRotation = currentInteractable.transform.rotation;
								StartCoroutine(MovingObject(currentInteractable, objectViewer.position, 6f));
                            }
                            else
                            {
								// Add Clue
								// The clue on ungrabbable item can be found on item
								Debug.Log("Trigger Item Dialog");
                                if (currentInteractable.itemInfo)
                                {
									//TestAPP.m_Instance.m_ClueManager.AddClue(m_Clue);
									for(int i=0; i< currentInteractable.itemInfo.clueList.Count; i++)
                                    {
										currentInteractable.itemInfo.clueList[i].isFound = true;

									}
									
								}
								
							}
							UIManager.m_Instance.RefreshInventory();
						}


					}
				}
				else
				{
					UIManager.m_Instance.SetHandCursor(false);
				}
			}
			else
			{
				UIManager.m_Instance.SetHandCursor(false);
			}

		}

		void Interact(Item item)
		{
			currentItem = item;

			if (item.image != null)
			{
				//UIManager.m_Instance.SetImage(item.image);
			}

			//audioPlayer.PlayAudio(item.audioClip);
			// UIManager.m_Instance.SetCaptions(item.text);
			// Invoke("CanFinish", item.audioClip.length + 0.5f);
			Invoke("CanFinish", 0.5f);
		}

		void CanFinish()
		{
			canFinish = true;

			if (currentItem.image == null && !currentItem.grabbable)
			{
				FinishView();
			}
			else
			{
				UIManager.m_Instance.SetBackImage(true);
			}

			// UIManager.m_Instance.SetCaptions("");
		}

		void FinishView()
		{
			canFinish = false;
			isViewing = false;
			UIManager.m_Instance.SetBackImage(false);

			if (currentItem.inventoryItem)
			{
				inventory.AddItem(currentItem);
				//audioPlayer.PlayAudio(writingSound);
				currentInteractable.CollectItem.Invoke();
			}
			if (currentItem.grabbable)
			{
				currentInteractable.transform.rotation = originRotation;
				StartCoroutine(MovingObject(currentInteractable, originPosition, 1f));
			}

			if (currentItem.requiredItem)
			{
				inventory.AddRequiredItens(currentItem);
			}

			OnFinishView.Invoke();
		}

		IEnumerator MovingObject(Interactables obj, Vector3 position, float scaleFactor)
		{
			obj.isMoving = true;
			float timer = 0;
			if(isViewing)
            {
				origionalScale = obj.transform.localScale;
			}
			
			while (timer < 1)
			{
				obj.transform.position = Vector3.Lerp(obj.transform.position, position, Time.deltaTime * 5);
				obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, origionalScale * scaleFactor, Time.deltaTime * 5);
				timer += Time.deltaTime;
				yield return null;
			}

			obj.transform.position = position;
			obj.isMoving = false;
		}

		void RotateObject()
		{
			float x = Input.GetAxis("Mouse X");
			float y = Input.GetAxis("Mouse Y");
			currentInteractable.transform.Rotate(myCam.transform.right, -Mathf.Deg2Rad * y * rotateSpeed, Space.World);
			currentInteractable.transform.Rotate(myCam.transform.up, -Mathf.Deg2Rad * x * rotateSpeed, Space.World);

			// Ray cast investigate clue
			// From the camera to the clue
			RaycastHit hit;
			Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(myCam.transform.position, ray.direction, Color.green);

			if (Physics.Raycast(ray, out hit, nearRayDistance))
			{
				ClueTrigger trigger = hit.collider.GetComponent<ClueTrigger>();

				if (trigger != null)
				{
					trigger.GainClue();
				}
			}

		}
	}
}