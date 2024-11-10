using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PullJSONFromServer : MonoBehaviour
{
    [SerializeField] private ProductManager productManager;
    [SerializeField] private Button cancelPullBtn;
    [SerializeField] private TextMeshProUGUI errorMessageText;
    
    private CancellationTokenSource serverPullCts;
    
    private const string JSONLink = "https://homework.mocart.io/api/products";
    private const string cancelledErrorTxt = "Server pull was cancelled";

    private void OnValidate()
    {
        Debug.Assert(productManager != null,
            "No ProductManager assigned, please assign it to the PullJSONFromServer in the inspector");
        Debug.Assert(cancelPullBtn != null,
            "No Cancel Pull Button assigned, please assign it to the PullJSONFromServer in the inspector");
    }

    public async void PullJSONFromServerAsync()
    {
        errorMessageText.text = "";
        if (serverPullCts != null)
            if (!serverPullCts.IsCancellationRequested)
                serverPullCts.Cancel();

        serverPullCts = new CancellationTokenSource();
        cancelPullBtn.interactable = true;
        
        ProdcutsPulledFromJSON productsFromServerData = await GetDataFromServer(JSONLink, serverPullCts.Token);
        
        if (productsFromServerData != null) productManager.CreateProducts(productsFromServerData);

        cancelPullBtn.interactable = false;
    }

    private async Task<ProdcutsPulledFromJSON> GetDataFromServer(string url, CancellationToken cancellationToken)
    {
        var http = UnityWebRequest.Get(url);
        var get = http.SendWebRequest();
        while (get.isDone == false && !cancellationToken.IsCancellationRequested) await Task.Yield();

        if (cancellationToken.IsCancellationRequested)
        {
            errorMessageText.text = cancelledErrorTxt;
            return null;
        }

        if (http.result == UnityWebRequest.Result.Success)
        {
            var jsonResponse = http.downloadHandler.text;
            var newProductsData = JsonUtility.FromJson<ProdcutsPulledFromJSON>(jsonResponse);
            return newProductsData;
        }

        var errorMessage = http.downloadHandler.text;
        errorMessageText.text = errorMessage;
        return null;
    }

    public void CancelDataRetrieval()
    {
        if (serverPullCts != null) serverPullCts.Cancel();
    }
}