using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker{
    int hunger;

    public Worker(int hunger = 100)
    {
        this.hunger = hunger;
    }

    public void setWorkerHunger()
    {
        hunger -= 1;
    }
}
