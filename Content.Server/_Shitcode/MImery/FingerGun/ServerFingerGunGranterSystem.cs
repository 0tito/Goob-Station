using Content.Shared._Shitcode.Mimery;

namespace Content.Server._Shitcode.Mimery;

public sealed class ServerFingerGunGranterSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<AddFingerGunComponentEvent>(OnAddFingerGunComponent);
    }

    private void OnAddFingerGunComponent(AddFingerGunComponentEvent args)
    {
        EnsureComp<FingerGunComponent>(args.Target);
    }
}

