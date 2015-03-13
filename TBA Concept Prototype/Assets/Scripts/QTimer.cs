using UnityEngine;
using System;
 
public class QTimer
 
{
	/// <summary>
	/// Constructor 
	/// </summary>
    public QTimer()
    {
        On = false;
        RecTime = 0.0f;
    }
 
	/// <summary>
	/// Checks if timer is running 
	/// </summary>
    public bool On { get; set; }
 
	/// <summary>
	/// Startup time 
	/// </summary>
    public float RecTime { get; set; }
 
	/// <summary>
	/// Start timer 
	/// </summary>
 
    public void Reset()
    {
        RecTime = Time.time;
        On = true;
    }
 
	/// <summary>
	/// Stop timer 
	/// </summary>
    public void Stop()
    {
        On = false;
        RecTime = 0.0f;
    }
 
	/// <summary>
	/// Check difference between current time and startup time 
	/// </summary>
    public float Difference()
    {
        return Time.time - RecTime;
    }
}
