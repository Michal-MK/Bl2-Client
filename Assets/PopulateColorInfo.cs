using PacketNS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateColorInfo : MonoBehaviour {

	public GameObject prefab;

	public static PopulateColorInfo instance;

	private void Awake() {
		instance = this;
	}

	public void OnDatReceived(Quest[] data) {
		List<Quest> processed = new List<Quest>();
		foreach (Quest q in data) {
			if (!ProcessedYet(processed, q)) { 
				processed.Add(q);
				GameObject g = Instantiate(prefab, transform.GetChild(1));
				g.GetComponent<ColorInfo>().SetUp(q.givenBy);
			}
		}
	}

	private bool ProcessedYet(List<Quest> processed, Quest quest) {
		foreach (Quest q in processed) {
			if(q.givenBy == quest.givenBy) {
				return true;
			}
		}
		return false;
	}
}
