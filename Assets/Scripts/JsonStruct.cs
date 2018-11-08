using System;

[Serializable]
public struct DataStruct {
    public int id,parent;
    public string author, title, content, img_name ,created_at;
        
}

[Serializable]
public struct ListTemplate {
    public DataStruct[] datas;
}