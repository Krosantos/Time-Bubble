using UnityEngine;
using System.Collections;

public class ItemTextParse : MonoBehaviour {

	public TextAsset textFile;
	public Transform itemPrefab;

	void Start(){
		StartCoroutine(ParseAndGenerate());
	}

	IEnumerator ParseAndGenerate(){
		Debug.Log (textFile.text);
		string cleanText = textFile.text.Replace("\r", "");
		string[] lines = cleanText.Split("/n"[0]);

		foreach (string line in lines){
			var newItem = Instantiate (itemPrefab, Random.insideUnitSphere*10f, Quaternion .identity) as Transform;

			string[] data = line.Split("," [0]);
			newItem.name = data[0];
			newItem.transform.localScale =new Vector3(1, float.Parse (data[1]),1);
			yield return 0;
		}
	}
}
