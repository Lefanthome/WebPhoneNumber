using PhoneNumbers;
using System;
using System.Collections.Generic;

namespace WebPhoneNumber.Helpers
{
    public class LibPhoneNumberHelper
    {

        public static bool IsValidPhoneNumber(string phoneNumber, string defaultRegion)
        {
            try
            {
                var util = PhoneNumberUtil.GetInstance();
                PhoneNumber number = null;
                if (phoneNumber.StartsWith("+") || phoneNumber.StartsWith("00"))
                {
                    if (phoneNumber.StartsWith("00"))
                    {
                        phoneNumber = "+" + phoneNumber.Remove(0, 2);
                    }
                }
                number = util.Parse(phoneNumber, defaultRegion);
                return util.IsValidNumber(number);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        public static List<Tuple<string, string, string>> GetPhoneExemple()
        {
            var list = new List<Tuple<string, string, string>>();
            var util = PhoneNumberUtil.GetInstance();
            var regions = util.GetSupportedRegions();

            foreach(var regionCode in regions)
            {
                var phoneNumber = util.GetExampleNumberForType(regionCode, PhoneNumberType.FIXED_LINE_OR_MOBILE);
                list.Add(new Tuple<string, string, string>(regionCode, phoneNumber.CountryCode.ToString(), phoneNumber.NationalNumber.ToString()));
            }

            return list;
        }

        public static string FormatInternational(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return string.Empty;

            PhoneNumber number = null;
            var util = PhoneNumberUtil.GetInstance();

            if (phoneNumber.StartsWith("+") || phoneNumber.StartsWith("00"))
            {
                if (phoneNumber.StartsWith("00"))
                {
                    phoneNumber = "+" + phoneNumber.Remove(0, 2);
                }

                number = util.Parse(phoneNumber, "");
                return util.Format(number, PhoneNumberFormat.INTERNATIONAL);
            }
            else if (!string.IsNullOrEmpty(regionCode))
            {
                number = util.Parse(phoneNumber, regionCode);
                return util.Format(number, PhoneNumberFormat.INTERNATIONAL);
            }
            return string.Empty;
        }


    }
}
