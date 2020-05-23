using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
    private bool defend;
    private bool attack;

    public bool Defend { get => defend; set => defend = value; }
    public bool Attack { get => attack; set => attack = value; }
}
