﻿using UnityEngine;

public enum ResultType
{
    Postion,
    Heading
}

public class NMEAResult
{
    public float Lat;
    public float Lon;
    public float Heading;
    public ResultType Type;
}

public class NMEAParser {

    public NMEAResult Parse(string nmeaSentence)
    {
        char[] separators = { ',' };
        var words = nmeaSentence.Split(separators);

        if (words.Length == 0)
        {
            return null;
        }
        //Debug.Log(words.Length);
        if (words[0] == "$GPGGA" && words.Length == 15)
        {
            //Debug.Log(words[0]);
            var degLat = int.Parse(words[2].Substring(0, 2));
            var minutesLat = float.Parse(words[2].Substring(2));
            var lat = degLat + minutesLat / 60;

            // NMEAGps incorrectly does not send five figures for longitude :/
            var decimalLocation = words[4].IndexOf(".");
            var degLon = int.Parse(words[4].Substring(0, decimalLocation - 2));
            var minutesLon = float.Parse(words[4].Substring(decimalLocation - 2));
            var lon = degLon + minutesLon / 60;

            return new NMEAResult
            {
                Type = ResultType.Postion,
                Lat = words[3] == "S" ? lat * -1 : lat,
                Lon = words[5] == "W" ? lon * -1 : lon
            };
        } else if (words[0] == "$PASHR" && words.Length == 12)
        {
            return new NMEAResult
            {
                Type = ResultType.Heading,
                Heading = float.Parse(words[2])
            };
        }
        else if (words[0] == "$GPRMC" && words.Length == 12)
        {
            var degLat = int.Parse(words[3].Substring(0, 2));
            var minutesLat = float.Parse(words[3].Substring(2));
            var lat = degLat + minutesLat / 60;

            var decimalLocation = words[5].IndexOf(".");
            var degLon = int.Parse(words[5].Substring(0, decimalLocation - 2));
            var minutesLon = float.Parse(words[5].Substring(decimalLocation - 2));
            var lon = degLon + minutesLon / 60;

            return new NMEAResult
            {
                Type = ResultType.Postion,
                Lat = words[4] == "S" ? lat * -1 : lat,
                Lon = words[6] == "W" ? lon * -1 : lon
            };
        }

        return null;
    }
}
