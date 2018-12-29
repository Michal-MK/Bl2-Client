using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketNS;

public class QuestPanelManager : MonoBehaviour {

	private List<GameObject> instantiatedThings = new List<GameObject>();

	public GameObject questAcceptEntryPrefab;
	public GameObject questTriggerEntryPrefab;

	public GameObject toAcceptPanel;
	public GameObject toTriggerPanel;


	private void NetworkingManagerScript_OnNewCentralDataReceived() {
		foreach (GameObject instance in instantiatedThings) {
			Destroy(instance);
		}
		instantiatedThings.Clear();

		List<Quest> sortingThingie = new List<Quest>(NetworkingManagerScript.instance.centralData);

		sortingThingie.Sort((Quest first, Quest second) => {return first.availableSince - second.availableSince; });


		foreach (Quest quest in sortingThingie) {
			switch (quest.status) {
				case QuestStatus.NotAccepted: {
					InstantiateAndSet(false, quest);
					break;
				}
				case QuestStatus.Hidden: {
					break;
				}
				case QuestStatus.Triggered: {
					break;
				}
				case QuestStatus.Accepted: {
					InstantiateAndSet(true, quest);
					break;
				}
			}
		}
	}

	private void InstantiateAndSet(bool isTrigger, Quest quest) {
		QuestEntry entry;
		if (isTrigger) {
			entry = Instantiate(questTriggerEntryPrefab).GetComponent<QuestEntry>();
			entry.transform.SetParent(toTriggerPanel.transform);
		}
		else {
			entry = Instantiate(questAcceptEntryPrefab).GetComponent<QuestEntry>();
			entry.transform.SetParent(toAcceptPanel.transform);
		}
		entry.transform.localScale = Vector3.one;
		entry.quest = quest;
		entry.manager = this;
		entry.label.text = quest.questName;
		instantiatedThings.Add(entry.gameObject);
	}

	public void QuestAccepted(int questId) {
		NetworkingManagerScript.instance.SendPacket(new PacketNS.Packet(questId,PacketNS.QuestStatus.Accepted));
	}

	public void QuestTriggered(int questId) {
		NetworkingManagerScript.instance.SendPacket(new PacketNS.Packet(questId, PacketNS.QuestStatus.Triggered));
	}

	public void QuestDismissed(int questId) {
		NetworkingManagerScript.instance.SendPacket(new PacketNS.Packet(questId, PacketNS.QuestStatus.Hidden));
	}


	private void Update() {
		if (NetworkingManagerScript.instance.centralDataUpdate) {
			NetworkingManagerScript.instance.centralDataUpdate = false;
			NetworkingManagerScript_OnNewCentralDataReceived();
		}
	}
}
