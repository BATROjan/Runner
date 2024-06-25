# **_VGBootstrapSystem_**
# Purpose of the package
The package was created to simplify the work with bootstrap via [Zenject](https://github.com/modesttree/Zenject)
# Technologies used 
- Unity 2019.4.38f1 or higher;
- Zenject 9.2.0-stcf3;
- VGCore 1.0.4;
- UniRX 7.1.0.

# Package setup
## Import
First of all you need to import the package to your project:

1. Go to the advanced package manager of your project:
![](https://answers.unity.com/storage/temp/175641-screenshot-21.png)
2. Set these:

- Name - Name of Registry (1 for all `com.vgcore` packages)
- URL - https://verdaccio.smw.team/
- Scope(s) - com.vgcore
3. Save

## Using
```c#
using VGBootstrapSystem;
```
Then you will be able to use methods of the package
