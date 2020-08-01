
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharplocker
{

    class Program
    {
        public static bool CheckModes(int threshold, IEnumerable<bool> modes)
        { return modes.Count(b => b) == threshold; }

        public static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage:");
            p.WriteOptionDescriptions(Console.Out);
            Console.WriteLine("\n for detailed information please take a look at the MSDN url: https://docs.microsoft.com/en-us/powershell/module/applocker/get-applockerpolicy?view=win10-ps");
        }


        static void Main(string[] args)
        {
            Info.PrintBanner();

            bool help = false;
            bool xmlOutput = false;
            bool localPolicy = false;
            bool domainPolicy = false;
            bool effectivePolicy = false;
            String ldapPath = "";
           

            var options = new OptionSet(){
                {"h|?|help","Show Help\n", o => help = true},
                {"l|local","Queries local applocker config\n",o=>localPolicy = true },
                {"d|domain","Queries domain applocker config (needs an ldap path)\n", o => domainPolicy = true },
                {"e|effective","Queries the effective applocker config on this computer\n", o => effectivePolicy = true },
                {"x|xml","output applocker in XML format (default is json) \n", o => xmlOutput = false },
                {"ldap=","the ldap filter to query the domain policy from\n", o => ldapPath = o }
                
            };

            try
            {
                options.Parse(args);
                IEnumerable<bool> modes = new List<bool> { localPolicy, domainPolicy, effectivePolicy };

                if(CheckModes(0,modes))
                {
                    ShowHelp(options);
                    return;
                }
                
                if(!CheckModes(1, modes))
                {
                    Console.WriteLine("You can only select one Policy at the time.");
                    return;
                }

                if(domainPolicy && String.IsNullOrEmpty(ldapPath))
                {
                    Console.WriteLine("you can only query domain applocker configuration if you specify an ldap filter.");
                    return;
                }

                if (help)
                {
                    ShowHelp(options);
                    return;
                }

                if (localPolicy)
                    Console.WriteLine(SharpLocker.GetAppLockerPolicy(SharpLocker.PolicyType.Local, ldapPath, xmlOutput));
                else if (domainPolicy)
                    Console.WriteLine(SharpLocker.GetAppLockerPolicy(SharpLocker.PolicyType.Domain, ldapPath, xmlOutput));
                else if (effectivePolicy)
                    Console.WriteLine(SharpLocker.GetAppLockerPolicy(SharpLocker.PolicyType.Effective, ldapPath, xmlOutput));
                else
                    throw new ArgumentException("mode not found");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                ShowHelp(options);
                return;
            }
        }
    }
}
