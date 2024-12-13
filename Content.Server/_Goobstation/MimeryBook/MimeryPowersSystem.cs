using Content.Server._Shitmed.DelayedDeath;
using Content.Server.Hands.Systems;
using Content.Server.Heretic.EntitySystems;
using Content.Server.Medical;
using Content.Server.Popups;
using Content.Server.Station.Systems;
using Content.Server.Store.Systems;
using Content.Shared.Abilities.Mime;
using Content.Shared.Actions;
using Content.Shared.Actions.Events;
using Content.Shared.Alert;
using Content.Shared.Body.Systems;
using Content.Shared.Coordinates.Helpers;
using Content.Shared.Hands.Components;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Maps;
using Content.Shared.MimeryBook;
using Content.Shared.Physics;
using Robust.Shared.Containers;
using Robust.Shared.Map;
using Robust.Shared.Timing;
using Content.Shared.Speech.Muting;
using Content.Shared.StatusEffect;
using Content.Shared.Stunnable;
using Content.Shared.Throwing;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Robust.Server.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Server._Goobstation.MimeryBook
{
    public sealed class MimeryPowersSystem : EntitySystem
    {
        [Dependency] private readonly StoreSystem _store = default!;
        [Dependency] private readonly PopupSystem _popup = default!;
        [Dependency] private readonly HandsSystem _hands = default!;
        [Dependency] private readonly EntityLookupSystem _lookup = default!;
        [Dependency] private readonly SharedTransformSystem _transform = default!;
        [Dependency] private readonly SharedBodySystem _body = default!;
        [Dependency] private readonly PhysicsSystem _phys = default!;
        [Dependency] private readonly SharedStunSystem _stun = default!;
        [Dependency] private readonly ThrowingSystem _throw = default!;
        [Dependency] private readonly SharedUserInterfaceSystem _ui = default!;
        [Dependency] private readonly StationSystem _station = default!;
        [Dependency] private readonly IPrototypeManager _prot = default!;
        [Dependency] private readonly StatusEffectsSystem _statusEffect = default!;
        [Dependency] private readonly PopupSystem _popupSystem = default!;
        [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;
        [Dependency] private readonly AlertsSystem _alertsSystem = default!;
        [Dependency] private readonly TurfSystem _turf = default!;
        [Dependency] private readonly IMapManager _mapMan = default!;
        [Dependency] private readonly SharedContainerSystem _container = default!;
        [Dependency] private readonly IGameTiming _timing = default!;
        [Dependency] private readonly SharedHandsSystem _sharedHands = default!;


        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<MimeryPowersComponent, ComponentInit>(OnComponentInit);
            SubscribeLocalEvent<MimeryPowersComponent, EventFingerGun>(OnFinger);
            //SubscribeLocalEvent<MimeryPowersComponent, InvisibleWallActionEvent>(OnInvisibleWall);

        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);


            var query = EntityQueryEnumerator<MimeryPowersComponent>();

        }

        private void OnComponentInit(EntityUid uid, MimeryPowersComponent component, ComponentInit args)
        {
            EnsureComp<MutedComponent>(uid);
            //_actionsSystem.AddAction(uid, ref component.MimeryWallActionEntity, component.MimeryWallAction, uid);
        }

        private void OnFinger(Entity<MimeryPowersComponent> ent, ref EventFingerGun args)
        {


           /*
            if (!TryUseAbility(ent, args))
                return;
*/
  //         var st = SaveFinger(Spawn("FingerGun", Transform(ent).Coordinates));
            if (!ent.Comp.FingerGunActive)
            {
                if (ent.Comp.FingerGunExists == false)
                  ent.Comp.FingerGun = Spawn("FingerGun", Transform(ent).Coordinates);
                ent.Comp.FingerGunExists = true;/*
                else
                {
                    //GetFingerGun(ent.Comp.FingerGun);
                }
                */
            }
            else
            {
                ent.Comp.FingerGunActive = false;
                HideGun(new Entity<HandsComponent>() , ent.Comp.FingerGun);
                    //TryDrop(uid, handsComp.ActiveHand, targetDropLocation, checkActionBlocker, doDropInteraction, handsComp);
                //Del(ent.Comp.FingerGun);
                return;
            }


            if (!_hands.TryForcePickupAnyHand(ent, ent.Comp.FingerGun))
            {
                _popup.PopupEntity(Loc.GetString("heretic-ability-fail"), ent, ent);

                Del(ent.Comp.FingerGun);
                return;
            }


            ent.Comp.FingerGunActive = true;
            args.Handled = true;


        }

        private void HideGun(Entity<HandsComponent> hand, EntityUid gun)
        {
            if (hand.Comp.ActiveHand == null)
                return;
            _sharedHands.TryDrop(gun, hand.Comp.ActiveHand, null, false, true, hand.Comp);

        }


    }
}
