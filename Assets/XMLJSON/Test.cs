using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using LitJson;
using System;
public class Test : MonoBehaviour 
{
    private string filepath ;
    private string jsonpath;

    void Start() 
    {
        filepath = Application.dataPath + @"/XMLJSON/my.xml";
        jsonpath = Application.dataPath + @"/XMLJSON/json.txt";
    }


	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,200,30),"CREATE XML"))
		{
			createXml();
		}
		if(GUI.Button(new Rect(10,50,200,30),"UPDATE XML"))
		{
			UpdateXml();
		}
		if(GUI.Button(new Rect(10,90,200,30),"ADD XML"))
		{
			AddXml();
		}
		if(GUI.Button(new Rect(10,130,200,30),"DELETE XML"))
		{
			deleteXml();
		}
		if(GUI.Button(new Rect(10,170,200,30),"SHOW XML"))
		{
			showXml();
		}
		if(GUI.Button(new Rect(10,210,200,30),"Resolve JSON"))
		{
			ResolveJson();
		}
		
		if(GUI.Button(new Rect(10,250,200,30),"Merger JSON"))
		{
			MergerJson();
		}

        if (GUI.Button(new Rect(10, 300, 400, 80), "<color=red>testMY JSON</color>"))
        {
            MergerMyJson();
        }

	}



	public void createXml()
	{
		if(!File.Exists (filepath))
		{	
			 XmlDocument xmlDoc = new XmlDocument();
			 XmlElement root = xmlDoc.CreateElement("transforms"); 
			 XmlElement elmNew = xmlDoc.CreateElement("rotation"); 
			 elmNew.SetAttribute("id","0");
 		     elmNew.SetAttribute("name","momo");
			
        	 XmlElement rotation_X = xmlDoc.CreateElement("x"); 
             rotation_X.InnerText = "0"; 
             XmlElement rotation_Y = xmlDoc.CreateElement("y"); 
             rotation_Y.InnerText = "1"; 
             XmlElement rotation_Z = xmlDoc.CreateElement("z"); 
             rotation_Z.InnerText = "2"; 
   			 rotation_Z.SetAttribute("id","1");
			
             elmNew.AppendChild(rotation_X);
             elmNew.AppendChild(rotation_Y);
             elmNew.AppendChild(rotation_Z);
			 root.AppendChild(elmNew);
             xmlDoc.AppendChild(root);
             xmlDoc.Save(filepath); 
			 Debug.Log("createXml OK!");
		}
	}
	public void UpdateXml()
	{
		if(File.Exists (filepath))
		{
			 XmlDocument xmlDoc = new XmlDocument();
			 xmlDoc.Load(filepath);
			 XmlNodeList nodeList=xmlDoc.SelectSingleNode("transforms").ChildNodes;
			 foreach(XmlElement xe in nodeList)
			 {
				if(xe.GetAttribute("id")=="0")
				{
					xe.SetAttribute("id","1000");
					foreach(XmlElement x1 in xe.ChildNodes)
					{
						if(x1.Name=="z")
						{
							 x1.InnerText="update00000";
						}
						
					}
					break;
				}
			 }
			 xmlDoc.Save(filepath);
			 Debug.Log("UpdateXml OK!");
		}
		
	}
	
	public void AddXml()
	{
		if(File.Exists (filepath))
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filepath);
			XmlNode root = xmlDoc.SelectSingleNode("transforms");
			XmlElement elmNew = xmlDoc.CreateElement("rotation"); 
			elmNew.SetAttribute("id","1");
 		    elmNew.SetAttribute("name","yusong");
			
        	 XmlElement rotation_X = xmlDoc.CreateElement("x"); 
             rotation_X.InnerText = "0"; 
			 rotation_X.SetAttribute("id","1");
             XmlElement rotation_Y = xmlDoc.CreateElement("y"); 
             rotation_Y.InnerText = "1"; 
             XmlElement rotation_Z = xmlDoc.CreateElement("z"); 
             rotation_Z.InnerText = "2"; 
   			
             elmNew.AppendChild(rotation_X);
             elmNew.AppendChild(rotation_Y);
             elmNew.AppendChild(rotation_Z);
			 root.AppendChild(elmNew);
             xmlDoc.AppendChild(root);
             xmlDoc.Save(filepath); 
			 Debug.Log("AddXml OK!");
		}
	}
	
	public void deleteXml()
	{
		if(File.Exists (filepath))
		{
			 XmlDocument xmlDoc = new XmlDocument();
			 xmlDoc.Load(filepath);
			 XmlNodeList nodeList=xmlDoc.SelectSingleNode("transforms").ChildNodes;
			 foreach(XmlElement xe in nodeList)
			 {
				if(xe.GetAttribute("id")=="1")
				{
					xe.RemoveAttribute("id");
				}
				
				foreach(XmlElement x1 in xe.ChildNodes)
				{
					if(x1.Name == "z")
					{
						x1.RemoveAll();
						
					}
				}
			 }
			 xmlDoc.Save(filepath);
			 Debug.Log("deleteXml OK!");
		}
		
	}

	public void showXml()
	{
		if(File.Exists (filepath))
		{
			 XmlDocument xmlDoc = new XmlDocument();
			 xmlDoc.Load(filepath);
			 XmlNodeList nodeList=xmlDoc.SelectSingleNode("transforms").ChildNodes;
			
			 foreach(XmlElement xe in nodeList)
			 {
				Debug.Log("Attribute :" + xe.GetAttribute("name"));
				Debug.Log("NAME :" + xe.Name);
				foreach(XmlElement x1 in xe.ChildNodes)
				{
					if(x1.Name == "y")
					{
						Debug.Log("VALUE :" + x1.InnerText);
						
					}
				}
			 }
			 Debug.Log("all = " + xmlDoc.OuterXml);
			
		}
	}
	
	public void ResolveJson()       
	{
		 string str = @"
            {
                ""Name""     : ""yusong"",
                ""Age""      : 26,
                ""Birthday"" : ""1986-11-21"",
 				""Thumbnail"":[
				{
           			""Url"":    ""http://xuanyusong.com"",
           			""Height"": 256,
           			""Width"":  ""200"" 
				},
				{
           			""Url"":    ""http://baidu.com"",
           			""Height"": 1024,
           			""Width"":  ""500"" 
				}

				]
            }";
		
		JsonData jd = JsonMapper.ToObject(str);
        //string a = JsonMapper.ToJson(jsonpath);
        //Debug.Log(a);
		Debug.Log("name = " + (string)jd["Name"]);
		Debug.Log("Age = " + (int)jd["Age"]);
		Debug.Log("Birthday = " + (string)jd["Birthday"]);
		JsonData jdItems = jd["Thumbnail"]; 
		
		for (int i = 0; i < jdItems.Count; i++)
		{
			Debug.Log("URL = " + jdItems[i]["Url"]);
			Debug.Log("Height = " + (int)jdItems[i]["Height"]);
        	Debug.Log("Width = " + jdItems[i]["Width"]);   
		}
	}

    public void MergerMyJson() 
    {
        ArrayList arr = ReadTxt();
        StringBuilder sss=new StringBuilder();
        for (int i = 0; i < arr.Count; i++)
        {
            sss.Append(arr[i]);
            Debug.Log(arr[i]);
        }
        JsonData jd = JsonMapper.ToObject(sss.ToString());
        string a = (string)jd["Name"];

        Debug.Log(((int)jd["Age"]).ToString());
        //FileInfo fi = new FileInfo(jsonpath);
        //byte[] bt = new byte[256];
      // int n = sr.Read(bt as char,0,bt.Length);
    }

    ArrayList ReadTxt() 
    {
        StreamReader sr = File.OpenText(jsonpath);
        string str=string.Empty;
        ArrayList arr = new ArrayList();
        while ((str = sr.ReadLine())!= null)
        {
            arr.Add(str);
        }
        sr.Close();
        sr.Dispose();
        return arr;
    }


    void WriteTxt(StringBuilder sb) 
    {
        StreamWriter sw = null;
        FileInfo fi = new FileInfo(jsonpath);

        if (fi.Exists)
        {
            sw = fi.AppendText();
            //  sw.Dispose();
        }
        else
        {
            sw = fi.CreateText();
            //  sw.Dispose();
        }
        sw.Write(sb.ToString());
        sw.Close();

        sw.Dispose();
    }

	public void MergerJson()
	{		
		StringBuilder sb = new StringBuilder ();
        JsonWriter writer = new JsonWriter (sb);

        writer.WriteObjectStart ();
        
		writer.WritePropertyName ("Name");
        writer.Write ("yusong");
		
		writer.WritePropertyName ("Age");
        writer.Write (26);
	
		writer.WritePropertyName ("Girl");
		
		writer.WriteArrayStart ();
		
		writer.WriteObjectStart();
		writer.WritePropertyName("name");
        writer.Write("ruoruo");
        writer.WritePropertyName("age");
        writer.Write(24);
		writer.WriteObjectEnd ();
		
		writer.WriteObjectStart();
		writer.WritePropertyName("name");
        writer.Write("momo");
        writer.WritePropertyName("age");
        writer.Write(26);
		writer.WriteObjectEnd ();
        
		writer.WriteArrayEnd();
		
		writer.WriteObjectEnd ();
		Debug.Log(sb.ToString ());
      //  FileStream fs = new FileStream(Application.dataPath+"/XMLJSON/json.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
       // writer.TextWriter.Write();

        WriteTxt(sb);/*Ð´ÈëTXTÎÄ¼þ*/
		JsonData jd = JsonMapper.ToObject(sb.ToString ());   
		Debug.Log("name = " + (string)jd["Name"]);
		Debug.Log("Age = " + (int)jd["Age"]);
		JsonData jdItems = jd["Girl"]; 
		for (int i = 0; i < jdItems.Count; i++)
		{
			Debug.Log("Girl name = " + jdItems[i]["name"]);
			Debug.Log("Girl age = " + (int)jdItems[i]["age"]);
		}	
	}
}
