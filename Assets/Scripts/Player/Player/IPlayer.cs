using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer 
{
    void BombStackUpgrade();
    void BombRangeUpgrade();
    void DoorInteraction(bool isOnDoor);
}
