//verify and pass product data... for database and cloud
//verify---> uploadToCloud--->getHaseValue--->packData--->sendToDatabase
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingAddproduct : MonoBehaviour
{
    public InputField ui_product_Title;
    //product catagoris class needed
    public InputField ui_product_discription;
    public InputField ui_price_discription;
    public Text tem_outinfo_description;
    static string image_path_upload;
    public Text image_path_state;
    public string cloud_image_hashValue;
    public string cloud_model_hashValue;
    public Text model_path_state;
    static string model_path_upload;
    public bool productData_valid;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tem_outinfo_description.text=Application.persistentDataPath;
        update_UI_stat();
    }
    public static void SetImagePath(string _imageFile_Path){
        image_path_upload=_imageFile_Path;
        
    }
    public static void SetModelPath(string _modelFile_Path){
        model_path_upload=_modelFile_Path;
    }
    void update_UI_stat(){
        if(image_path_upload!=null){
            image_path_state.color=Color.green;
            image_path_state.text="VALID";
        }else{
            image_path_state.color=Color.red;
            image_path_state.text="REQUIRED";
        }
        if(model_path_upload!=null){
            model_path_state.color=Color.green;
            model_path_state.text="VALID";
        }else{
            //model should be optional
            model_path_state.color=Color.red;
            model_path_state.text="REQUIRED";
        }
    }
    bool validateProductData(){
        bool data_IsValid=true;
        //chack data integristy...if null display massege box
        //if succsefful display upload progress
        //if complet change panel to selling... new added product are listed by(selling class)
        return data_IsValid;
    }
    void Display_progress(bool _isProductDat_valid,string _prodID){
        //call upload processes... /returns prod id for cloude storage
        string productId= _prodID;
        //display upload progress
        cloudManeger.SetRefrence();
        cloudManeger.uploadFileToCloud(model_path_upload,productId,"model.gltf",out cloud_model_hashValue);
        cloudManeger.uploadFileToCloud(image_path_upload,productId,"image.jpg",out cloud_image_hashValue);
        //get product hash value from cloudManeger

        
    }
    void Database_addProduct(string _prodID){
        productManeger product=new productManeger();
        product.productTitle=ui_product_Title.text;
        product.product_discription=ui_product_discription.text;
        product.product_price=ui_price_discription.text;
        product.product_image_hash=cloud_image_hashValue;
        product.product_model_hash=cloud_model_hashValue;
        DataBase_Manager.AddProducts_value(product.AddProductToDatabase(),_prodID);
        
        //set ui panale to selling
    }
    public void AddProductToDataBase(){// called by ui button
        string productID=DataBase_Manager.CreatProduct_table();
        Display_progress(validateProductData(),productID);
        Database_addProduct(productID);
        UImaneger.currentUIpanel=UImaneger.uiPanels.selling;
    }
}
