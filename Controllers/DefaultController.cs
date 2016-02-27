using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic.Devices;

namespace AspNetSystemInfo.Controllers
{
    public class DefaultController : Controller
    {
        public PermissionSetAttribute _permissionSetAttribute = new PermissionSetAttribute(SecurityAction.Assert);

        public ComputerInfo _computerInfo = new ComputerInfo();
        public PerformanceCounter _uptime = new PerformanceCounter("System", "System Up Time");
        public DriveInfo _driveInfo = new DriveInfo("c");
        //protected ManagementClass _cpuManagement = new ManagementClass("Win32_Processor");

        public TimeSpan Uptime
        {
            get { return TimeSpan.FromSeconds(_uptime.NextValue()); }
        }

        // GET: Default
        public ActionResult Index()
        {
            return View(this);
        }

        public static string ToSizeString(double bytes)
        {
            var culture = CultureInfo.CurrentUICulture;
            const string format = "#,0.0";

            if (bytes < 1024)
                return bytes.ToString("#,0", culture);
            bytes /= 1024;
            if (bytes < 1024)
                return bytes.ToString(format, culture) + " KB";
            bytes /= 1024;
            if (bytes < 1024)
                return bytes.ToString(format, culture) + " MB";
            bytes /= 1024;
            if (bytes < 1024)
                return bytes.ToString(format, culture) + " GB";
            bytes /= 1024;
            return bytes.ToString(format, culture) + " TB";
        }
    }
}