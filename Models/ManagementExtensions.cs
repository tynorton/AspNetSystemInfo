using System.Collections.Generic;
using System.Management;

namespace AspNetSystemInfo.Models
{
    public static class ManagementExtensions
    {
        public static Dictionary<string, Dictionary<string, object>> GetManagementInfo(string key, bool dontInsertNull = false)
        {
            var dict = new Dictionary<string, Dictionary<string, object>>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + key);

            foreach (ManagementObject share in searcher.Get())
            {
                var items = new Dictionary<string, object>();
                foreach (PropertyData PC in share.Properties)
                {
                    if (PC.Value != null && PC.Value.ToString() != "")
                    {
                        switch (PC.Value.GetType().ToString())
                        {
                            case "System.String[]":
                                string[] str = (string[])PC.Value;

                                string str2 = "";
                                foreach (string st in str)
                                    str2 += st + " ";

                                items.Add(PC.Name, str2);

                                break;
                            case "System.UInt16[]":
                                ushort[] shortData = (ushort[])PC.Value;


                                string tstr2 = "";
                                foreach (ushort st in shortData)
                                    tstr2 += st.ToString() + " ";

                                items.Add(PC.Name, tstr2);

                                break;

                            default:
                                items.Add(PC.Name, PC.Value.ToString());
                                break;
                        }
                    }
                    else
                    {
                        if (!dontInsertNull)
                            items.Add(PC.Name, "No Information available");
                        else
                            continue;
                    }
                }
                dict.Add(share.Path.ToString(), items);
            }

            return dict;
        }
    }
}