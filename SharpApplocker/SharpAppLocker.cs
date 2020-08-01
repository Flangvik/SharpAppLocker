using System;
using System.Xml;
using Newtonsoft.Json;

namespace SharpApplocker {
    public class SharpAppLocker {
        public enum PolicyType {
            Local,
            Domain,
            Effective
        }

        public static string GetAppLockerPolicy(PolicyType policyType, string ldapPath = "", bool xmlOutput = false) {

            // Create IAppIdPolicyHandler COM interface
            IAppIdPolicyHandler IAppHandler = new AppIdPolicyHandlerClass();
            string policies;

            switch(policyType) {
                case PolicyType.Local:
                case PolicyType.Domain:
                    policies = IAppHandler.GetPolicy(ldapPath);
                    break;

                case PolicyType.Effective:
                    policies = IAppHandler.GetEffectivePolicy();
                    break;

                default:
                    throw new InvalidOperationException();
            }

            if (xmlOutput)
                return policies;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(policies);
            return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);
        }
    }
}
