using ProtoBuf;
using System.Collections.Generic;

[ProtoContract]
class Rank {
	[ProtoMember(1)]
	public List<Examine> examineList;
}
