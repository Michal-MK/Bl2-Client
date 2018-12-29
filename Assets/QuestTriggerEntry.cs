using PacketNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerEntry : QuestEntry {

	public void QuestTriggeredButton() {
		manager.QuestTriggered(quest.questID);
	}

}
