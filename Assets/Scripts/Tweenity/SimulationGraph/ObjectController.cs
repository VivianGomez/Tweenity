//
//  ObjectController.cs
//  Tweenity
//
//  Created by Vivian Gómez.
//  Copyright © 2021 Vivian Gómez - Pablo Figueroa - Universidad de los Andes
//

using System.Threading;
using UnityEngine;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// Esta clase permite buscar los métodos o funciones y ejecutarlos, usando la técnica de introspección
// del lenguaje (C#)
public class ObjectController : MonoBehaviour
{
    public UnityEngine.Object modelObject;
    CancellationTokenSource tokenSource;


    private void Awake() {
        tokenSource = new CancellationTokenSource();
    }


    private void OnDisable() 
    {
        tokenSource.Cancel();
    }

    // Busca el método en el objeto modelObject, que tenga el mismo nombre y parámetos dados
    // Si lo encuentra, lo invoca con los parámetros dados (si es que tiene)
    public async Task<MethodInfo> MethodAccess(string methodName, string args, int delay=200)
    {
        Debug.Log("Se está buscando el método llamado "+methodName+" con los parámetros "+ args);
        var modelObjectScript = modelObject.GetType();
        var loadingMethod = modelObjectScript.GetMethod(methodName);

        if(loadingMethod!=null)
        {
            if(args=="")
            {
                loadingMethod.Invoke(modelObject, System.Type.EmptyTypes);
            }
            else
            {
                var splitArgs = Regex.Replace(args, @"\s", "").Split(';');
                if(methodName.StartsWith("Play") || methodName=="Wait")
                {
                    delay = (int) loadingMethod.Invoke(modelObject, splitArgs);
                }
                else
                {
                    loadingMethod.Invoke(modelObject, splitArgs);
                }
                
            }
        }
        else
        {
            Debug.Log("No existe un método llamado "+ methodName + ", en el objeto "+ modelObject);
        }
        
        Debug.Log("Esperando ... "+ delay);

        await Task.Delay(delay, tokenSource.Token);

        return loadingMethod;
    }  
}
