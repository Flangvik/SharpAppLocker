
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpApplocker
{
    internal class Program
    {
        private static bool CheckModes(int threshold, IEnumerable<bool> modes) => modes.Count(b => b) == threshold;

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage:");
            p.WriteOptionDescriptions(Console.Out);         
            Console.WriteLine("Examples:");
            Console.WriteLine("         SharpAppLocker.exe --effective --allow --outfile \"C:\\Windows\\Tasks\\Rules.json\" \n");
            Console.WriteLine("         SharpAppLocker.exe --effective --allow --rules=\"FileHashRule,FilePathRule\" --outfile=\"C:\\Windows\\Tasks\\Rules.json\"\n");
            Console.WriteLine("         SharpAppLocker.exe -e -D");
            Console.WriteLine("");
            Console.WriteLine("For detailed information please take a look at the MSDN url: https://docs.microsoft.com/en-us/powershell/module/applocker/get-applockerpolicy?view=win10-ps");


        }

        static void Main(string[] args)
        {
            Info.PrintBanner();

            bool help = false;
            bool localPolicy = false;
            bool domainPolicy = false;
            bool effectivePolicy = false;
            bool allowOnly = false;
            bool denyOnly = false;

            string ldapPath = "";
            string outFilePath = "";
            string[] ruleTypes = new string[] { "All" };


            var options = new OptionSet(){
                {"h|?|help","Show Help\n", o => help = true},
                {"l|local","Queries local applocker config\n",o=>localPolicy = true },
                {"d|domain","Queries domain applocker config (needs an ldap path)\n", o => domainPolicy = true },
                {"e|effective","Queries the effective applocker config on this computer\n", o => effectivePolicy = true },
                {"A|allow","Only return allowed action rules\n", o => allowOnly = true },
                {"D|deny","Only return deny action rules\n", o => denyOnly = true },
                {"ldap=","The ldap filter to query the domain policy from\n", o => ldapPath = o },
                {"rules=","Comma seperated list of ruleTypes to filter \"FileHashRule, FilePathRule, FilePublisherRule, All\" default: All\n", o => ruleTypes = o.Split(',') },
                {"outfile=","Filepath to write found rules to disk in JSON format \n", o => outFilePath = o }
            };

            try
            {
                options.Parse(args);

                IEnumerable<bool> policyModes = new List<bool> { localPolicy, domainPolicy, effectivePolicy };

                if (help)
                {
                    ShowHelp(options);
                    return;
                }

                if (CheckModes(0, policyModes))
                {
                    ShowHelp(options);
                    return;
                }

                if (!CheckModes(1, policyModes))
                {
                    Console.WriteLine("[!] You can only select one Policy at the time.");
                    return;
                }

                if (domainPolicy && String.IsNullOrEmpty(ldapPath))
                {
                    Console.WriteLine("[!] You can only query domain AppLocker configuration if you specify an LDAP filter.");
                    return;
                }


                string outPutData = "";

                if (localPolicy)
                    outPutData = SharpAppLocker.GetAppLockerPolicy(SharpAppLocker.PolicyType.Local, ruleTypes, ldapPath,  allowOnly, denyOnly);
                else if (domainPolicy)
                    outPutData = SharpAppLocker.GetAppLockerPolicy(SharpAppLocker.PolicyType.Domain, ruleTypes, ldapPath,  allowOnly, denyOnly);
                else if (effectivePolicy)
                    outPutData = SharpAppLocker.GetAppLockerPolicy(SharpAppLocker.PolicyType.Effective, ruleTypes, ldapPath,  allowOnly, denyOnly);
                else
                    throw new ArgumentException("[!] Policy-mode not found");

                if (!string.IsNullOrEmpty(outFilePath))
                {
                    File.WriteAllText(outFilePath, outPutData);
                    Console.WriteLine($"[+] Output written to: {outFilePath} \n");
                }
                else
                    Console.WriteLine(outPutData);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.InnerException);
                ShowHelp(options);
                return;
            }
        }
    }
}
