using UnityEngine;
using PacketNS;
using UnityEngine.UI;

public class QuestGameObject : MonoBehaviour {

	public Quest quest;

	public QuestPanelManager manager;

	public TMPro.TextMeshProUGUI label;

	public TMPro.TextMeshProUGUI questInfo;

	public GameObject acceptButton;
	public GameObject dismissButton;
	public GameObject triggerButton;

	private void Start() {
		GetComponent<Image>().color = Colors.GetColor(quest.givenBy);
	}

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
