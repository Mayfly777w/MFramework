using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 资源管理类
/// </summary>
public class ResMgr : MonoSingleton<ResMgr>
{
    /// <summary>
    /// 资源缓存
    /// </summary>
    private Dictionary<string, object> resCacheDic;

    public override void Init()
    {
        base.Init();
        resCacheDic = new Dictionary<string, object>();
    }

    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Load<T>(string name) where T : Object
    {
        if (resCacheDic.ContainsKey(name) && resCacheDic[name] != null)
        {
            return (T)resCacheDic[name];
        }
        else
        {
            T asset = Addressables.LoadAssetAsync<T>(name).WaitForCompletion();

            if (asset == null)
            {
                Debug.Log($"资源加载失败：{name}");
                return null;
            }

            resCacheDic[name] = asset;
            return asset;
        }
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<T> LoadAsync<T>(string name) where T : Object
    {
        AsyncOperationHandle<T> handle;

        if (resCacheDic.ContainsKey(name))//判断是否已在缓存中
        {
            handle = (AsyncOperationHandle<T>)resCacheDic[name];
            if (!handle.IsDone)//判断资源是否加载完成
            {
                await handle.Task;
            }

            return (T)resCacheDic[name];
        }

        handle = Addressables.LoadAssetAsync<T>(name);//如果不在缓存中，则加载资源
        resCacheDic[name] = handle;
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }

        Debug.Log($"资源加载失败：{name}");
        return null;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Release<T>(string name)
    {
        if (!resCacheDic.ContainsKey(name))//如果本就不在缓存中
        {
            return;
        }

        AsyncOperationHandle<T> handle = (AsyncOperationHandle<T>)resCacheDic[name];
        Addressables.Release(handle);
        resCacheDic.Remove(name);
    }
}
