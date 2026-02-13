using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class AppSettings
    {
        private static AppSettings? _instance;

        public string Environment { get; set; } = "Staging";

        public bool EnablePaymentLogging { get; set; } = true;

        public bool EnablePaymentTiming { get; set; } = true;

        private AppSettings() { }

        public static AppSettings Instance => _instance ??= new AppSettings();
    }
}
