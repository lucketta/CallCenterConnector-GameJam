using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablePortsManager : MonoBehaviour
{
    [SerializeField] private float countDownTime = 30f;
    private CablePort[] ports;
    private int numberSelected = 0;
    public void Start()
    {
        //get all ports when game starts
        ports = FindObjectsOfType<CablePort>();
        Debug.Log(ports.Length);
        StartCoroutine(ActivatePorts());
    }

    public CablePort SelectClosestPort(CablePort myPort)
    {
        float min = float.MaxValue;
        int minIndex = -1;
        CablePort closestPort;

        for (int i = 0; i < ports.Length; i++)
        {
            if (ports[i] != myPort && !ports[i].wasSelected)
            {
                if (Vector3.Distance(myPort.transform.position, ports[i].transform.position) < min)
                {
                    minIndex = i;
                    min = Vector3.Distance(myPort.transform.position, ports[i].transform.position);
                }
            }
            if (minIndex == -1 && i == ports.Length - 1)
            {
                Debug.Log("Not enough ports in scene");
                return null;
            }
        }

        closestPort = ports[minIndex];
        Debug.Log(minIndex);
        closestPort.wasSelected = true;
        closestPort.myRenderer.material = closestPort.selectedMaterial;
        numberSelected++;

        return closestPort;
    }

    public CablePort SelectRandomPort()
    {
        CablePort myPort = ports[UnityEngine.Random.Range(0, ports.Length)];
        int index = -1;
        // avoid selecting the same port multiple times.
        while (myPort.wasSelected)
        {
            index = UnityEngine.Random.Range(0, ports.Length);
            myPort = ports[index];
        }
        myPort.myRenderer.material = myPort.selectedMaterial;
        myPort.wasSelected = true;
        numberSelected++;

        return myPort;
    }

    public IEnumerator ActivatePorts()
    {
        CablePort port1;
        CablePort port2;
        float currentTime;

        while (numberSelected <= ports.Length)
        {
            if(numberSelected == ports.Length)
            {
                Debug.Log("All Ports selected. You win (?)");
                yield break;
            }
            //have an even number of ports in the scene
            port1 = SelectRandomPort();
            port2 = SelectRandomPort();
            if (port1 == null || port2 == null)
            {
                Debug.Log("Not enough ports (Activate Ports)");
                yield break;

            }
            Debug.Log("Ports activated!");
            currentTime = countDownTime;

            while (currentTime > 0)
            {
                yield return new WaitForSeconds(1f);
                currentTime--;
                Debug.Log("Countdown: " + currentTime);
            }
        }
    }
}
