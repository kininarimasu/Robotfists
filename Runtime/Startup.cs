using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Robotfists
{
    [DefaultExecutionOrder(int.MinValue)]
    public abstract class Startup : MonoBehaviour
    {
        ServiceProvider serviceProvider;

        protected void Awake()
        {
            var services = new ServiceCollection();

            // default services
            services.AddSingleton<IInjectInto>(InjectInto);
            services.AddSingleton<IInstantiator, Instantiator>();

            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            InjectIntoAllObjectsInLoadedScenes();
        }

        protected void OnDestroy()
        {
            serviceProvider.Dispose();
            serviceProvider = null;
        }

        protected abstract void ConfigureServices(IServiceCollection services);

        void InjectIntoAllObjectsInLoadedScenes()
        {
            foreach (var obj in FindAllObjectsOfType<MonoBehaviour>())
            {
                InjectInto(obj);
            }
        }

        void InjectInto(object obj)
        {
            foreach (var field in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                Debug.Log(field);
                if (field.IsDefined(typeof(ServiceAttribute), false))
                {
                    var targetType = field.FieldType;
                    var targetValue = serviceProvider.GetService(targetType) ?? throw new Exception("TODO 1");
                    field.SetValue(obj, targetValue);
                }
                else if (field.IsDefined(typeof(ComponentAttribute), false))
                {
                    var monoObj = obj as MonoBehaviour ?? throw new Exception("TODO 3");
                    var targetType = field.FieldType;
                    var targetValue = monoObj.GetComponent(targetType) ?? throw new Exception("TODO 2");
                    field.SetValue(monoObj, targetValue);
                }
            }
        }

        static IEnumerable<GameObject> GetAllRootGameObjects()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                GameObject[] rootObjs = SceneManager.GetSceneAt(i).GetRootGameObjects();
                foreach (GameObject obj in rootObjs)
                    yield return obj;
            }
        }

        static IEnumerable<T> FindAllObjectsOfType<T>() where T : MonoBehaviour
        {
            foreach (GameObject obj in GetAllRootGameObjects())
            {
                foreach (T child in obj.GetComponentsInChildren<T>(true))
                    yield return child;
            }
        }

    }
}
