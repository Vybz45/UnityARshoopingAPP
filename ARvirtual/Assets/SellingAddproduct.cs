using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingAddproduct : MonoBehaviour
{
    public Text tem_outinfo_description;
    static string image_path_upload;
    public Text image_path_state;
    public Text model_path_state;
    static string model_path_upload;
    
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
            model_path_state.color=Color.red;
            model_path_state.text="REQUIRED";
        }
    }
}
