using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace ConsoleApplication3.Classes
{
    public class KeyGenerator
    {
        public string GenerateNewKey()
        {
            var data = this.GetKeyData();
            var keyParts = this.GetKeyParts(data);
            var key = this.AppendPartsToFullKey(keyParts);

            return key;

        }

        private byte[] GetKeyData()
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault<NetworkInterface>();
            if (networkInterface == null)
            {
                throw new Exception("Problem with Network element, please do something");
            }

            var physicalAddressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();

            return physicalAddressBytes;
        }

        private IEnumerable<int> GetKeyParts(byte[] data)
        {
            var processedData = data.Select(new Func<byte, int, int>(eval_a));
            IEnumerable<int> keyParts = processedData.Select(eval_a).ToArray();

            return keyParts;
        }
        private string AppendPartsToFullKey(IEnumerable<int> keyParts)
        {
            var key = "";
            var sb = new StringBuilder();
            foreach (var item in keyParts)
            {
                sb.Append(item + "-");
            }
            key = sb.ToString().TrimEnd('-');

            return key;
        }

        //code from exe file
        private int eval_a(byte A_0, int A_1)
        {
            var test = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
            return (int)(A_0 ^ test[A_1]);
        }

        //code from exe file
        private int eval_a(int A_0)
        {
            if (A_0 <= 999)
            {
                return A_0 * 10;
            }
            return A_0;
        }
    }
}
