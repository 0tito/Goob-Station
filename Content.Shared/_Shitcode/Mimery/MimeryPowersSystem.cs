using Content.Shared.Abilities.Oni;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.Mind;
using Content.Shared.Speech.Muting;
using Robust.Shared.Network;


namespace Content.Shared._Shitcode.Mimery;

public sealed class MimeryPowersSystem : EntitySystem
{

    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly ActionContainerSystem _actionContainer = default!;
    [Dependency] private readonly INetManager _netManager = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MimeryPowersComponent, ComponentInit>(OnMimeryPowersGranted);
        //SubscribeLocalEvent<MimeryPowersComponent, FIngerGunEvent>(OnFingerGunUsed);
    }

    private void OnMimeryPowersGranted(Entity<MimeryPowersComponent> ent, ref ComponentInit args)
    {
        _actions.AddAction(ent, "ActionMimeryWall");
        //_actions.AddAction(ent, "ActionFingerGun");
        RaiseLocalEvent(new AddFingerGunComponentEvent(ent));
        EnsureComp<MutedComponent>(ent);
    }
}
