using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// ��Դ������
/// </summary>
public class ResMgr : MonoSingleton<ResMgr>
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    private Dictionary<string, object> resCacheDic;

    public override void Init()
    {
        base.Init();
        resCacheDic = new Dictionary<string, object>();
    }

    /// <summary>
    /// ͬ��������Դ
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
                Debug.Log($"��Դ����ʧ�ܣ�{name}");
                return null;
            }

            resCacheDic[name] = asset;
            return asset;
        }
    }

    /// <summary>
    /// �첽������Դ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<T> LoadAsync<T>(string name) where T : Object
    {
        AsyncOperationHandle<T> handle;

        if (resCacheDic.ContainsKey(name))//�ж��Ƿ����ڻ�����
        {
            handle = (AsyncOperationHandle<T>)resCacheDic[name];
            if (!handle.IsDone)//�ж���Դ�Ƿ�������
            {
                await handle.Task;
            }

            return (T)resCacheDic[name];
        }

        handle = Addressables.LoadAssetAsync<T>(name);//������ڻ����У��������Դ
        resCacheDic[name] = handle;
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }

        Debug.Log($"��Դ����ʧ�ܣ�{name}");
        return null;
    }

    /// <summary>
    /// �ͷ���Դ
    /// </summary>
    public void Release<T>(string name)
    {
        if (!resCacheDic.ContainsKey(name))//������Ͳ��ڻ�����
        {
            return;
        }

        AsyncOperationHandle<T> handle = (AsyncOperationHandle<T>)resCacheDic[name];
        Addressables.Release(handle);
        resCacheDic.Remove(name);
    }
}
