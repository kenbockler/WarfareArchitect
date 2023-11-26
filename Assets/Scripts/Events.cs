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

    public static event Action<float> OnSetStone;
    public static void SetStone(float value) => OnSetStone?.Invoke(value);

    public static event Func<float> OnGetStone;
    public static float GetStone() => OnGetStone?.Invoke() ?? 0;

    public static event Action<float> OnSetIron;
    public static void SetIron(float value) => OnSetIron?.Invoke(value);

    public static event Func<float> OnGetIron;
    public static float GetIron() => OnGetIron?.Invoke() ?? 0;

    public static event Action<float> OnSetUranium;
    public static void SetUranium(float value) => OnSetUranium?.Invoke(value);

    public static event Func<float> OnGetUranium;
    public static float GetUranium() => OnGetUranium?.Invoke() ?? 0;

    public static event Action<int> OnSetLives;
    public static void SetLives(int value) => OnSetLives?.Invoke(value);

    public static event Func<int> OnGetLives;
    public static int GetLives() => OnGetLives?.Invoke() ?? 0;

    public static event Action<WaveData> OnStartWave;
    public static void StartWave(WaveData data) => OnStartWave?.Invoke(data);

    public static event Action<WaveData> OnEndWave;
    public static void EndWave(WaveData data) => OnEndWave?.Invoke(data);

    public static event Action<bool> OnEndGame;
    public static void EndGame(bool win) => OnEndGame?.Invoke(win);

    public static event Action<bool> OnPlaceSupportBlock;
    public static void PlaceSupportBlock(bool b) => OnPlaceSupportBlock?.Invoke(b);
}
