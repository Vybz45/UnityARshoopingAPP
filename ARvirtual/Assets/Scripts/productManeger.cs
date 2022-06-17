using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class productManeger : MonoBehaviour
{
    // populate propertis to be uploaded to databease
    public string productTitle;
    public string product_discription;
    public string product_price;
    public string product_image_hash;
    public string product_model_hash;
    public productManeger(){

    }
    public string AddProductToDatabase(){
        //if data valid upload to data base as json
        string json=JsonUtility.ToJson(this);
        return json;
    }
}
