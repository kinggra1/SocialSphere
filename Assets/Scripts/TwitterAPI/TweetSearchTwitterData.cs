using System.Collections;
using UnityEngine;
using System;

public class TweetSearchTwitterData
{
    public string tweetText = "";
    public string name = "";
    public string screenName = "";
    public string profileImageUrl = "";
    public string location = "";

    public override string ToString()
    {
        return screenName + " (" + name + ") posted: \"" + tweetText + "\". Location: " + location + ". Profile image URL: " + profileImageUrl;
    }
}