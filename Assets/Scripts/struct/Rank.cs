using UnityEngine;
using System.Collections;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using System;

[ProtoContract]
class Rank {
	[ProtoMember(1)]
	// public List<Examine> examineList;
	public int aaa {get;set;}

}

class RankManager {
	public Rank rank;

	public void Load () {
		string file = Application.persistentDataPath + "/ranklist";
		if (File.Exists(file)) {
			var f = File.OpenRead (file);
			rank = Serializer.Deserialize<Rank>(f);
			f.Close ();
		}
	}

	public void Save () {
		var f = File.Create (Application.persistentDataPath + "/ranklist");
		Serializer.Serialize (f, rank);
		f.Close ();
	}

	public void AddExamine (Examine examine) {
		// rank.Add (examine);
	}
}
