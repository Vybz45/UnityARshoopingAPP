using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Siccity.GLTFUtility;



public class modelLod : MonoBehaviour
{
    public static string filepath;

    
    GameObject model;
    // Start is called before the first frame update
    void importModelFromFile()
    {
        
        GameObject result = Importer.LoadFromFile(filepath);
        model=result;
        
    }
    public void displayModelRunT(){
        importModelFromFile();
        GameObject modeTransform=new GameObject();
        Instantiate(model,modeTransform.transform);
    }

    // Update is called once per frame
    public void chacking()
    {
       string tmp=Application.dataPath+"/files_test_resorces/cannon-gltf-embedded.gltf";
       
        filepath=tmp;
        cloudManeger cman=this.gameObject.GetComponent<cloudManeger>();
        cman.SetRefrence();
        cman.uploadFileToCloud(filepath);//upload to cloud from filedir

        string downloadedfilesDir=Application.dataPath+"/files_test_resorces";
        cman.DownloadFromCloud("empty",downloadedfilesDir);
        
       // displayModelRunT();
    }
    public void getFilePath(){
        //file explorer
         string tmp=Application.dataPath;
        filepath=tmp;
        DirectoryInfo df=new DirectoryInfo(filepath);
        FileInfo[] info=df.GetFiles();
        foreach(FileInfo file in df.GetFiles()){
            print(info.Length);
            print(file.Name);
        }
    }
}
