using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntity : OpenableEntity
{
    public DoorEntity(string name, string idetifier = null ):
        base(name, idetifier)
    {

    }
    public override void Interact(EscapeGame game)
    {
        Debug.Log("You found an exit!");
        game.Finish();
    }

}

public class ExitDoorEntity : DoorEntity
{
    public ExitDoorEntity(string name, string identifier=null) : base(name, identifier)
    {

    }
    protected override void Open(EscapeGame game)
    {
        Debug.Log("It's the right exit!");
        game.Escape();
    }
}

public class MonsterDoorEntity : DoorEntity
{
    public MonsterDoorEntity(string name, string identifier = null) : base(name, identifier)
    {

    }
    protected override void Open(EscapeGame game)
    {
        Debug.Log("<color=red>You release a monster. </color>");
        game.Die();
    }
}