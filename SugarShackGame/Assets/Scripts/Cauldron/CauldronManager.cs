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
    GameObject[] cauldronObjects;

    public void Initialize() {
    }

    public void PhysicsRefresh() {
        foreach (var cauldron in cauldrons) {
            cauldron.PhysicsRefresh();
        }
    }

    public void PreInitialize() {
        cauldrons = new List<Cauldron>();

        cauldronObjects = GameObject.FindGameObjectsWithTag("Cauldron");
    }

    public void Refresh() {
        foreach (var cauldron in cauldrons) {
            cauldron.Refresh();
        }
    }

    public void CreateCauldron(Player _player) {
        Cauldron cauldron = cauldronObjects[cauldrons.Count].AddComponent<Cauldron>();
        cauldron.player = _player;
        Debug.Log("This cauldron " + cauldron + " was linked with player : " + _player.index);

        cauldron.PreInitialize();
        cauldron.Initialize();

        cauldrons.Add(cauldron);
    }
}
