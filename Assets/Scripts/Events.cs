using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class Events
{
    public static event Action<DrillData> OnDrillSelected;
    public static void DrillSelected(DrillData data) => OnDrillSelected?.Invoke(data);

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

    public static event Action<int> OnSetResource1;

    public static void SetResource1(int value) => OnSetResource1?.Invoke(value);

    public static event Func<int> OnGetResource1;

    public static int GetResource1() => OnGetResource1?.Invoke() ?? 0;

    public static event Action<int> OnSetResource2;

    public static void SetResource2(int value) => OnSetResource2?.Invoke(value);

    public static event Func<int> OnGetResource2;

    public static int GetResource2() => OnGetResource2?.Invoke() ?? 0;

    public static event Action<int> OnSetResource3;

    public static void SetResource3(int value) => OnSetResource3?.Invoke(value);

    public static event Func<int> OnGetResource3;

    public static int GetResource3() => OnGetResource3?.Invoke() ?? 0;

    public static event Action<int> OnSetLives;

    public static void SetLives(int value) => OnSetLives?.Invoke(value);

    public static event Func<int> OnGetLives;

    public static int GetLives() => OnGetLives?.Invoke() ?? 0;
}
