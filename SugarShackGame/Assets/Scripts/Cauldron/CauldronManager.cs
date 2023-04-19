using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Manager(typeof(CauldronManager))]
public class CauldronManager : IFlow
{
    #region Singleton
    private static CauldronManager instance;

    public static CauldronManager Instance {
        get {
            if (instance == null) {
                instance = new CauldronManager();
            }
            return instance;
        }
    }

    private CauldronManager() {
        //Private constructor to prevent outside instantiation
    }
    #endregion

    List<Cauldron> cauldrons;

    public void Initialize() {
        foreach (var cauldron in cauldrons) {
            cauldron.Initialize();
        }
    }

    public void PhysicsRefresh() {
        foreach (var cauldron in cauldrons) {
            cauldron.PhysicsRefresh();
        }
    }

    public void PreInitialize() {
        cauldrons = new List<Cauldron>();

        foreach (var cauldron in cauldrons) {
            cauldron.PreInitialize();
        }
    }

    public void Refresh() {
        foreach (var cauldron in cauldrons) {
            cauldron.Refresh();
        }
    }
}
