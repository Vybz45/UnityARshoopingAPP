using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class DataBase_Manager : MonoBehaviour
{
    static DatabaseReference databaseReference;
    static string uid;
    void Start(){
        // Get the root reference location of the database.
        databaseReference=FirebaseDatabase.DefaultInstance.RootReference;
        
    }
    public static string CreatProduct_table(){//returen product id as identifier for cloude storage refrence
        databaseReference=FirebaseDatabase.DefaultInstance.RootReference;
        string userid=AuthManeger.User.UserId;
        uid=userid;
        
        string product_key= databaseReference.Child("Users").Child(userid).Child("Selling").Push().Key;
        databaseReference.Child("Products").Child(product_key).SetValueAsync(null);
        //databaseReference.Child("Users").Child(userid).Child("Selling").Child(product_key);
        return product_key;

    }
    public static void AddProducts_value(string _in_json,string _prodID){
        databaseReference.Child("Users").Child(uid).Child("Selling").Child(_prodID).SetRawJsonValueAsync(_in_json);
    }


}
