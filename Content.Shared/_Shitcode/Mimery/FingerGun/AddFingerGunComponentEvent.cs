using Robust.Shared.Serialization;

namespace Content.Shared._Shitcode.Mimery;

[Serializable,NetSerializable]
public sealed class AddFingerGunComponentEvent : EntityEventArgs
{
    public EntityUid Target { get; }

    public AddFingerGunComponentEvent(EntityUid target)
    {
        Target = target;
    }
}
