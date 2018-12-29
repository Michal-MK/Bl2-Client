using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Igor.TCP;
using System;
using PacketNS;

public class NetworkingManagerScript : MonoBehaviour {

	TCPClient thisClient;

	//public static event EventHandler OnNewCentralDataReceived;


	public static NetworkingManagerScript instance;

	public enum QuestStatus {
		NotAccepted,
		Triggered,
		Accepted

	}

	public Quest[] centralData { get; set; }
	public bool centralDataUpdate;

	void Start() {
		DontDestroyOnLoad(this.gameObject);
		instance = this;
		
	}

	public void ConnectTo(string IPAddress, ushort port) {
		thisClient = new TCPClient(IPAddress, port);
		thisClient.SetUpClientInfo(SystemInfo.deviceName);
		thisClient.Connect();

		thisClient.getConnection.dataIDs.DefineCustomDataTypeForID<Packet>(Constants.PACKET_ID, UselessCallback);

		thisClient.getConnection.dataIDs.DefineCustomDataTypeForID<Quest[]>(Constants.PROPERTY_SYNC, OnCentralDataReceived);
	}

	private void UselessCallback(Packet arg1, byte arg2) {
		throw new NotImplementedException();
	}

	//private void GetConnection_OnStringReceived(object sender, PacketReceivedEventArgs<string> e) {
	//	int k = 2;
	//	string s = "YO";
	//}

	private void OnCentralDataReceived(Quest[] arg1, byte arg2) {
		centralData = arg1;
		centralDataUpdate = true;
		//OnNewCentralDataReceived(arg2, EventArgs.Empty);

	}


	//private void GetConnection_OnStringReceived(object sender, PacketReceivedEventArgs<string> e) {
	//	if (e.data[0] == '#') {
	//		//Valid data
	//		int questId = int.Parse(e.data.Split(':')[1]);

	//		QuestStatus status = 0;
	//		switch (e.data.Split(':')[0]) {
	//			case "N": {
	//				status = QuestStatus.NotAccepted;
	//				break;
	//			}
	//			case "T": {
	//				status = QuestStatus.Triggered;
	//				break;
	//			}
	//			case "A": {
	//				status = QuestStatus.Accepted;
	//				break;
	//			}
	//		}


	//	}
	//}

	private void Disconnect() {

	}
		
	private void Connect() {

	}

	public void SendPacket(Packet packet) {

		Connect();
		thisClient.getConnection.SendData(Constants.PACKET_ID, SimpleTCPHelper.GetBytesFromObject(packet));
		Disconnect();

	}
}