using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkSetup : MonoBehaviour {

	public string currentIpAddress;
	public ushort currentPort;

	public TMPro.TMP_InputField ipInputField;
	public TMPro.TMP_InputField portInputField;
	public Button connectButton;

	public NetworkingManagerScript nms;

	void Start () {
		currentIpAddress = ipInputField.text;
		currentPort = ushort.Parse(portInputField.text);
	}

	public void OnPortChange() {
		currentPort = ushort.Parse(portInputField.text);
	}
	public  void OnIpChange() {
		currentIpAddress = ipInputField.text;
	}
	
	public void OnButtonPress() {
		nms.ConnectTo(currentIpAddress, currentPort);
		SceneManager.LoadScene(1);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Return)){
			OnButtonPress();
		}		
	}
}
