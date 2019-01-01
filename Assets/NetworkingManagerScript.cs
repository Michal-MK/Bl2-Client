using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Igor.TCP;
using System;
using PacketNS;

public class NetworkingManagerScript : MonoBehaviour {

	TCPClient thisClient;
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

	private void OnCentralDataReceived(Quest[] arg1, byte arg2) {
		centralData = arg1;
		centralDataUpdate = true;
		//OnNewCentralDataReceived(arg2, EventArgs.Empty);

	}


	public void SendPacket(Packet packet) {

		thisClient.getConnection.SendData(Constants.PACKET_ID, SimpleTCPHelper.GetBytesFromObject(packet));

	}

	private void OnApplicationPause(bool pause) {
		if(pause == true) {
			thisClient.Disconnect();
		}
		else {
			if(thisClient != null) {
				thisClient.Connect();
			}
		}
	}

	private void OnApplicationQuit() {
		thisClient.Disconnect();
	}

}