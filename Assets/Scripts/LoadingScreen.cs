using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
	public string SceneToLoad;
	
	void Update()
	{
		if (Application.GetStreamProgressForLevel(SceneToLoad) == 1){Application.LoadLevel(SceneToLoad);} 
	}


}