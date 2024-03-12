using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class Vector3Converter : JsonConverter<Vector3>
{
	public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
        var jObj = JObject.Load(reader);	
		var x = (float)jObj["x"];
		var y = (float)jObj["y"];
		var z = (float)jObj["z"];
		return new Vector3(x, y, z);

	}

	public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
	{

		writer.WriteStartObject();  //Áß°ýÈ£ ½ÃÀÛ
		writer.WritePropertyName("x");
		writer.WriteValue(value.x);
		writer.WritePropertyName("y");
		writer.WriteValue(value.y);
		writer.WritePropertyName("z");
		writer.WriteValue(value.z);
		writer.WriteEndObject();	//Áß°ýÈ£ ³¡
	}
}


public class QuaternionConverter : JsonConverter<Quaternion>
{
	public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var jObj = JObject.Load(reader);   
		var x = (float)jObj["x"];
		var y = (float)jObj["y"];
		var z = (float)jObj["z"];
		var w = (float)jObj["z"];
		return new Quaternion(x, y, z, w);
	}

	public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
	{
		writer.WriteStartObject();  //Áß°ýÈ£ ½ÃÀÛ
		writer.WritePropertyName("x");
		writer.WriteValue(value.x);
		writer.WritePropertyName("y");
		writer.WriteValue(value.y);
		writer.WritePropertyName("z");
		writer.WriteValue(value.z);
		writer.WritePropertyName("w");
		writer.WriteValue(value.w);
		writer.WriteEndObject();    //Áß°ýÈ£ ³¡
	}
}

