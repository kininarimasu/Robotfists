# Robotfists

Dependency Injection Framework for Unity3D.
Uses `Microsoft.Extensions.DependencyInjection` as a service resolver.

# Installation

Modify your `manifest.json` file found at `/<PROJECT>/Packages/manifest.json` by including the following line

```
{
	"dependencies": {
		...
		"com.danieleverland.scriptableobjectarchitecture": "https://github.com/kininarimasu/Robotfists.git#master",
		...
	}
}
```

# Usage

Startup.cs

```
using Microsoft.Extensions.DependencyInjection;

public class Startup : Robotfists.Startup
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IService, Service>();
    }
}

```

TestBehaviourScript.cs

```
using UnityEngine;
using Robotfists;

public class TestBehaviourScript : MonoBehaviour
{
    [Service]
    readonly IService _service = default;

    [Component]
    readonly Rigidbody _rigidbody = default;

    void Awake()
    {
        Debug.Log(_service);
        Debug.Log(_rigidbody);
    }
}

```
