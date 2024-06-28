using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
public interface ICanLoadAssetsAsync
{
    public async Task<IList<TAssetType>> LoadAssetsAsync<TAssetType>(string key,Action<TAssetType> action)
    {
        var list = await Addressables.LoadAssetsAsync(key,action);
        Debug.Log(typeof(TAssetType).Name+"资源列表加载完成!");
        return list;
    }
    public async Task<TAssetType> LoadAssetAsync<TAssetType>(string key)
    {
        var asset= await Addressables.LoadAssetAsync<TAssetType>(key);
        Debug.Log(typeof(TAssetType).Name+"资源加载完成!");
        return asset;
    }
    public async Task<TAssetType> LoadAssetAsync<TAssetType>(string key,Action action)
    {
        var asset= await Addressables.LoadAssetAsync<TAssetType>(key);
        Debug.Log(typeof(TAssetType).Name+"资源加载完成!");
        action?.Invoke();
        return asset;
    }
    public async Task<TAssetType> LoadAssetAsync<TAssetType,T1>(string key,Action<T1>action,T1 param)
    {
        var asset= await Addressables.LoadAssetAsync<TAssetType>(key);
        Debug.Log(typeof(TAssetType).Name+"资源加载完成!");
        action?.Invoke(param);
        Debug.Log("完成回调Action");
        return asset;
    }
    public async Task<TAssetType> LoadAssetAsync<TAssetType,T1,T2>(string key,Action<T1,T2>action,T1 param1,T2 param2)
    {
        var asset= await Addressables.LoadAssetAsync<TAssetType>(key);
        Debug.Log(typeof(TAssetType).Name+"资源加载完成!");
        action?.Invoke(param1,param2);
        Debug.Log("完成回调Action");
        return asset;
    }
    public async Task<TAssetType> LoadAssetAsync<TAssetType,T1,T2,T3>(string key,Action<T1,T2,T3>action,T1 param1,T2 param2,T3 param3)
    {
        var asset= await Addressables.LoadAssetAsync<TAssetType>(key);
        Debug.Log(typeof(TAssetType).Name+"资源加载完成!");
        action?.Invoke(param1,param2,param3);
        Debug.Log("完成回调Action");
        return asset;
    }
}

