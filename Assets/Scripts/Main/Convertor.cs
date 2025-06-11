using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class Convertor : SingletonMonoBehaviour<Convertor>
{
    public List<string> SplitString(string input, char splitter)
    {
        List<string> result = new List<string>();
        string[] parts = input.Split(splitter);

        foreach (string part in parts)
        {
            if (!string.IsNullOrEmpty(part))
            {
                result.Add(part);
            }
        }

        return result;
    }

    public DateTime? ConvertToDate(string input)
    {
        int day;
        int month;
        int year = DateTime.Today.Year;
        string[] parts = input.Split(new char[] { '.', ',', '/', ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length > 3)
        {
            return null;
        }

        if (!int.TryParse(parts[0], out day) || day < 1 || day > 31)
        {
            return null;
        }

        if (!int.TryParse(parts[1], out month) || month < 1 || month > 12)
        {
            return null;
        }

        if (parts.Length == 3)
        {
            if (!int.TryParse(parts[2], out year))
            {
                return null;
            }
        }
        return new DateTime(year, month, day);
    }
}
