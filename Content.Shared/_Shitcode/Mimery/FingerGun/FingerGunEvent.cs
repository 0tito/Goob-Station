using Content.Shared.Actions;
using Robust.Shared.Serialization;

namespace Content.Shared._Shitcode.Mimery;

[Serializable, NetSerializable]
public sealed partial class FingerGunEvent : InstantActionEvent { }

public sealed partial class FingerGunShotEvent : EventArgs
{

}
