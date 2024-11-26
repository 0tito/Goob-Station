using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared.MimeryBook;

[Serializable, NetSerializable]
public sealed partial class MimeryBookDoAfterEvent : SimpleDoAfterEvent
{
}
