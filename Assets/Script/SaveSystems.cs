using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystems : MonoBehaviour
{
   public static SaveSystems controls;
    string filePath;
	
	
    void Awake()
    {
		controls = this;
		
		filePath = Application.persistentDataPath + "/sava.fun";
        
    }
	public void SaveGame(GameData data)
	{
		FileStream dataStream = new FileStream( filePath, FileMode.Create);
		
		BinaryFormatter converter = new BinaryFormatter();
		converter.Serialize(dataStream, data);
		dataStream.Close();
	}
	public GameData LoadGame()
	{
		if(File.Exists(filePath)){
			FileStream dataStream = new FileStream(filePath, FileMode.Open);
			
			BinaryFormatter converter = new BinaryFormatter();
			GameData data = converter.Deserialize(dataStream) as GameData;
			
			dataStream.Close();
			return data;
		
		}
		else{
			Debug.LogError("Save file not found in" + filePath);
			return null;
		}
		
	}
   
}


