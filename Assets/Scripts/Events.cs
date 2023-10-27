using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class Events
{
    public static event Action<DrillData> OnDrillSelected;
    public static void DrillSelected(DrillData data) => OnDrillSelected?.Invoke(data);

    public static event Action<TowerComponentData> OnTowerComponentSelected;
    public static void TowerComponentSelected(TowerComponentData data) => OnTowerComponentSelected?.Invoke(data);

    public static event Action<FoundationData> OnFoundationSelected;
    public static void FoundationSelected(FoundationData data) => OnFoundationSelected?.Invoke(data);

    public static event Action<StructureData> OnStructureSelected;
    public static void StructureSelected(StructureData data) => OnStructureSelected?.Invoke(data);

    public static event Action<GunBaseData> OnGunBaseSelected;
    public static void GunBaseSelected(GunBaseData data) => OnGunBaseSelected?.Invoke(data);

    public static event Action<GunData> OnGunSelected;
    public static void GunSelected(GunData data) => OnGunSelected?.Invoke(data);

    public static event Action<SupportBlockData> OnSupportBlockSelected;
    public static void SupportBlockSelected(SupportBlockData data) => OnSupportBlockSelected?.Invoke(data);

    public static event Action<int> OnSetStone;

    public static void SetStone(int value) => OnSetStone?.Invoke(value);

    public static event Func<int> OnGetStone;

    public static int GetStone() => OnGetStone?.Invoke() ?? 0;

    public static event Action<int> OnSetIron;

    public static void SetIron(int value) => OnSetIron?.Invoke(value);

    public static event Func<int> OnGetIron;

    public static int GetIron() => OnGetIron?.Invoke() ?? 0;

    public static event Action<int> OnSetUranium;

    public static void SetUranium(int value) => OnSetUranium?.Invoke(value);

    public static event Func<int> OnGetUranium;

    public static int GetUranium() => OnGetUranium?.Invoke() ?? 0;

    public static event Action<int> OnSetLives;

    public static void SetLives(int value) => OnSetLives?.Invoke(value);

    public static event Func<int> OnGetLives;

    public static int GetLives() => OnGetLives?.Invoke() ?? 0;
}
