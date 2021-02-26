using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
	[SerializeField]
	battleManager battleManager;

	[SerializeField]
	Transform mainCameraTransform;
	[SerializeField]
	float[] mainCameraBoundaries = new float[4];

	[SerializeField][Range(2f,10f)]
	float cameraSpeed;

	public void initilize()
	{
		battleManager = GetComponent<battleManager>();
		float verticalSpace = battleManager.getMap().getSize()[0] - 10;
		if(verticalSpace > 0)
		{
			mainCameraBoundaries[0] = verticalSpace * 2;
			mainCameraBoundaries[1] = verticalSpace * -2;
			mainCameraBoundaries[2] = (verticalSpace * 1) + 2;
			mainCameraBoundaries[3] = (verticalSpace * -1) + 2;
		}
		else
		{
			mainCameraBoundaries[0] = 0;
			mainCameraBoundaries[1] = -0;
			mainCameraBoundaries[2] = 2;
			mainCameraBoundaries[3] = -2;
		}
	}

	void Update()
	{
		if(mainCameraTransform.position.x > mainCameraBoundaries[0])
		{
			mainCameraTransform.position = new Vector3(mainCameraBoundaries[0], mainCameraTransform.position.y, mainCameraTransform.position.z);
			return;
		}
		if (mainCameraTransform.position.x < mainCameraBoundaries[1])
		{
			mainCameraTransform.position = new Vector3(mainCameraBoundaries[1], mainCameraTransform.position.y, mainCameraTransform.position.z);
			return;
		}
		if (mainCameraTransform.position.y > mainCameraBoundaries[2])
		{
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraBoundaries[2], mainCameraTransform.position.z);
			return;
		}
		if (mainCameraTransform.position.y < mainCameraBoundaries[3])
		{
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraBoundaries[3], mainCameraTransform.position.z);
			return;
		}
		if (Input.GetKey("w"))
		{
				mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y + cameraSpeed * Time.deltaTime, mainCameraTransform.position.z);
		}
		if (Input.GetKey("s"))
		{
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y - cameraSpeed * Time.deltaTime, mainCameraTransform.position.z);
		}
		if (Input.GetKey("a"))
		{
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x - cameraSpeed * Time.deltaTime, mainCameraTransform.position.y, mainCameraTransform.position.z);
		}
		if (Input.GetKey("d"))
		{
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x + cameraSpeed * Time.deltaTime, mainCameraTransform.position.y, mainCameraTransform.position.z);
		}
	}
}
