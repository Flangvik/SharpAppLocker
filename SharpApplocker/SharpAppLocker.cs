using System;
using Microsoft.Security.ApplicationId.PolicyManagement;
using Microsoft.Security.ApplicationId.PolicyManagement.PolicyModel;
using Newtonsoft.Json;

namespace SharpApplocker
{
    public class SharpAppLocker
    {
        public enum PolicyType
        {
            Local,
            Domain,
            Effective
        }

        public static string GetAppLockerPolicy(PolicyType policyType, string ldapPath = "", bool xmlOutput = false)
        {

            AppLockerPolicy appLockerPolicy;
            if (policyType == PolicyType.Local)
            {
                appLockerPolicy = PolicyManager.GetLocalPolicy();
            }
            else if (policyType == PolicyType.Domain)
            {
                appLockerPolicy = PolicyManager.GetDomainPolicy(ldapPath);
            }
            else
            {
                if (policyType != PolicyType.Effective)
                {
                    throw new InvalidOperationException();
                }
                appLockerPolicy = PolicyManager.GetEffectivePolicy();
            }
            if (xmlOutput)
            {
                return (appLockerPolicy.ToXml());
            }
            return JsonConvert.SerializeObject(appLockerPolicy, Formatting.Indented);
        }
    }
}
