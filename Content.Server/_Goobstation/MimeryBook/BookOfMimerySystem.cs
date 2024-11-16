using Content.Shared.Interaction.Events;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.Interaction.Events;
using Content.Shared.Magic.Components;
using Content.Shared.Mind;
using Robust.Shared.Network;


namespace Content.Server._Goobstation.MimeryBook;

public sealed class BookOfMimerySystem : EntitySystem
{

    public override void Initialize()
    {
        SubscribeLocalEvent<BookOfMimeryComponent, UseInHandEvent>(OnUseInHand);
    }

    private void OnUseInHand(Entity<BookOfMimeryComponent> ent, ref UseInHandEvent args)
    {

    }

}


