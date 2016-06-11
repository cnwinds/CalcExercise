using ProtoBuf;
using System.Collections.Generic;

[ProtoContract]
class Wrong
{
    [ProtoMember(1)]
    public List<Examine> examineList;
}
