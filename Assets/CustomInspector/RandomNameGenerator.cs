using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomNameGenerator 
{
    static List<string> firstNames, lastNames;

    static RandomNameGenerator()
    {
        firstNames = new List<string>();
        #region FirstNames
        firstNames.Add("Steven");
        firstNames.Add("Lisa");
        firstNames.Add("Angelica");
        firstNames.Add("Misty");
        firstNames.Add("Tara");
        firstNames.Add("David");
        firstNames.Add("Grace");
        firstNames.Add("Ashley");
        firstNames.Add("David");
        firstNames.Add("Stanley");
        #endregion
        #region LastNames
        lastNames = new List<string>();
        lastNames.Add("Oliver");
        lastNames.Add("Hernandez");
        lastNames.Add("Smith");
        lastNames.Add("Gordon");
        lastNames.Add("Robinson");
        lastNames.Add("Wilson");
        lastNames.Add("Durham");
        lastNames.Add("Rhodes");
        lastNames.Add("Bonilla");
        lastNames.Add("Thomas");
        #endregion

    }

    static string GetRandomFirstName()
    {
        int rand = Random.Range(0, firstNames.Count);
        return firstNames[rand];
    }

    static string GetRandomLastName()
    {
        int rand = Random.Range(0, lastNames.Count);
        return lastNames[rand];
    }

    public static string GetRandomName()
    {
        string str = "";
        str = GetRandomFirstName() + " " + GetRandomLastName();
        return str;
    }
}
