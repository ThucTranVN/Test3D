public enum ListenType
{
    ANY = 0,
    LEFT_MOUSE_CLICK,
    RIGHT_MOUSE_CLICK,
    UPDATE_PLAYER_INFO,
    RELOAD_ANIMATION_EVENT,
    UPDATE_AMMO
}

public enum UIType
{
    UNKNOWN,
    SCREEN,
    POPUP,
    NOTIFY,
    OVERLAP
}

public enum WeaponSlot
{
    Primary = 0,
    Secondary = 1
}

public enum AIStateID
{
    ChasePlayer,
    Death,
    Idle,
    FindWeapon,
    AttackPlayer
}

public enum SocketID
{
    Spine,
    RightHand
}

public enum EquipWeaponBy
{
    AI,
    Player
}
