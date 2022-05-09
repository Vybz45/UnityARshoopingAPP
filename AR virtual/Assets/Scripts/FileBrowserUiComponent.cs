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
        if(isDir){
            fileBrowserManeger.SwitchCurrentDir(filePath+displayName);
            //update browser
        }else if(isFile){
            fileBrowserManeger.set_requestedFilePath(filePath+displayName);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
