using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDisplayRotate.MVVM.Model
{
    internal class DeviceList
    {
        private string? displayList { set; get;}
        private string[]? gyroList { set; get;}
        private bool? connectState { set; get;}

        public DeviceList(string displayList, string[] gyroList, bool connectState)
        {
            this.displayList = displayList;
            this.gyroList = gyroList;
            this.connectState = connectState;
        }

    }
}
