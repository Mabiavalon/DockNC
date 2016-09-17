# DockNC
Docking Library for Avalonia

**This project is still a Work In Progress. More informatio will be added as more of the project becomes complete.**

[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/DockNC/Lobby?utm_source=badge&utm_medium=badge)

| Windows | OSX/Linux |
| :---: |  :---: |
| [![Build status](https://ci.appveyor.com/api/projects/status/35okrh54qkpi8cal/branch/master?svg=true)](https://ci.appveyor.com/project/DarnellWilliams/docknc/branch/master) | [![Build Status](https://travis-ci.org/Mabiavalon/DockNC.svg?branch=master)](https://travis-ci.org/Mabiavalon/DockNC) | 

**Mabiavalon.DockNC** is a control docking library written for [Avalonia](https://github.com/AvaloniaUI/Avalonia).

[![Example GIF Animation](https://puu.sh/rdyPD/863681ed37.gif)](https://puu.sh/rdyPD/863681ed37.gif)

### Build using IDE

* [Visual Studio Community 2015](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx) for `Windows` builds.
* [MonoDevelop](http://www.monodevelop.com/) for `Linux` and `OSX` builds.

Open `Mabiavalon.DockNC.sln` in selected IDE and run `Build` command.

### Build on Windows using script

Open up a Powershell prompt and execute the bootstrapper script:
```PowerShell
PS> .\build.ps1 -Target "Default" -Platform "Any CPU" -Configuration "Release"
```

### Build on Linux/OSX using script

Open up a terminal prompt and execute the bootstrapper script:
```Bash
$ ./build.sh --target "Default" --platform "Any CPU" --configuration "Release"
```

## NuGet

Mabiavalon.DockNC is delivered as a NuGet package.

Using the nightly build feed:
* Add `https://www.myget.org/F/mabiavalon-ci/api/v2` to your package sources
* Update your package using `Mabiavalon.DockNC` feed

You can install the package like this:

`Install-Package Mabiavalon.DockNC -Pre`

### Package Dependencies

* [Avalonia](https://github.com/AvaloniaUI/Avalonia)

### Package Sources

* https://api.nuget.org/v3/index.json
* https://www.myget.org/F/avalonia-ci/api/v2



