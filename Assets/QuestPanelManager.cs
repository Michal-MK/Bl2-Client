using System.Collections.Generic;
using UnityEngine;
using PacketNS;
using System.Linq;

public class QuestPanelManager : MonoBehaviour {

	private List<GameObject> instantiatedThings = new List<GameObject>();

	public GameObject questAcceptEntryPrefab;

	public Transform toAcceptPanel;
	public Transform toTriggerPanel;


	private void NetworkingManagerScript_OnNewCentralDataReceived() {

		//Epic easy programming
		foreach (GameObject instance in instantiatedThings) {
			Destroy(instance);
		}
		instantiatedThings.Clear();


		List<Quest> acceptedQuests = NetworkingManagerScript.instance.centralData.Where(QuestOne => { return QuestOne.status == QuestStatus.Accepted; }).ToList<Quest>();
		List<Quest> toAcceptQuests = NetworkingManagerScript.instance.centralData.Where(QuestOne => { return QuestOne.status == QuestStatus.NotAccepted; }).ToList<Quest>();

		acceptedQuests.Sort((Quest first, Quest second) => { return first.questLevel - second.questLevel; } );

		toAcceptQuests = (from q in toAcceptQuests
						  orderby q.availableSince, q.questID
						  select q).ToList();


		foreach (Quest quest in toAcceptQuests) {
			InstantiateAndSet(quest);
		}

		foreach(Quest quest in acceptedQuests) {
			InstantiateAndSet(quest);
		}
	}

	private void InstantiateAndSet(Quest quest) {
		QuestGameObject entry = Instantiate(questAcceptEntryPrefab).GetComponent<QuestGameObject>();

		if (quest.status == QuestStatus.Accepted) {
			entry.transform.SetParent(toTriggerPanel);
			entry.triggerButton.SetActive(true);
			entry.acceptButton.SetActive(false);
			entry.dismissButton.SetActive(false);
		}
		else {
			entry.transform.SetParent(toAcceptPanel);
			entry.triggerButton.SetActive(false);
			entry.acceptButton.SetActive(true);
			entry.dismissButton.SetActive(true);
		}

		entry.questInfo.text = string.Format("L: {0}, {1}, SM: {2}", quest.questLevel , quest.acceptedBy, quest.availableSince);
		entry.transform.localScale = Vector3.one;
		entry.quest = quest;
		entry.manager = this;
		entry.label.text = quest.questName;
		instantiatedThings.Add(entry.gameObject);
	}

	public void SendQuestChange(int questID, QuestStatus status) {
		NetworkingManagerScript.instance.SendPacket(new Packet(questID, status));
	}

	private void Update() {
		if (NetworkingManagerScript.instance.centralDataUpdate) {
			NetworkingManagerScript.instance.centralDataUpdate = false;
			NetworkingManagerScript_OnNewCentralDataReceived();
			PopulateColorInfo.instance.OnDataReceived(NetworkingManagerScript.instance.centralData);
		}
	}
}
