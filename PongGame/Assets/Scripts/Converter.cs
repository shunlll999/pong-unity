using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Converter {

	public static string  JsonToString( string target ){

		string[] newString = Regex.Split(target,"\"");

		return newString[1];

	}

	public static Vector3 JsonToVecter3(string target ){

		Vector3 newVector;
		string[] newString = Regex.Split(target,",");
		newVector = new Vector3( float.Parse(newString[0]), float.Parse(newString[1]), float.Parse(newString[2]));

		return newVector;

	}

}
