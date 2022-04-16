using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDisplayRotate.MVVM.Model
{
    public class DeviceList
    {
        public string displayList { set; get;}
        public string[] gyroList { set; get;}
        public bool connectState { set; get;}

        public DeviceList(string displayList, string[] gyroList, bool connectState)
        {
            this.displayList = displayList;
            this.gyroList = gyroList;
            this.connectState = connectState;
        }

    }
}
