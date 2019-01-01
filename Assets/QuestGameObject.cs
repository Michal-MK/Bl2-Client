using UnityEngine;
using PacketNS;

public class QuestGameObject : MonoBehaviour {

	public Quest quest;

	public QuestPanelManager manager;

	public TMPro.TextMeshProUGUI label;

	public TMPro.TextMeshProUGUI questInfo;

	public GameObject acceptButton;
	public GameObject dismissButton;
	public GameObject triggerButton;

	public void QuestAcceptedButton() {
		manager.SendQuestChange(quest.questID, QuestStatus.Accepted);
	}

	public void QuestDismissedButton() {
		manager.SendQuestChange(quest.questID, QuestStatus.Hidden);
	}

	public void QuestTriggeredButton() {
		manager.SendQuestChange(quest.questID, QuestStatus.Triggered);
	}
}
