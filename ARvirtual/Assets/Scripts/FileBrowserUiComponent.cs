using System.Globalization;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileBrowserUiComponent : MonoBehaviour
{
    //each instance of fileBrowser ui component contains this data
    //data is filled by FBmaneger
    //responds to fbmaneger to navigate dir
    //respons to fbmaneger with filePath
    
    public Color fileIcon;
    public Color dirIcon;
    string displayName;
    public bool isFile,isDir;
    string filePath;
    public void SeFiletData(string _displayName,string _filePath_){
        isFile=true;
        displayName=_displayName;
        filePath=_filePath_;
        updateComponent();
        
    }
    public void SetDirData(string _displayName,string _path){
        isDir=true;
        displayName=_displayName;
        filePath=_path;
        updateComponent();
        
    }
    void updateComponent(){
        Text childDispayComp;
       if(gameObject.GetComponentInChildren<Text>().gameObject.name=="displayName"){
           childDispayComp=gameObject.GetComponentInChildren<Text>();
       }else{
           print("..component locating error");
           return;
       }
       //continue
       childDispayComp.text=displayName;
       if(isFile){
           childDispayComp.color=fileIcon;
       }else if(isDir){
           childDispayComp.color=dirIcon;
       }
       
    }
    public void onAction(){
       updateFileBrouder();
    }
    internal void updateFileBrouder(){
         if(isDir){
            fileBrowserManeger.SwitchCurrentDir(filePath);
            //update browser
           
        }else if(isFile){
            //fileBrowserManeger.set_requestedFilePath(filePath);
            
            //chack if fileBrouswerManager.targetIs_image is true
            //---->> chack if selected file macths expecterd target extention image
            //if so sellingAddProduct.setImagePath(filePath)
            FileTypeValidate_Forward();
        }
    }
    internal void FileTypeValidate_Forward(){

        if(fileBrowserManeger.targetIs_image){
            
            string thisExtenion=fileBrowserManeger.targetExtention_image;
            bool isValid=displayName.EndsWith(thisExtenion);
            
            if(isValid){
                SellingAddproduct.SetImagePath(filePath);
                 print("image file path.,......"+filePath);
                print("extension is match....."+thisExtenion);
            }else{
                print("extension no match...Expected.."+thisExtenion);
                SellingAddproduct.SetImagePath(null);
            }
        }else if(fileBrowserManeger.targetIs_model){
            string thisExtenion=fileBrowserManeger.targetExtention_model;
            bool isValid=displayName.EndsWith(thisExtenion);
            
            if(isValid){
                SellingAddproduct.SetModelPath(filePath);
                print("model file path.,......"+filePath);
                print("extension is match....."+thisExtenion);
            }else{
                print("extension no match...Expected.."+thisExtenion);
                 SellingAddproduct.SetModelPath(null);
            }
        }
    }
    public void setIcon(){
        GameObject childDispayComp;
        if(isDir&&!isFile){
            if(gameObject.GetComponentInChildren<GameObject>().gameObject.name=="dirIcon"){
                childDispayComp=gameObject.GetComponentInChildren<GameObject>().gameObject;
                childDispayComp.SetActive(true);
            }else if(gameObject.GetComponentInChildren<GameObject>().gameObject.name=="fileIcon"){
                childDispayComp=gameObject.GetComponentInChildren<GameObject>().gameObject;
                childDispayComp.SetActive(false);
            }
            else{
                print("..component locating error");
                return;
            }
            
        }else if(isFile&&!isDir){
            if(gameObject.GetComponentInChildren<Image>().gameObject.name=="dirIcon"){
                childDispayComp=gameObject.GetComponentInChildren<Image>().gameObject;
                childDispayComp.SetActive(false);
            }else if(gameObject.GetComponentInChildren<Image>().gameObject.name=="fileIcon"){
                childDispayComp=gameObject.GetComponentInChildren<Image>().gameObject;
                childDispayComp.SetActive(true);
            }
            else{
                
                print("..component locating error");
                return;
            }
        }else{
            print("error..");
        }
      
    }
    // Start is called before the first frame update
    void Start()
    {
        //onstart show path
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void fn(){
        
    }
}
