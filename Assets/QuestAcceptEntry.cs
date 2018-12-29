using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacketNS;

public class QuestAcceptEntry : QuestEntry {

	public void QuestAcceptedButton () {
		manager.QuestAccepted(quest.questID);
	}

	
	public void QuestDismissedButton() {
		manager.QuestDismissed(quest.questID);
	}
}
