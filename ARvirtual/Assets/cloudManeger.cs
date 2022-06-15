using System.Threading;
//uloads and downloads model data to cloud storage 
//passed data to and from modelloder
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;



public class cloudManeger : MonoBehaviour
{
    FirebaseStorage storage ;
    StorageReference storageRef;
    public void SetRefrence(){
        // Get a reference to the storage service, using the default Firebase App
        storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        storageRef = storage.GetReferenceFromUrl("gs://virtual-shopping-83c7b.appspot.com/");
    }
    public void uploadFileToCloud(string filepath){//upload this file from disk
        //typs to upload limage gltf glb from loacl device
        // File located on disk
        string localFile = filepath;

        // Create a reference to the file you want to upload
        StorageReference riversRef = storageRef.Child("product/model.gltf");

        // Upload the file to the path "images/rivers.jpg"
        riversRef.PutFileAsync(localFile)
            .ContinueWith((Task<StorageMetadata> task) => {
                if (task.IsFaulted || task.IsCanceled) {
                    Debug.Log(task.Exception.ToString());
                    // Uh-oh, an error occurred!
                }
                else {
                    // Metadata contains file metadata such as size, content-type, and download URL.
                    StorageMetadata metadata = task.Result;
                    string md5Hash = metadata.Md5Hash;
                    Debug.Log("Finished uploading...");
                    Debug.Log("md5 hash = " + md5Hash);
                }
            });

    }
    public void DownloadFromCloud(string _sourcePath,string _destinationPath ){

        StorageReference reference=storageRef.Child("product/model.gltf");//from clickd product database name 
                // Create local filesystem URL
        string localUrl = _destinationPath+"/temp_modfromCloud.gltf";//file name assigned heres

        // Start downloading a file
        Task task = reference.GetFileAsync(localUrl,
            new StorageProgress<DownloadState>(state => {
                // called periodically during the download
                Debug.Log(string.Format(
                    "Progress: {0} of {1} bytes transferred.",
                    state.BytesTransferred,
                    state.TotalByteCount
                ));
            }), CancellationToken.None);

        task.ContinueWithOnMainThread(resultTask => {
            if (!resultTask.IsFaulted && !resultTask.IsCanceled) {
                Debug.Log("Download finished.");
            }
        });
    }
    public void getPrepdAsset(){
        //get proccessed glb and gltf files from class to upload

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
