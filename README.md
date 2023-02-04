> :warning: **This is still in active development and isn't meant to be used in production. Breaking changes might be introduced. Contributions are welcome!** :warning:

# How to use
Install: `dotnet add package SS.GenericController --version 1.1.0`

Add `builder.Services.AddGenericController(); in ` in `Program.cs`
![enter image description here](https://i.imgur.com/iMKPc69.png)
---
Create a controller that inherits from `GenericControllerBase` and call it's constructor and pass it a `Microsoft.EntityFrameworkCore.DbContext`

![Your controller that inherits from the base](https://i.imgur.com/rPbxAa4.png)
---
For each class that implements `IObjectBase` a controller with the following endpoints will be added:
![Get many](https://i.imgur.com/eAUPLnV.png)
![Get single](https://i.imgur.com/PnuYV9D.png)
![Add](https://i.imgur.com/A9nBXBl.png)
![Update](https://i.imgur.com/6Fa3YVl.png)
![Delete](https://i.imgur.com/oDxQ0uA.png)
`entitiesToInclude` is a string that can be supplied in order to `.Include()` and `.ThenInclude()` related entities on the model and the format is as follows:

    "[entity to include].[entity to then include],[another entity to include].[another entity to then include]"
---
Since all methods are virtual, you have the ability to override them and add attributes on each individual one
![Overriden](https://i.imgur.com/5lGSRhJ.png)
