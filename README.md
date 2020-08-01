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



 V1.0.0 - by Flangvik & Jean_Maes_1994


Usage:
  -h, -?, --help             Show Help

  -l, --local                Queries local applocker config

  -d, --domain               Queries domain applocker config (needs an ldap
                               path)

  -e, --effective            Queries the effective applocker config on this
                               computer

  -x, --xml                  output applocker in XML format (default is json)

      --ldap=VALUE           the ldap filter to query the domain policy from
```

 for detailed information please take a look at the MSDN url: https://docs.microsoft.com/en-us/powershell/module/applocker/get-applockerpolicy?view=win10-ps
