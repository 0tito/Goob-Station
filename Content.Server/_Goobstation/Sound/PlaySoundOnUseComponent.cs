namespace Content.Server._Goobstation.Sound;

[RegisterComponent]

public sealed partial class PlaySoundOnUseComponent : Component
{

    [DataField]
    public string Sound = string.Empty;

}
