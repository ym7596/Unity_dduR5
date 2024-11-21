using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RestProtocol
{
    private void SetBodyData(UnityWebRequest request, string jsonString)
    {
        if (request == null || string.IsNullOrEmpty(jsonString))
            return;

        var data = Encoding.UTF8.GetBytes(jsonString);

        if (data.Length > 0)
        {
            request.uploadHandler = new UploadHandlerRaw(data);
            request.SetRequestHeader("Content-Type", "application/json");
        }
    }

    private void SetHeaders(UnityWebRequest request, Dictionary<string, string> headers)
    {
        if (request == null || headers == null || headers.Count == 0)
            return;

        foreach(var header in headers)
        {
            var findHeader = request.GetRequestHeader(header.Key);

            if (string.IsNullOrEmpty(findHeader))
                request.SetRequestHeader(header.Key, header.Value);
        }
    }
    
    public async Awaitable<string> Get(string url, Dictionary<string, string> header)
    {
        using var req = UnityWebRequest.Get(url);
        SetHeaders(req,header);
     
        req.downloadHandler = new DownloadHandlerBuffer();
       
        await req.SendWebRequest();
        
        if (req.result == UnityWebRequest.Result.Success)
        {
            return req.downloadHandler.text;
        }
        else
        {
            throw new System.Exception(req.error);
        }
    }

    public IEnumerator GetCo(string url, Dictionary<string, string> header, Action<string> onComplete)
    {
        using var req = UnityWebRequest.Get(url);
        SetHeaders(req,header);

        req.downloadHandler = new DownloadHandlerBuffer();

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            onComplete?.Invoke(req.downloadHandler.text);
        }
        else
        {
            Debug.LogError(req.error.ToString());
        }
    }
}
