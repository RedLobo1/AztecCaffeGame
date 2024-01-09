using UnityEngine;

public interface ITimeStoppable
{
    void StopTime(float duration);
}

public class TimeStopper : MonoBehaviour, ITimeStoppable
{
    public void StopTime(float duration)
    {
        StartCoroutine(StopTimeCoroutine(duration));
    }

    private System.Collections.IEnumerator StopTimeCoroutine(float duration)
    {
        // Stop time
        Time.timeScale = 0;

        // Wait for the specified duration
        yield return new WaitForSecondsRealtime(duration);

        // Resume time
        Time.timeScale = 1;
    }
}
