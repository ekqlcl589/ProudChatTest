using Nettention.Proud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChat
{
    class CustomMarshaler : Nettention.Proud.Marshaler
    {
        public static bool Read(Message msg, out List<System.String> strList)
        {
            strList = null;
            long size = 0;
            if (!msg.ReadScalar(ref size))
            {
                return false;
            }
            strList = new List<System.String>();
            for (int i = 0; i < size; ++i)
            {
                System.String s;
                if (!msg.Read(out s))
                {
                    return false;
                }
                strList.Add(s);
            }
            return true;
        }

        public static void Write(Message msg, List<System.String> strList)
        {
            msg.WriteScalar(strList.Count);
            for (int i = 0; i < strList.Count; ++i)
            {
                msg.Write(strList[i]);
            }
        }
    }
}
