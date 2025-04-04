using Content.Shared._Shitcode.Mimery;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Mind;
using Content.Shared.Weapons.Ranged.Systems;
using Robust.Shared.Network;

namespace Content.Server._Shitcode.Mimery;

public sealed class FingerGunSystem : EntitySystem
{
    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly ActionContainerSystem _actionContainer = default!;
    [Dependency] private readonly INetManager _netManager = default!;
    [Dependency] private readonly SharedGunSystem _gun = default!;
    [Dependency] private readonly SharedHandsSystem _hands = default!;


    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<FingerGunComponent, ComponentInit>(OnComponentInit);
        SubscribeLocalEvent<FingerGunComponent, FingerGunEvent>(OnFingerGunUsed);
        SubscribeLocalEvent<FingerGunComponent, FingerGunShotEvent>(OnFingerGunShot);
    }


    private void OnComponentInit(Entity<FingerGunComponent> ent, ref ComponentInit args)
    {
        _actions.AddAction(ent, "ActionFingerGun");
    }
    private void OnFingerGunUsed(Entity<FingerGunComponent> ent, ref FingerGunEvent args)
    {
        if (!args.Handled)
        {
            ent.Comp.OnHand = !ent.Comp.OnHand;

            if (!ent.Comp.OnHand)
            {
                if (!ent.Comp.FingerGunExists)
                    ent.Comp.FingerGun = Spawn("FingerGun", Transform(ent).Coordinates);
            }
            //should give you the gun

            else
                ;
            //delete the gun

            if (!_hands.TryForcePickupAnyHand(ent, ent.Comp.FingerGun))
            {
                Del(ent.Comp.FingerGun);
                return;
            }

        }
        args.Handled = true;
    }

    private void OnFingerGunShot(Entity<FingerGunComponent> ent, ref FingerGunShotEvent args)
    {
        //pew pew but should use the info from fingerguncomponent,
    }

}
