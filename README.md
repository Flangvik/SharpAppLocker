# SharpAppLocker

C# port of the Get-AppLockerPolicy PS cmdlet

```
 _____ _                       ___              _                _
/  ___| |                     / _ \            | |              | |
\ `--.| |__   __ _ _ __ _ __ / /_\ \_ __  _ __ | |     ___   ___| | _____ _ __
 `--. \ '_ \ / _` | '__| '_ \|  _  | '_ \| '_ \| |    / _ \ / __| |/ / _ \ '__|
/\__/ / | | | (_| | |  | |_) | | | | |_) | |_) | |___| (_) | (__|   <  __/ |
\____/|_| |_|\__,_|_|  | .__/\_| |_/ .__/| .__/\_____/\___/ \___|_|\_\___|_|
                       | |         | |   | |
                       |_|         |_|   |_|


 V1.1.0 - by Flangvik & Jean_Maes_1994 , vastly improved by am0nsec


Usage:
  -h, -?, --help              Show Help
 
  -l, --local                   Queries local applocker config

  -d, --domain               Queries domain applocker config (needs an ldap path)

  -e, --effective            Queries the effective applocker config on this computer

  -A, --allow                 Only return allowed action rules

  -D, --deny                 Only return deny action rules

      --ldap=VALUE         The ldap filter to query the domain policy from

      --rules=VALUE        Comma seperated list of ruleTypes to filter "FileHashRule, FilePathRule, FilePublisherRule,  All" default: All

      --outfile=VALUE      Filepath to write found rules to disk in JSON format

Examples:
         SharpAppLocker.exe --effective --allow --outfile "C:\Windows\Tasks\Rules.json"

         SharpAppLocker.exe --effective --allow --rules="FileHashRule,FilePathRule" --outfile="C:\Windows\Tasks\Rules.json"

         SharpAppLocker.exe -e -D

```

![CS execution](https://i.imgur.com/BMSY2T5.png)



 for detailed information please take a look at the MSDN url: https://docs.microsoft.com/en-us/powershell/module/applocker/get-applockerpolicy?view=win10-ps
